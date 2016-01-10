using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Utils;

namespace Ent2D.Conflict {
    public class BattleConflictPair {
        public BattleConflictAgitator First;
        public BattleConflictAgitator Second;

        public BattleConflictPair(BattleConflictAgitator first, BattleConflictAgitator second) {
            First = first;
            Second = second;
        }
    }
}
