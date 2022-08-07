using AltaGamesTest.Gameplay;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IContainersMover))]
    public class ContainersMoverInstaller : InterfaceInstaller<IContainersMover>
    {
    }
}
