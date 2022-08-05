using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class VolumePumper : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _pumpingSpeedCurve;

        private GameControls _gameControls;
        private IVolumeContainer _mainContainer;
        private IProjectileProvider _projectileProvider;
        private Coroutine _pumpProcess;

        private IVolumeContainer ProjectileContainer 
            => _projectileProvider.GetProjectile().VolumeContainer;

        [Inject]
        public void Construct(IVolumeContainer mainContainer, 
            IProjectileProvider projectileProvider)
        {
            _mainContainer = mainContainer;
            _projectileProvider = projectileProvider;
        }

        protected void Awake()
        {
            _gameControls = new GameControls();
            _gameControls.Volume.StartOfSplitting.performed += OnStartOfSplitting;
            _gameControls.Volume.EndOfSplitting.performed += OnEndOfSplitting;
            _gameControls.Enable();
        }

        private void OnEndOfSplitting(InputAction.CallbackContext obj)
        {
            if (_pumpProcess != null)               
                StopCoroutine(_pumpProcess);

            if (ProjectileContainer != null)
                _pumpProcess = StartCoroutine(
                    StartPumpProcess(ProjectileContainer, _mainContainer));
        }

        private void OnStartOfSplitting(InputAction.CallbackContext obj)
        {
            if (_pumpProcess != null)
                StopCoroutine(_pumpProcess);

            if(ProjectileContainer != null)
                _pumpProcess = StartCoroutine(
                    StartPumpProcess(_mainContainer, ProjectileContainer));
        }

        private IEnumerator StartPumpProcess(IVolumeContainer fromContainer, IVolumeContainer toContainer)
        {
            float timer = 0;
            while (true)
            {
                if (fromContainer == null || toContainer == null)
                    yield return null;

                float volume = fromContainer.ClaimVolume(_pumpingSpeedCurve.Evaluate(timer) * Time.deltaTime);
                if (volume == 0)
                    break;

                toContainer.AddVolume(volume);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
