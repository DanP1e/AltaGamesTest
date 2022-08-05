using UnityEngine;
using UnityEngine.SceneManagement;

namespace AltaGamesTest.Scenes
{
    public class SceneLoader : MonoBehaviour 
    {
        [SerializeField] private string _sceneName;

        public void LoadScene() 
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
