using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IDestroyable
    {
        public event UnityAction<IDestroyable> Destroyed;

        public void Destroy();
    }
}
