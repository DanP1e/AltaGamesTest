using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AltaGamesTest.Gameplay
{
    public class InfectableObstaclesChecker : MonoBehaviour, IInfectableObstaclesChecker
    {
        [Min(2)]
        [SerializeField] private int _raysCount = 3;
        [Min(0.1f)]
        [SerializeField] private float _raysLength = 5;
        [SerializeField] private MeshRenderer _meshRenderer;

        private bool _isHasObstacleForward = false;

        public event UnityAction NewObstacleDetected;

        private Vector3 RayDirection
            => transform.forward;

        public bool IsHasObstacleForward()
        {
            bool result = false;

            foreach (var i in ThrowRays(GetRaysOrigins()))
            {
                if (i.collider != null)
                {
                    IInfectable infectabelComponent
                        = i.collider.GetComponent(typeof(IInfectable)) as IInfectable;

                    if (infectabelComponent != null 
                    && !infectabelComponent.IsInfected)
                        result = true;

                    break;
                }
            }

            if (_isHasObstacleForward != result 
            || result == true)
                NewObstacleDetected?.Invoke();

            _isHasObstacleForward = result;

            return result;
        }

        protected void OnDrawGizmosSelected()
        {
            if (_meshRenderer == null)
                return;

            Gizmos.color = Color.blue;
            List<Vector3> origins = GetRaysOrigins();
            List<RaycastHit> hits = ThrowRays(origins);

            Vector3 direction = RayDirection;

            for (int i = 0; i < origins.Count; i++)
            {
                Vector3 point = hits[i].point == Vector3.zero
                ? origins[i] + (direction * _raysLength)
                : hits[i].point;

                Gizmos.DrawSphere(point, 0.2f);
                Gizmos.DrawLine(origins[i], point);
            }
        }

        private List<RaycastHit> ThrowRays(List<Vector3> origins)
        {
            List<RaycastHit> result = new List<RaycastHit>();
            Vector3 direction = RayDirection;
            foreach (var origin in origins)
            {
                RaycastHit hit;
                Physics.Raycast(origin, direction, out hit, _raysLength);
                result.Add(hit);
            }

            return result;
        }

        private List<Vector3> GetRaysOrigins()
        {
            List<Vector3> result = new List<Vector3>();

            float width = transform.localScale.x;
            for (int i = 0; i < _raysCount; i++)
            {

                Vector3 pos = _meshRenderer.transform.position;
                float xPos = Mathf.Lerp(
                    _meshRenderer.bounds.min.x,
                    _meshRenderer.bounds.max.x,
                    GetPointFactor(i));

                pos.x = xPos;

                result.Add(pos);
            }

            return result;
        }

        private float GetPointFactor(int i)
        {
            if ((_raysCount - 1) <= 0)
                return 0;

            return (float)i / (float)(_raysCount - 1);
        }
    }
}
