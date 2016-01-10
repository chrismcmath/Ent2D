using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D;

namespace Ent2D.Utils {
    public static class VectorUtils {
        public const float RADIANS_TO_DEGREES_RATIO = 57.2957795f;

        /* Broken 
        public static float GetAngle(Vector2 a, Vector2 b) {
            return RadiansToDegrees(Mathf.Atan2(b.y - a.y, b.x - a.x));
        }
        */

        public static bool IsFacingRight(Vector2 v) {
            return Vector2.Dot(v, Vector2.right) >= 0f;
        }

        public static Vector2 GetOrthogonalVector(Vector2 v) {
            return new Vector2(v.y, -1 * v.x);
        }

        public static float RadiansToDegrees(float radians) {
            return radians * RADIANS_TO_DEGREES_RATIO;
        }

        public static float GetPolarizedDotProduct(Vector2 a, Vector2 b) {
            return Vector2.Dot(a.normalized, b.normalized) * GetAnglePolarity(a, b);
        }

        public static float GetInversePolarizedDotProduct(Vector2 a, Vector2 b) {
            return (1f - Vector2.Dot(a.normalized, b.normalized)) * GetAnglePolarity(a, b);
        }

        public static float GetAnglePolarity(Vector2 a, Vector2 b) {
            if (Vector3.Cross(a,b).z < 0f) {
                return -1f;
            }
            return 1f;
        }

        public static float GetAngle(Vector2 a, Vector2 b) {
            float angle = Vector2.Angle(a, b);
            Vector3 cross = Vector3.Cross(a, b);
            if (cross.z < 0f) {
                angle = -angle;
            }
            return angle;
        }
    }
}
