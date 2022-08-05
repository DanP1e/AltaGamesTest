using InspectorAddons;
using System;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ProjectilesSpawner : MonoBehaviour, IProjectileProvider
    {
        [SerializeField] private InterfaceComponent<IProjectile> _projectilePrefab;

        private IProjectile _projectile;
        private Context _context;

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
            GameObject obj = _context.Container.InstantiatePrefab(_projectilePrefab.Object);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            _projectile = (IProjectile)obj.GetComponent(typeof(IProjectile));          
            _projectile.Destroyed += UnsubscribeProjectile;
            _projectile.CriticalVolumeAchiever.CriticalVolumeAchieved += OnCriticalVolumeAchived;
        }

        private void OnCriticalVolumeAchived(ICriticalVolumeAchiever achiver)
        {
            achiver.CriticalVolumeAchieved -= OnCriticalVolumeAchived;
            UnsubscribeProjectile(_projectile);
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
