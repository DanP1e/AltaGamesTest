using System;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class SpawnNotificator : MonoBehaviour
    {
        private IDeliverer _deliverer;
        private IProjectileProvider _projectileProvider;

        [Inject]
        public void Construct(
            IDeliverer deliverer, 
            IProjectileProvider projectileProvider)
        {
            _deliverer = deliverer;
            _projectileProvider = projectileProvider;
            deliverer.DeliveredTo += OnDelivererDeliveredTo;
        }

        private void OnDelivererDeliveredTo(GameObject arg0)
        {
            _deliverer.DeliveredTo -= OnDelivererDeliveredTo;
            _projectileProvider.SpawnNewProjectile();
        }
    }
}
