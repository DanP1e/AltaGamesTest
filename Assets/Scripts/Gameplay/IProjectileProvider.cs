namespace AltaGamesTest.Gameplay
{
    public interface IProjectileProvider
    {
        public IProjectile GetLastProjectile();

        public void SpawnNewProjectile();
    }
}
