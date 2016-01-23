using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ent2D.Events.Actions {
    [System.Serializable]
    public class EntGameObjectAction : EntAction {
        public bool Active;

        public EntGameObjectAction() {}
        public EntGameObjectAction(bool active) {
            Active = active;
        }
    }
}
