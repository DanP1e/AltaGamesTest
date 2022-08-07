using AltaGamesTest.Effects;
using AltaGamesTest.Interactions;
using InspectorAddons;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class InfectableObstacle : InteractDummy, IInfectable
    {
        [SerializeField] private Color _infectedColor = Color.yellow;
        [SerializeField] private InterfaceComponent<IColorProvider> _obstacleColorProviderComponent;
        [SerializeField] private float _explosionDelay = 1;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Transform _explosionTransformParams;

        private DiContainer _sceneContainer;
        private IColorProvider _obstacleColorProvider;
        private bool _isInfected = false;

        public bool IsInfected => _isInfected;

        public event UnityAction<IDestroyable> Destroyed;
        public event UnityAction<IInfectable> Infected;

        [Inject]
        public void Construct(DiContainer sceneContainer) 
        {
            _sceneContainer = sceneContainer;
            _obstacleColorProvider = _obstacleColorProviderComponent.Interface;
        }

        public void Infect()
        {
            if (_isInfected)
                return;

            StartCoroutine(StartExplosionProcess(_explosionDelay));
            _obstacleColorProvider.Color = _infectedColor;
            _isInfected = true;

            Infected?.Invoke(this);
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Destroyed?.Invoke(this);
        }

        protected void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        private IEnumerator StartExplosionProcess(float explosionDelay)
        {
            yield return new WaitForSeconds(explosionDelay);
            Explode();
        }

        private void Explode()
        {
            var effect = _sceneContainer.InstantiatePrefab(_explosionPrefab);
            effect.transform.position = _explosionTransformParams.position;
            effect.transform.localScale = _explosionTransformParams.localScale;

            Destroy();
        }     
    }
}
