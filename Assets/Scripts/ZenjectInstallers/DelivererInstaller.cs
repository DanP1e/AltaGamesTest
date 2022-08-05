using AltaGamesTest.Gameplay;
using UnityEngine;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(IDeliverer))]
    public class DelivererInstaller : InterfaceInstaller<IDeliverer>
    {
    }
}
