using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IInfectable : IDestroyable
    {
        event UnityAction<IInfectable> Infected;

        bool IsInfected { get; }

        void Infect();
    }
}
