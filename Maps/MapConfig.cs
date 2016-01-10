using UnityEngine;

using Ent2D.Utils;

namespace Ent2D.Maps {
    public class MapConfig : MonoBehaviour {
        public Bounds Bounds;
        public Bounds DeadZone;

        public void OnDrawGizmos() {
            Gizmos.color = Color.white;
            DebugUtils.DrawBounds(Bounds);

            Gizmos.color = Color.red;
            DebugUtils.DrawBounds(DeadZone);
        }

        public bool OutOfBounds(Vector2 pos) {
            return
                pos.x < DeadZone.min.x ||
                pos.x > DeadZone.max.x ||
                pos.y < DeadZone.min.y ||
                pos.y > DeadZone.max.y;
        }
    }
}
