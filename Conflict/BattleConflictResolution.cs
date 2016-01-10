using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Utils;

namespace Ent2D.Conflict {
    public class BattleConflictResolution {
        public enum Result {LOSE=0, DRAW, WIN}

        private BattleConflictAgitator _Agitator;
        public BattleConflictAgitator Agitator {
            get { return _Agitator; }
        }

        private Result _Result;
        public Result ConflictResult {
            get { return _Result; }
        }

        private BattleConflict _Conflict;
        public BattleConflict Conflict {
            get { return _Conflict; }
        }

        public BattleConflictResolution(BattleConflictAgitator agitator, Result result, BattleConflict conflict) {
            _Agitator = agitator;
            _Result = result;
            _Conflict = conflict;
        }

        public void Resolve() {
            switch (_Result) {
                case Result.WIN:
                    Win();
                    break;
                case Result.LOSE:
                    Lose();
                    break;
                case Result.DRAW:
                    Draw();
                    break;
            }
        }

        public void Print() {
            return;
            Debug.LogFormat("[CONFLICT] {0}, {1}",
                    _Agitator, _Result);
            Debug.LogFormat("[CONFLICT] <color=#000ff0ff>----------</color> {0}{1}{2} ({3}{4}{5})",
                    ConflictClinic.LOG_CONTROLLER_PREFIX, _Agitator , ConflictClinic.LOG_CONTROLLER_SUFFIX,
                    ConflictClinic.LOG_BEHAVIOUR_PREFIX, _Agitator.Controller, ConflictClinic.LOG_BEHAVIOUR_SUFFIX
                    );
        }

        private void Win() {
            Debug.Log("------ winner: + " + _Agitator.Controller.name + " ------");
            _Agitator.Behaviour.OnWinConflict(this);
        }
        private void Lose() {
            Debug.Log("------ loser: + " + _Agitator.Controller.name + " ------");
            _Agitator.Behaviour.OnLoseConflict(this);
        }

        private void Draw() {
            Debug.Log("------ stalemate: + " + _Agitator.Controller.name + " ------");
            _Agitator.Behaviour.OnDrawConflict(this);
        }
    }
}
