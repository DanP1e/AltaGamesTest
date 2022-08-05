using AltaGamesTest.Gameplay;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IProjectileProvider))]
    public class ProjectileProviderInstaller : InterfaceInstaller<IProjectileProvider>
    {

    }
}
