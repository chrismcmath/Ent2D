using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D;

namespace Ent2D.Utils {
    public static class ColliderUtils {
        public const string COLLISION_TAG_PLATFORM = "PLATFORM";

        public static bool IsIntersecting(List<Collider2D> cols, string layer) {
            foreach (Collider2D col in cols) {
                if (IsIntersecting(col, layer)) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsIntersecting(Collider2D col, string layer) {
            int layerInt = LayerMask.NameToLayer(layer);
            return col.IsTouchingLayers(1 << layerInt);
        }
    }
}
