namespace AltaGamesTest.Gameplay
{
    public interface IProjectile : IDestroyable
    {
        public IVolumeContainer VolumeContainer { get; }

        public ICriticalVolumeAchiever CriticalVolumeAchiever { get; }
    }
}
