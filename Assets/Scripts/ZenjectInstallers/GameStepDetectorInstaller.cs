using AltaGamesTest.Gameplay;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IGameStepDetector))]
    public class GameStepDetectorInstaller : InterfaceInstaller<IGameStepDetector>
    {
    }
}
