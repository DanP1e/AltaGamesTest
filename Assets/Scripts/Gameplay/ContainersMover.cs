using AltaGamesTest.Interactions;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Gameplay
{
    public class ContainersMover : StopableMonoBehaviour, IContainersMover
    {
        [SerializeField] private float _movementSpeed;

        private IInfectableObstaclesChecker _obstaclesChecker;
        private IGameStepDetector _gameStepDetector;
        private bool _isMoving = false;

        public bool IsMoving { get => _isMoving && enabled; }

        [Inject]
        public void Construct(
            IInfectableObstaclesChecker obstaclesChecker,
            IGameStepDetector gameStepDetector)
        {
            _obstaclesChecker = obstaclesChecker;
            _gameStepDetector = gameStepDetector;
            _gameStepDetector.StepDone += OnGameStepDone;
        }

        private void OnGameStepDone()
        {
            StopAllCoroutines();
            if (gameObject.scene.isLoaded)
                StartCoroutine(StartMovingProcess());
        }

        protected void OnEnable()
        {
            if (_isMoving)
                StartCoroutine(StartMovingProcess());
        }

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        protected void OnDestroy()
        {
            _gameStepDetector.StepDone -= OnGameStepDone;
        }

        private IEnumerator StartMovingProcess()
        {
            _isMoving = true;
            while (!_obstaclesChecker.IsHasObstacleForward()
                    && enabled == true)
            {
                transform.Translate(transform.forward * _movementSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _isMoving = false;
        }
    }
}
