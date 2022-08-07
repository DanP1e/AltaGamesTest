using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        private IVolumeContainer _volumeContainer;
        private ICriticalVolumeAchiever _criticalVolumeAchiever;

        public IVolumeContainer VolumeContainer => _volumeContainer;

        public ICriticalVolumeAchiever CriticalVolumeAchiever => _criticalVolumeAchiever;

        public event UnityAction<IDestroyable> Destroyed;

        [Inject]
        public void Container(
            IVolumeContainer volumeContainer, 
            ICriticalVolumeAchiever criticalVolumeAchiever)
        {
            _volumeContainer = volumeContainer;
            _criticalVolumeAchiever = criticalVolumeAchiever;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            if(gameObject.scene.isLoaded)
                Destroyed?.Invoke(this);
        }
    }
}
