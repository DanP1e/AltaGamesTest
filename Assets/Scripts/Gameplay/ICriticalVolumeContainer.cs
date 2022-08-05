using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface  ICriticalVolumeAchiever
    {
        public event UnityAction<ICriticalVolumeAchiever> CriticalVolumeAchieved;

        public float CriticalVolume { get; }

        public bool IsCriticalVolume { get; }
    }
}
