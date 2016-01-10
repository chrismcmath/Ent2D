using UnityEngine;
using System.Collections;

using Ent2D.Camera;
using Ent2D.Utils;

namespace Ent2D.Camera {
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class EntGrid : MonoBehaviour {
        public Vector2 GridSize = Vector2.one;
        public Vector2 Offset = Vector2.zero;
        public Color HorizontalLineColor;
        public Color VerticalLineColor;

        private UnityEngine.Camera _Camera;
        private BattleCameraController _Controller;

        public void Awake() {
            Setup();
        }

        public void OnDrawGizmos() {
            Setup();

            float frustumHeight = 2f * Mathf.Abs(transform.position.z) * Mathf.Tan(_Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * _Camera.aspect;

            Bounds gridBounds = new Bounds(
                    _Camera.transform.position,
                    new Vector3(frustumWidth, frustumHeight, 0f));

            DebugUtils.DrawBounds(gridBounds);

            Vector2 naturalOffset = new Vector2(GridSize.x / 2f, GridSize.y / 2f);
            naturalOffset += Offset;
            naturalOffset = new Vector2(naturalOffset.x % GridSize.x, naturalOffset.y % GridSize.y);

            Vector2 center = new Vector2(
                    Mathf.Floor(gridBounds.center.x / Mathf.Max(GridSize.x, 0.00001f)),
                    Mathf.Floor(gridBounds.center.y / Mathf.Max(GridSize.y, 0.00001f)))
                + naturalOffset;

            Gizmos.color = HorizontalLineColor;
            float x = Mathf.Ceil((gridBounds.size.x * -0.5f) / GridSize.x);
            for (; x <= (gridBounds.size.x / 2f); x += GridSize.x) {
                DrawHorizontalLine(center.x + x,
                        gridBounds.min.y,
                        gridBounds.max.y);
            }

            Gizmos.color = VerticalLineColor;
            float y = Mathf.Ceil((gridBounds.size.y * -0.5f) / GridSize.y);
            for (; y <= (gridBounds.size.y / 2f); y += GridSize.y) {
                DrawVerticalLine(gridBounds.min.x,
                        gridBounds.max.x,
                         center.y + y);
            }
        }

        private void Setup() {
            if (_Camera == null) {
                _Camera = GetComponent<UnityEngine.Camera>();
            }
            if (_Controller == null) {
                _Controller = GetComponent<BattleCameraController>();
            }
        }

        private void DrawHorizontalLine(float x, float startY, float endY) {
            DebugUtils.DrawLine(
                    new Vector2(x, startY),
                    new Vector2(x, endY)
                    );
        }

        private void DrawVerticalLine(float startX, float endX, float y) {
            DebugUtils.DrawLine(
                    new Vector2(startX, y),
                    new Vector2(endX, y)
                    );
        }
    }
}
