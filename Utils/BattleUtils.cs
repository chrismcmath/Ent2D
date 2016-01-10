using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D;
using Ent2D.Conflict;

namespace Ent2D.Utils {
    public static class BattleUtils {
        public static bool Overpowers(BattleConflictAgitator a, BattleConflictAgitator b) {
            if (Overpowers(a.Behaviour, b.Behaviour)) {
                return true;
            } else if (a.Behaviour.GetType() == b.Behaviour.GetType()) {
                return a.Behaviour.ResolveConflictTie(a.Behaviour, b.Behaviour);
            }
            return false;
        }

        public static bool Overpowers(EntBehaviour a, EntBehaviour b) {
            return a.ConflictIndex > b.ConflictIndex;
        }
    }
}
