using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Utils;

namespace Ent2D.Conflict {
    public class BattleConflict {
        public const string LOG_CONTROLLER_PREFIX = "<color=#add8e6ff>";
        public const string LOG_CONTROLLER_SUFFIX = "</color>";
        public const string LOG_BEHAVIOUR_PREFIX = "<color=#008000ff>";
        public const string LOG_BEHAVIOUR_SUFFIX = "</color>";

        private BattleConflictPair _Pair;
        private bool _Resolved = false;

        public BattleConflictAgitator Winner {
            get {
                if (BattleUtils.Overpowers(_Pair.First, _Pair.Second)) {
                    return _Pair.First;
                } else if (BattleUtils.Overpowers(_Pair.Second, _Pair.First)) {
                    return _Pair.Second;
                }
                return null;
            }
        }

        public BattleConflictAgitator Loser {
            get {
                if (BattleUtils.Overpowers(_Pair.First, _Pair.Second)) {
                    return _Pair.Second;
                } else if (BattleUtils.Overpowers(_Pair.Second, _Pair.First)) {
                    return _Pair.First;
                }
                return null;
            }
        }

        public BattleConflict(EntController contA,
                EntController contB,
                EntUtils.ColliderType typeA,
                EntUtils.ColliderType typeB) {
            _Pair = new BattleConflictPair(
                    new BattleConflictAgitator(contA, typeA),
                    new BattleConflictAgitator(contB, typeB));
        }

        public bool HasAgitator(EntController avatCont) {
            return 
                _Pair.First.Controller == avatCont ||
                _Pair.Second.Controller == avatCont;
        }

        public void Print() {
            Debug.LogFormat("[CONFLICT] <color=#0000ffff>----------</color> {0}{1}{2} ({3}{4}{5}) - {6}{7}{8} ({9}{10}{11})",
                    LOG_CONTROLLER_PREFIX, _Pair.First.Controller.name, LOG_CONTROLLER_SUFFIX,
                    LOG_BEHAVIOUR_PREFIX, GetBehaviourName(_Pair.First), LOG_BEHAVIOUR_SUFFIX,
                    LOG_CONTROLLER_PREFIX, _Pair.Second.Controller.name, LOG_CONTROLLER_SUFFIX,
                    LOG_BEHAVIOUR_PREFIX, GetBehaviourName(_Pair.Second), LOG_BEHAVIOUR_SUFFIX
                    );
        }

        public string GetBehaviourName(BattleConflictAgitator a) {
            return a.Behaviour.GetType().Name;
        }

        public bool Equals(BattleConflict other) {
            return (this._Pair.First == other._Pair.First && this._Pair.Second == other._Pair.Second) ||
                (this._Pair.First == other._Pair.Second && this._Pair.Second == other._Pair.First);
        }

        public static bool Equal(BattleConflict bc1, BattleConflict bc2) {
            return bc1.Equals(bc2);
        }

        public static bool operator == (BattleConflict bc1, BattleConflict bc2) {
            return Equal(bc1, bc2);
        }

        public static bool operator != (BattleConflict bc1, BattleConflict bc2) {
            return !Equal(bc1, bc2);
        }
    }
}
