using UnityEngine;
using Zenject;

namespace AltaGamesTest.ZenjectInstallers
{
    [RequireComponent(typeof(Context))]
    public class ContextInstaller : InterfaceInstaller<Context>
    {
        
    }
}
