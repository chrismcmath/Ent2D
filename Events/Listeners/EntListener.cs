using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;
using Ent2D.Utils;

namespace Ent2D.Events.Listeners {
    public abstract class EntListener : MonoBehaviour {
        private EntActionModel _ActionModel;
        public EntActionModel ActionModel {
            get {
                if (_ActionModel == null) {
                    _ActionModel = gameObject.GetComponentInDirectChildren<EntActionModel>();
                    if (_ActionModel == null) {
                        _ActionModel = CreateActionModel();
                    }
                    _ActionModel.Load();
                }
                return _ActionModel;
            }
        }

        public void EnsureModel() {
            if (gameObject.GetComponentInDirectChildren<EntActionModel>() == null) {
                CreateActionModel();
            }
        }

        public void OnEvent(string evtKey) {
            string santisedKey = EntUtils.SanitiseEventKey(evtKey);
            if (ActionModel.ContainsKey(santisedKey)) {
                foreach (EntAction action in ActionModel[santisedKey]) {
                    OnAction(action);
                }
            }
        }

        public abstract Type GetActionType();
        protected abstract void OnAction(EntAction action);

        private EntActionModel CreateActionModel() {
            Transform childT = UnityUtils.CreateEmptyChild(
                    string.Format("{0} Actions", name), transform, true);
            return childT.gameObject.AddComponent<EntActionModel>();
        }
    }
}
