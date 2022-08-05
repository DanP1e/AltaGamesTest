namespace AltaGamesTest.Gameplay
{
    public interface IVolumeContainer
    {
        public float AvailableVolume { get; }

        public float ClaimVolume(float expectedClaimingVolume);

        public void AddVolume(float volume);
    }
}
