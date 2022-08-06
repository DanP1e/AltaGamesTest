using AltaGamesTest.Scenes;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class DefeatChecker : MonoBehaviour
    {
        private ICriticalVolumeAchiever _mainCriticalVolumeAchiver;
        private IProjectileProvider _projectileProvider;
        private ISceneFinisher _sceneFinisher;

        [Inject]
        public void Construct(
            ICriticalVolumeAchiever mainCriticalVolumeAchiver,
            IProjectileProvider projectileProvider,
            ISceneFinisher sceneFinisher)
        {
            _mainCriticalVolumeAchiver = mainCriticalVolumeAchiver;
            _projectileProvider = projectileProvider;
            _sceneFinisher = sceneFinisher;
            _projectileProvider.NewProjectileSpawned += OnNewProjectileSpawned;
            _projectileProvider.GetLastProjectile().Destroyed += OnLastProjectileDestroyed;
        }

        private void OnNewProjectileSpawned(IProjectile projectile)
        {
            projectile.Destroyed += OnLastProjectileDestroyed;
        }

        private void OnLastProjectileDestroyed(IDestroyable projectile)
        {
            projectile.Destroyed -= OnLastProjectileDestroyed;
            if (!_mainCriticalVolumeAchiver.IsCriticalVolume 
                && !_sceneFinisher.IsGameFinishing)
                _sceneFinisher.FinishWithDefeat();
        }
    }
}
