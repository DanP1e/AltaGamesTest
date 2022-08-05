using InspectorAddons;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private InterfaceComponent<IVolumeContainer> _volumeContainerComponent;
        [SerializeField] private InterfaceComponent<ICriticalVolumeAchiever> _criticalVolumeAchieverComponent;

        private IVolumeContainer _volumeContainer;
        private ICriticalVolumeAchiever _criticalVolumeAchiever;

        public IVolumeContainer VolumeContainer => _volumeContainer;

        public ICriticalVolumeAchiever CriticalVolumeAchiever => _criticalVolumeAchiever;

        public event UnityAction<IDestroyable> Destroyed;

        [Inject]
        public void Container()
        {
            _volumeContainer = _volumeContainerComponent.Interface;
            _criticalVolumeAchiever = _criticalVolumeAchieverComponent.Interface;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
