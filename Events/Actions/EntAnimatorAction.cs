using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ent2D.Events.Actions {
    [System.Serializable]
    public class EntAnimatorAction : EntAction {
        public string State;
        public bool Forced = false;

        public EntAnimatorAction() {}
        public EntAnimatorAction(string state) {
            State = state;
        }
    }
}
