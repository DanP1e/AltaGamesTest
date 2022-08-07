using UnityEngine;
using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public class DummyInfectedObstacle : MonoBehaviour, IInfectable
    {
        public bool IsInfected => false;

        public event UnityAction<IInfectable> Infected;
        public event UnityAction<IDestroyable> Destroyed;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Infect()
        {
            
        }

        protected void OnDestroy()
        {
            if(gameObject.scene.isLoaded)
                Destroyed?.Invoke(this);
        }
    }
}
