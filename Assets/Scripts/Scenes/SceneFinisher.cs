using AltaGamesTest.Interactions;
using AltaGamesTest.Utilities;
using System;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Scenes
{
    public class SceneFinisher : MonoBehaviour, ISceneFinisher
    {
        [SerializeField] private GameObject _defeatWindow;
        [SerializeField] private GameObject _successWindow;
        
        private bool _isGameFinishing = false;
        private IComponentsProvider<IStopable> _stopableComponentsProvider;

        public bool IsGameFinishing { get => _isGameFinishing; }

        [Inject]
        public void Construct(IComponentsProvider<IStopable> stopableComponentsProvider) 
        {
            _stopableComponentsProvider = stopableComponentsProvider;
        }

        public void FinishWithDefeat()
        {
            CheckIsGameFinishing();
            FinishWithWindow(_defeatWindow);
        }

        public void FinishWithSuccess() 
        {
            CheckIsGameFinishing();
            FinishWithWindow(_successWindow);
        }

        protected void Awake()
        {
            DisableWindows();
        }

        private void FinishWithWindow(GameObject window) 
        {
            DisableWindows();
            window.SetActive(true);
            _isGameFinishing = true;

            foreach (var item in _stopableComponentsProvider.GetComponents())
                item.Stop();
        }

        private void CheckIsGameFinishing()
        {
            if (_isGameFinishing)
                throw new Exception("The scene is already finishing. " +
                    "You can't finish the scene twice!");
        }

        private void DisableWindows()
        {
            _defeatWindow.SetActive(false);
            _successWindow.SetActive(false);
        }
    }
}
