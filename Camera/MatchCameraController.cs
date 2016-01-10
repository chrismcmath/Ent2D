using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Utils;

namespace Ent2D.Match {
    public class MatchCameraController : MatchComponent {
        public float Padding = 2f;
        public float Dampening = 0.1f;

        private float _Distance = 0f;
        public float Distance {
            get { return _Distance; }
        }

        private UnityEngine.Camera _Camera;
        private List<Vector2> _PointsOfInterest = new List<Vector2>();
        private Bounds _DebugBounds;

        protected override void Init() {
            base.Init();
            _Camera = GetComponent<UnityEngine.Camera>();

            if (_Camera == null) {
                Debug.LogError("Could not find camera");
            }
        }

        public void Update() {
            GetPointsOfInterest();
        }

        private void GetPointsOfInterest() {
            _PointsOfInterest = new List<Vector2>();

            foreach (EntController cont in _Controller.Avatars) {
                _PointsOfInterest.Add(BoundPoint(cont.transform.position));
            }

            Bounds poiBounds = GetBoundsFromPointsOfInterest();
            _DebugBounds = poiBounds;

            _Distance = GetDistance(poiBounds);
            Vector3 targetPos = new Vector3(
                    poiBounds.center.x,
                    poiBounds.center.y,
                    -1f * _Distance);

            Vector3 tweenedPos;
            if (Dampening > 0f) {
                tweenedPos = _Camera.transform.position + ((targetPos - _Camera.transform.position) * (Time.deltaTime / Dampening));
            } else {
                tweenedPos = targetPos;
            }
            _Camera.transform.position = tweenedPos;
        }

        private float GetDistance(Bounds poiBounds) {
            float height = poiBounds.size.y;
            float heightFromWidth = poiBounds.size.x / _Camera.aspect;
            float frustumHeight = Mathf.Max(height, heightFromWidth);

            //http://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
            float distance = frustumHeight * 0.5f / Mathf.Tan(_Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

            return distance;
        }

        private Vector2 BoundPoint(Vector2 position) {
            Bounds mapBounds = _Controller.Map.Bounds;

            return new Vector2(
                    Mathf.Min(mapBounds.max.x, Mathf.Max(mapBounds.min.x, position.x)),
                    Mathf.Min(mapBounds.max.y, Mathf.Max(mapBounds.min.y, position.y)));
        }

        private Bounds GetBoundsFromPointsOfInterest() {
            Vector2 min = Vector2.zero;
            Vector2 max = Vector2.zero;

            bool isFirst = true;
            foreach (Vector2 poi in _PointsOfInterest) {
                if (isFirst) {
                    min = poi;
                    max = poi;
                    isFirst = false;
                }
                min = new Vector2(
                        Mathf.Min(min.x, poi.x - Padding),
                        Mathf.Min(min.y, poi.y - Padding));
                max = new Vector2(
                        Mathf.Max(max.x, poi.x + Padding),
                        Mathf.Max(max.y, poi.y + Padding));
            }
            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }

        public void OnDrawGizmos() {
            foreach (Vector2 poi in _PointsOfInterest) {
                Gizmos.color = Color.green;
                DebugUtils.DrawBounds(new Bounds(poi, Vector2.one));
            }

            Gizmos.color = Color.yellow;
            DebugUtils.DrawBounds(_DebugBounds);
        }
    }
}
