using AltaGamesTest.Interactions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ProjectileDeliverer : MonoBehaviour, IDeliverer
    {
        [SerializeField] private float _speed = 10;

        private ICriticalVolumeAchiever _criticalVolumeAchiever;
        private GameControls _gameControls;

        public event UnityAction<GameObject> DeliveredTo;

        [Inject]
        public void Construct(ICriticalVolumeAchiever criticalVolumeAchiever)
        {
            _criticalVolumeAchiever = criticalVolumeAchiever;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent(typeof(IInteractable)) == null)
                return;

            DeliveredTo?.Invoke(collision.gameObject);
            Destroy(gameObject);
        }

        protected void Awake()
        {
            _gameControls = new GameControls();
            _gameControls.Volume.EndOfSplitting.performed += OnEndOfSpliting;
            _gameControls.Enable();
        }

        protected void OnDestroy()
        {
            _gameControls.Volume.EndOfSplitting.performed -= OnEndOfSpliting;
            _gameControls.Disable();
        }

        private void OnEndOfSpliting(InputAction.CallbackContext obj)
        {
            if (_criticalVolumeAchiever == null)
                return;

            if(_criticalVolumeAchiever.IsCriticalVolume)
                StartCoroutine(StartMove());
        }

        private IEnumerator StartMove()
        {
            while (true)
            {
                transform.Translate(transform.forward * Time.deltaTime * _speed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
