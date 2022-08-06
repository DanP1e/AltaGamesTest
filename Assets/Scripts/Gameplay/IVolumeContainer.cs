using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IVolumeContainer
    {
        public event UnityAction<IVolumeContainer> VolumeChanged;

        public float AvailableVolume { get; }

        public float ClaimVolume(float expectedClaimingVolume);

        public void AddVolume(float volume);
    }
}
