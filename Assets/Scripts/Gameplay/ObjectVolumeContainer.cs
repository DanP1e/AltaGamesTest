using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ObjectVolumeContainer : MonoBehaviour, IVolumeContainer, ICriticalVolumeAchiever
    {
        [SerializeField] private float _criticalVolume = 0.35f;
        [SerializeField] private float _startVolume = 4f;
        [SerializeField] private float _volumeSizeRatio = 0.5f;
        [SerializeField] private Transform _scalingObject;

        private float _avaliableVolume = 0;

        public event UnityAction<ICriticalVolumeAchiever> CriticalVolumeAchieved;

        public bool IsCriticalVolume => _avaliableVolume >= _criticalVolume;

        public float AvailableVolume => _avaliableVolume;

        public float CriticalVolume => _criticalVolume;

        [Inject]
        public void Construct() 
        {
            _avaliableVolume = _startVolume;
            UpdateShape();
        }

        public float ClaimVolume(float expectedPumpingVolume)
        {
            float volumeRemainder = Mathf.Max(0, _avaliableVolume - expectedPumpingVolume);
            float realPumpedVolume = Mathf.Min(volumeRemainder, expectedPumpingVolume);
            _avaliableVolume -= realPumpedVolume;
            CheckIsCriticalVolumeAchived();
            UpdateShape();
            return realPumpedVolume;
        }    

        public void AddVolume(float volume)
        {
            _avaliableVolume += volume;
            CheckIsCriticalVolumeAchived();
            UpdateShape();
        }

        private void CheckIsCriticalVolumeAchived() 
        {
            if (IsCriticalVolume)
                CriticalVolumeAchieved?.Invoke(this);
        }

        private void UpdateShape()
        {
            if (_scalingObject == null)
                return;

            _scalingObject.localScale = Vector3.one * (AvailableVolume * _volumeSizeRatio);
        }
    }
}
