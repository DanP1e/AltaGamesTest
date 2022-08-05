using AltaGamesTest.Gameplay;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(ICriticalVolumeAchiever))]
    public class CriticalVolumeContainerInstaller : InterfaceInstaller<ICriticalVolumeAchiever>
    {
    }
}
