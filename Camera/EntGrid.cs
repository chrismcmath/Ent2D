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
        public Material Mat;

        private float _Width;
        private float _Height;
        private UnityEngine.Camera _Camera;
        private MatchCameraController _Controller;

        public void Awake() {
            if (Mat == null) {
                Mat = new Material(Shader.Find("Diffuse"));
            }
            Setup();
        }

        private void Setup() {
            if (_Camera == null) {
                _Camera = GetComponent<UnityEngine.Camera>();
            }
            if (_Controller == null) {
                _Controller = GetComponent<MatchCameraController>();
            }
        }

        private void OnPostRender() {
            Setup();

            InitGL();
            DrawGrid();
            EndGL();
        }
        
        private void InitGL() {
            GL.PushMatrix();
            Mat.SetPass(0);
            GL.LoadOrtho();
        }

        private void EndGL() {
            GL.PopMatrix();
        }

        private void OnDrawGizmos() {
            Bounds gridBounds = _Camera.orthographic ?
                GetOrthographicGridBounds() : GetPerspectiveGridBounds();

            //DebugUtils.DrawBounds(gridBounds);
        }

        private void DrawGrid() {
            Vector2 naturalOffset = new Vector2(GridSize.x / 2f, GridSize.y / 2f);
            naturalOffset += Offset;
            naturalOffset = new Vector2(naturalOffset.x % GridSize.x, naturalOffset.y % GridSize.y);

            Bounds gridBounds = _Camera.orthographic ?
                GetOrthographicGridBounds() : GetPerspectiveGridBounds();

            Vector2 offset = new Vector2(
                    _Camera.transform.position.x % GridSize.x,
                    _Camera.transform.position.y % GridSize.y);
            offset += naturalOffset;

            GL.Color(HorizontalLineColor);
            float midX = gridBounds.size.x / 2f;
            for (float x = 0f; x <= midX; x += GridSize.x) {
                DrawHorizontalLine((midX + x - offset.x) / gridBounds.size.x, 0, 1);
                DrawHorizontalLine((midX - x - offset.x) / gridBounds.size.x, 0, 1);
            }

            GL.Color(VerticalLineColor);
            float midY = gridBounds.size.y / 2f;
            for (float y = 0f; y <= midY; y += GridSize.y) {
                DrawVerticalLine(0, 1, (midY + y - offset.y) / gridBounds.size.y);
                DrawVerticalLine(0, 1, (midY - y - offset.y) / gridBounds.size.y);
            }
        }

        private Bounds GetOrthographicGridBounds() {
            _Height = 2f * _Camera.orthographicSize;
            _Width = _Height * _Camera.aspect;
            return new Bounds(
                    _Camera.transform.position,
                    new Vector3(_Width, _Height, 0f));
        }

        private Bounds GetPerspectiveGridBounds() {
            _Height = 2f * Mathf.Abs(transform.position.z) * Mathf.Tan(_Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            _Width = _Height * _Camera.aspect;

            return new Bounds(
                    _Camera.transform.position,
                    new Vector3(_Width, _Height, 0f));
        }

        private void DrawHorizontalLine(float x, float startY, float endY) {
            GL.Begin(GL.LINES);
            SetPoint(x, startY);
            SetPoint(x, endY);
            GL.End();
        }

        private void DrawVerticalLine(float startX, float endX, float y) {
            GL.Begin(GL.LINES);
            SetPoint(startX, y);
            SetPoint(endX, y);
            GL.End();
        }

        private void SetPoint(float x, float y) {
            GL.Vertex(new Vector2(x, y));
        }
    }
}
