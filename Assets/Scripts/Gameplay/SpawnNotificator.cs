using System;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class SpawnNotificator : MonoBehaviour
    {
        private IInfectableObstaclesChecker _obstaclesChecker;
        private IProjectileProvider _projectileProvider;

        [Inject]
        public void Construct(
            IInfectableObstaclesChecker obstaclesChecker, 
            IProjectileProvider projectileProvider)
        {
            _obstaclesChecker = obstaclesChecker;
            _projectileProvider = projectileProvider;
            _obstaclesChecker.NewObstacleDetected += OnNewObstacleDetected;
        }

        private void OnNewObstacleDetected()
        {
            try
            {
                _projectileProvider.SpawnNewProjectile();
            }
            catch (DoubleSpawnException) { }
        }
    }
}
