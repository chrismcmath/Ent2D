using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Conflict;

namespace Ent2D.Utils {
    public static class CollectionUtils {
        public static bool Contains<T>(List<T> items, T item) where T : class {
            foreach (T i in items) {
                Debug.Log("compare " + i + " with " + item);
                if (i.Equals(item)) {
                    Debug.Log("return true");
                    return true;
                }
            }
            Debug.Log("return false");


            /*
            BattleConflict testA = new BattleConflict();
            BattleConflict testB = new BattleConflict();
            bool crap = testA == testB;

            */

            return false;
        }

        public static bool Contains(List<BattleConflict> items, BattleConflict item) {
            foreach (BattleConflict i in items) {
                if (i == item) {
                    return true;
                }
            }
            return false;
        }
    }
}
