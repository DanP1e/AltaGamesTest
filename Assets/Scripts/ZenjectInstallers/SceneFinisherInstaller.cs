using AltaGamesTest.Scenes;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(ISceneFinisher))]
    public class SceneFinisherInstaller : InterfaceInstaller<ISceneFinisher>
    {
    }
}
