using UnityEngine;
using UnityEngine.SceneManagement;

namespace AltaGamesTest.Scenes
{
    public class SceneReloader : MonoBehaviour
    {
        public void Reload() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
