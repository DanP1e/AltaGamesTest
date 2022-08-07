using AltaGamesTest.Interactions;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class VolumePumper : StopableMonoBehaviour
    {
        [SerializeField] private AnimationCurve _pumpingSpeedCurve;

        private GameControls _gameControls;
        private IVolumeContainer _mainContainer;
        private IProjectileProvider _projectileProvider;
        private IContainersMover _containersMover;

        private IVolumeContainer ProjectileContainer
        {
            get 
            {
                IProjectile projectile = _projectileProvider.GetLastProjectile();
                return projectile == null ? null : projectile.VolumeContainer;
            }
        }                 

        [Inject]
        public void Construct(
            IVolumeContainer mainContainer, 
            IProjectileProvider projectileProvider,
            IContainersMover containersMover)
        {
            _mainContainer = mainContainer;
            _projectileProvider = projectileProvider;
            _containersMover = containersMover;
        }

        protected void Awake()
        {
            _gameControls = new GameControls();
            _gameControls.Volume.StartOfSplitting.performed += OnStartOfSplitting;
            _gameControls.Volume.EndOfSplitting.performed += OnEndOfSplitting;
            _gameControls.Enable();
        }

        protected void OnDestroy()
        {
            _gameControls.Volume.StartOfSplitting.performed -= OnStartOfSplitting;
            _gameControls.Volume.EndOfSplitting.performed -= OnEndOfSplitting;

            _gameControls.Enable();
        }

        private void OnEndOfSplitting(InputAction.CallbackContext obj)
        {
            TryStopPumpProcess();

            if (IsCanStartPumpProcess()
             && !_projectileProvider.GetLastProjectile().CriticalVolumeAchiever.IsCriticalVolume)
                StartCoroutine(
                    StartPumpProcess(ProjectileContainer, _mainContainer));
        }

        private void OnStartOfSplitting(InputAction.CallbackContext obj)
        {
            TryStopPumpProcess();

            if (IsCanStartPumpProcess())
                StartCoroutine(
                    StartPumpProcess(_mainContainer, ProjectileContainer));
        }

        private bool IsCanStartPumpProcess()
        {
            return ProjectileContainer != null 
                && enabled == true
                && !_containersMover.IsMoving;
        }

        private void TryStopPumpProcess()
        {
            if (enabled == false)
                return;

            StopAllCoroutines();
        }

        private IEnumerator StartPumpProcess(
            IVolumeContainer fromContainer,
            IVolumeContainer toContainer)
        {
            float timer = 0;
            while (fromContainer != null
                && toContainer != null
                && ProjectileContainer != null
                && !_containersMover.IsMoving
                && enabled == true)
            {
                float volume = fromContainer
                    .ClaimVolume(_pumpingSpeedCurve.Evaluate(timer) * Time.deltaTime);

                if (volume == 0)
                    break;

                toContainer.AddVolume(volume);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
