using AltaGamesTest.Effects;
using AltaGamesTest.Interactions;
using InspectorAddons;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class InfectableObstacle : InteractDummy, IInfectable, IDestroyable
    {
        [SerializeField] private Color _infectedColor = Color.yellow;
        [SerializeField] private InterfaceComponent<IColorProvider> _obstacleColorProviderComponent;
        [SerializeField] private float _explosionDelay = 1;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Transform _explosionTransformParams;

        private DiContainer _sceneContainer;
        private IColorProvider _obstacleColorProvider;

        public event UnityAction<IDestroyable> Destroyed;

        [Inject]
        public void Construct(DiContainer sceneContainer) 
        {
            _sceneContainer = sceneContainer;
            _obstacleColorProvider = _obstacleColorProviderComponent.Interface;
        }

        public void Infect()
        {
            StartCoroutine(StartExplosionProcess(_explosionDelay));
            _obstacleColorProvider.Color = _infectedColor;
        }

        public void Destroy()
        {
            Destroy(gameObject);
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
