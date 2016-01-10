using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;
using Ent2D.Utils;

namespace Ent2D.Events.Listeners {
    public abstract class EntListener : MonoBehaviour {
        [SerializeField]
        private EntActionModel _ActionModel;

        public void Start() {
            CheckModel();
            _ActionModel.Load();
        }

        public void CheckModel() {
            if (_ActionModel == null) {
                _ActionModel = GetComponentInChildren<EntActionModel>();
            }
            if (_ActionModel == null) {
                CreateActionModel();
            }
        }

        public void OnEvent(string evtKey) {
            string santisedKey = EntUtils.SanitiseEventKey(evtKey);
            if (_ActionModel.ContainsKey(santisedKey)) {
                foreach (EntAction action in _ActionModel[santisedKey]) {
                    OnAction(action);
                }
            }
        }

        public abstract Type GetActionType();
        protected abstract void OnAction(EntAction action);

        private void CreateActionModel() {
            if (_ActionModel != null) return;

            Transform childT = UnityUtils.CreateEmptyChild(
                    string.Format("{0} Actions", name), transform, true);
            _ActionModel = childT.gameObject.AddComponent<EntActionModel>();
        }
    }
}
