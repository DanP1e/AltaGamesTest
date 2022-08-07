using AltaGamesTest.Scenes;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class SuccessChecker : MonoBehaviour
    {
        private ISceneFinisher _sceneFinisher;
        private IVolumeContainer _mainContainer;

        [Inject]
        public void Construct(
            ISceneFinisher sceneFinisher,
            IVolumeContainer mainContainer)
        {
            _sceneFinisher = sceneFinisher;
            _mainContainer = mainContainer;
        }

        public void OnTriggerEnter(Collider other)
        {
            IVolumeContainer volumeContainer
                = other.GetComponent(typeof(IVolumeContainer)) as IVolumeContainer;

            if(volumeContainer != null
            && volumeContainer == _mainContainer)
            {
                _sceneFinisher.FinishWithSuccess();
            }
        }
    }
}
