using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Utils;

namespace Ent2D.Conflict {
    public class BattleConflictAgitator {
        public EntController Controller;
        public EntBehaviour Behaviour;
        public EntUtils.ColliderType ColliderType;

        public BattleConflictAgitator(EntController agitator, EntUtils.ColliderType colliderType) {
            Controller = agitator;
            Behaviour = agitator.CurrentBehaviour;
            ColliderType = colliderType;
        }

        public bool Equals(BattleConflictAgitator other) {
            return Controller == other.Controller;
        }

        public static bool Equal(BattleConflictAgitator bca1, BattleConflictAgitator bca2) {
            if (OnlyOneNull((object) bca1, (object) bca2)) {
                return false;
            } else if (BothNull((object) bca1, (object) bca2)) {
                return true;
            }

            return bca1.Controller == bca2.Controller;
        }

        public static bool OnlyOneNull(object a, object b) {
            return (a == null && b != null) || (a != null && b == null);
        }

        public static bool BothNull(object a, object b) {
            return (a == null && b == null);
        }

        public static bool operator == (BattleConflictAgitator bc1, BattleConflictAgitator bc2) {
            return Equal(bc1, bc2);
        }

        public static bool operator != (BattleConflictAgitator bc1, BattleConflictAgitator bc2) {
            return !Equal(bc1, bc2);
        }
    }
}
