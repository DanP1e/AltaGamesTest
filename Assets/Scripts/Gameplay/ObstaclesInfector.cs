using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ObstaclesInfector : MonoBehaviour
    {
        [SerializeField] private float _volumeInfectionRadiusRatio = 1f;
        
        private IDeliverer _deliverer;
        private IVolumeContainer _volumeContainer;

        [Inject]
        public void Construct(
            IDeliverer deliverer, 
            IVolumeContainer volumeContainer) 
        {
            _deliverer = deliverer;
            _volumeContainer = volumeContainer;
            _deliverer.DeliveredTo += OnProjectileDelivered;
        }

        private void OnProjectileDelivered(GameObject arg0)
        {
            RaycastHit[] hits = Physics.SphereCastAll(
                transform.position, 
                _volumeContainer.AvailableVolume * _volumeInfectionRadiusRatio, 
                Vector3.up);

            foreach (var hit in hits)
            {
                Component infectableObject = hit.collider.GetComponent(typeof(IInfectable));
                if (infectableObject != null)
                    ((IInfectable)infectableObject).Infect();
            }
        }
    }
}
