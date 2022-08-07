using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IInfectableObstaclesChecker
    {
        event UnityAction NewObstacleDetected;

        bool IsHasObstacleForward();
    }
}