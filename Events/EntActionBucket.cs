using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;
using Ent2D.Events.Listeners;
using Ent2D.Utils;

namespace Ent2D.Events {
    public class EntActionBucket : MonoBehaviour {
        public string EventKey;

        private List<EntAction> _Actions = new List<EntAction>();
        public List<EntAction> Actions {
            get { return _Actions; }
        }

        private EntListener _Listener;
        public EntListener Listener {
            get {
                if (_Listener == null) {
                    _Listener = GetComponentInParent<EntListener>();
                }
                return _Listener;
            }
        }

        public void AddAction() {
            Transform childT = UnityUtils.CreateEmptyChild("Action", transform, true);
            childT.gameObject.AddComponent(Listener.GetActionType());
        }

        public void Load() {
            _Actions = new List<EntAction>(GetComponentsInChildren<EntAction>());
        }
    }
}
