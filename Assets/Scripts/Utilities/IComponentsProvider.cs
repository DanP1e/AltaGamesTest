using System.Collections.Generic;

namespace AltaGamesTest.Utilities
{
    public interface IComponentsProvider<T>
    {
        public List<T> GetComponents();
    }
}
