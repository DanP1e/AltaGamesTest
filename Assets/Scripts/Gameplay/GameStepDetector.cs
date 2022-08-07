using AltaGamesTest.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class GameStepDetector : MonoBehaviour, IGameStepDetector
    {
        [SerializeField] private float _gameStepMaxTime = 1f;

        public event UnityAction StepDone;

        public bool IsStepInProcess => _infectedComponents.Count != 0;

        private IProjectileProvider _projectileProvider;
        private IComponentsProvider<IInfectable> _infectableComponentsProvider;
        private List<IInfectable> _infectableComponents = new List<IInfectable>();
        private List<IInfectable> _infectedComponents = new List<IInfectable>();

        [Inject]
        public void Construct(IProjectileProvider projectileProvider,
            IComponentsProvider<IInfectable> infectableComponentsProvider)
        {
            _projectileProvider = projectileProvider;
            _infectableComponentsProvider = infectableComponentsProvider;
            _infectableComponents = _infectableComponentsProvider.GetComponents();

            _projectileProvider.GetLastProjectile().Destroyed += OnProjectileDestroyed;
            _projectileProvider.NewProjectileSpawned += OnProjectileSpawned;

            foreach (var infectableComponent in _infectableComponents)
            {
                infectableComponent.Infected += OnInfectableComponentInfected;
                infectableComponent.Destroyed += OnInfectableComponentDestroyed;
            }
        }

        protected void OnDestroy()
        {
            foreach (var item in _infectableComponents)
            {
                item.Destroyed -= OnInfectableComponentDestroyed;
                item.Infected -= OnInfectableComponentInfected;
            }
        }

        private void OnProjectileSpawned(IProjectile projectile)
        {
            projectile.Destroyed += OnProjectileDestroyed;
        }

        private void OnProjectileDestroyed(IDestroyable projectile)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            StartCoroutine(StartStepTimer());
        }

        private void OnInfectableComponentDestroyed(IDestroyable destroyableComponent)
        {
            int index = _infectableComponents.FindIndex(x => x == destroyableComponent);
            IInfectable infectableComponent = _infectableComponents[index];

            destroyableComponent.Destroyed -= OnInfectableComponentDestroyed;
            infectableComponent.Infected -= OnInfectableComponentInfected;

            _infectedComponents.Remove(infectableComponent);
            _infectableComponents.Remove(infectableComponent);

            CheckIsStepDone();
        }

        private void CheckIsStepDone()
        {
            if (!IsStepInProcess)
            {
                StepDone?.Invoke();          
                StopAllCoroutines();
            }
        }

        private void OnInfectableComponentInfected(IInfectable infectableComponent)
        {
            _infectedComponents.Add(infectableComponent);
        }

        private IEnumerator StartStepTimer()
        {
            yield return new WaitForSeconds(_gameStepMaxTime);
            StepDone?.Invoke();
        }
    }
}
