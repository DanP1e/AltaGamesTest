using AltaGamesTest.Interactions;
using InspectorAddons;
using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ProjectilesSpawner : StopableMonoBehaviour, IProjectileProvider
    {
        [SerializeField] private InterfaceComponent<IProjectile> _projectilePrefab;

        private IProjectile _projectile;
        private Context _context;

        public event UnityAction<IProjectile> NewProjectileSpawned;

        public IProjectile GetLastProjectile()
            => _projectile;

        [Inject]
        public void Construct(Context context)
        {
            _context = context;
            SpawnNewProjectile();
        }

        public void SpawnNewProjectile()
        {
            if (enabled == false)
                return;

            if (_projectile != null)
                throw new DoubleSpawnException();

            GameObject obj = _context.Container.InstantiatePrefab(_projectilePrefab.Object);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.SetParent(transform);
            _projectile = (IProjectile)obj.GetComponent(typeof(IProjectile));          
            _projectile.Destroyed += UnsubscribeProjectile;
            NewProjectileSpawned?.Invoke(_projectile);
        }

        private void UnsubscribeProjectile(IDestroyable projectile)
        {
            projectile.Destroyed -= UnsubscribeProjectile;
            _projectile = null;
        }

        private void OnDestroy()
        {
            if (_projectile == null)
                return;

            _projectile.Destroy();
        }
    }
}
