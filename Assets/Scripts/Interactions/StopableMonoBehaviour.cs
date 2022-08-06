using UnityEngine;

namespace AltaGamesTest.Interactions
{
    public class StopableMonoBehaviour : MonoBehaviour, IStopable
    {
        public void Resume()
            => enabled = true;

        public void Stop()
            => enabled = false;
    }
}
