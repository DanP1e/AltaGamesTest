using System;
using Zenject;

namespace AltaGamesTest.ZenjectInstallers
{
    public class InterfaceInstaller<T> : MonoInstaller
        where T : class
    {
        public override void InstallBindings()
        {
            T component = GetComponent(typeof(T)) as T;
            if (component == null)
                throw new ArgumentException($"The installer did not " +
                    $"find a component with type \"{typeof(T).Name}\" on object \"{name}\"");

            Container.Bind<T>().FromInstance(component);
        }
    }
}
