using AltaGamesTest.Interactions;
using AltaGamesTest.Utilities;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IComponentsProvider<IStopable>))]
    public class StopableComponentsProviderInstaller : InterfaceInstaller<IComponentsProvider<IStopable>>
    {
    }
}
