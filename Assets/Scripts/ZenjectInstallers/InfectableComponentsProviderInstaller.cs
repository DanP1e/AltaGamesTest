using AltaGamesTest.Gameplay;
using AltaGamesTest.Utilities;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IComponentsProvider<IInfectable>))]
    public class InfectableComponentsProviderInstaller 
        : InterfaceInstaller<IComponentsProvider<IInfectable>>
    {
    }
}
