using UnityEngine;
using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public interface IDeliverer
    {
        public event UnityAction<GameObject> DeliveredTo;
    }
}
