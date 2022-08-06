using System.Collections;
using UnityEngine;

namespace AltaGamesTest.Utilities
{
    public class ObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private float _destroyDelay = 0.35f;

        protected void OnEnable()
        {
            StopAllCoroutines();    
            StartCoroutine(StartDestroyProcess());
        }

        private IEnumerator StartDestroyProcess() 
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }
    }
}
