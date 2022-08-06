using InspectorAddons;
using System.Collections;
using UnityEngine;

namespace AltaGamesTest.Effects
{
    public class ColorTransitionMaker : MonoBehaviour
    {
        [SerializeField] private Color _fromColor = Color.white;
        [SerializeField] private Color _toColor = Color.black;
        [SerializeField] private AnimationCurve _transitionCurve;
        [SerializeField] private InterfaceComponent<IColorProvider> _colorProviderComponent;

        private Vector4 _fromColorVector;
        private Vector4 _toColorVector;
        private IColorProvider _colorProvider;

        protected void OnEnable()
        {          
            _fromColorVector = ColorToVector(_fromColor);
            _toColorVector = ColorToVector(_toColor);
            _colorProvider = _colorProviderComponent.Interface;

            StopAllCoroutines();
            StartCoroutine(StartTransitionProcess());
        }

        private IEnumerator StartTransitionProcess() 
        {
            float maxTime = _transitionCurve.keys[_transitionCurve.length - 1].time;
            float timer = 0;
            while (true)
            {
                Vector4 newColorVector = Vector4.LerpUnclamped(
                    _fromColorVector, 
                    _toColorVector, 
                    _transitionCurve.Evaluate(timer));

                _colorProvider.Color = VectorToColor(newColorVector);
                timer += Time.deltaTime;

                if(timer >= maxTime) 
                    break;

                yield return new WaitForEndOfFrame();
            }
        }

        private Vector4 ColorToVector(Color color) 
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }

        private Color VectorToColor(Vector4 vector)
        {
            return new Color(vector.x, vector.y, vector.z, vector.w);
        }
    }
}
