using AltaGamesTest.Gameplay;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Effects
{
    public class QuadSizeUpdater : MonoBehaviour
    {
        [SerializeField] private float _widthVolumeRatio = 0.5f;

        private IVolumeContainer _volumeContainer;

        [Inject]
        public void Construct(IVolumeContainer volumeContainer)
        {
            _volumeContainer = volumeContainer;
            UpdateScale(volumeContainer);
            _volumeContainer.VolumeChanged += UpdateScale;
        }

        private void UpdateScale(IVolumeContainer volumeContainer)
        {
            Vector3 scale = transform.localScale;
            scale.x = volumeContainer.AvailableVolume * _widthVolumeRatio;
            transform.localScale = scale;
        }
    }
}
