using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AltaGamesTest.Utilities
{
    public static class ComponentsFinder
    {
        public static List<T> Find<T>() 
            => MonoBehaviour.FindObjectsOfType<Object>().OfType<T>().ToList();
    }
}
