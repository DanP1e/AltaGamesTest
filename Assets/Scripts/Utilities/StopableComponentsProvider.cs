using AltaGamesTest.Interactions;
using System.Collections.Generic;
using UnityEngine;

namespace AltaGamesTest.Utilities
{
    public class StopableComponentsProvider : MonoBehaviour, IComponentsProvider<IStopable>
    {
        public List<IStopable> GetComponents()
            => ComponentsFinder.Find<IStopable>();
    }
}
