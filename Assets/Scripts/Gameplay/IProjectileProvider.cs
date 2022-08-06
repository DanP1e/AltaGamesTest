using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IProjectileProvider
    {
        public event UnityAction<IProjectile> NewProjectileSpawned;

        public IProjectile GetLastProjectile();

        public void SpawnNewProjectile();
    }
}
