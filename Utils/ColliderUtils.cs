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

        public static bool IsIntersecting(List<Collider2D> aCols, List<Collider2D> bCols) {
            foreach (Collider2D aCol in aCols) {
                if (IsIntersecting(aCol, bCols)) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsIntersecting(Collider2D aCol, List<Collider2D> bCols) {
            foreach (Collider2D bCol in bCols) {
                if (IsIntersecting(aCol, bCol)) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsIntersecting(Collider2D aCol, Collider2D bCol) {
            return aCol.IsTouching(bCol);
        }

        public static List<Collider2D> GetIntersectingColliders(List<Collider2D> cols) {
            List<Collider2D> returnColliders = new List<Collider2D>();
            foreach (Collider2D col in cols) {
                returnColliders.AddRange(GetIntersectingColliders(col));
            }
            return returnColliders;
        }

        //NOTE: This seems like a really hacky way to do this
        //      Does unity really not have a collider.Overlap?
        public static List<Collider2D> GetIntersectingColliders(Collider2D col) {
            if (col.GetType() == typeof(CircleCollider2D)) {
                CircleCollider2D cc = (CircleCollider2D) col;
                return new List<Collider2D>(Physics2D.OverlapCircleAll(cc.transform.position, cc.radius));
            }
            return null;
        }
    }
}
