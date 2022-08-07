using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IGameStepDetector
    {
        bool IsStepInProcess { get; }

        event UnityAction StepDone;
    }
}