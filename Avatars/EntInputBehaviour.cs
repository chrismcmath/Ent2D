using UnityEngine;
using System;
using System.Collections.Generic;

using Ent2D.Conflict;
using Ent2D.Utils;

namespace Ent2D {
    public abstract class EntInputBehaviour : EntBehaviour {
        private EntPlayableController _PlayableController;
        public EntPlayableController PlayableController {
            get {
                if (_PlayableController == null) {
                    _PlayableController = GetComponent<EntPlayableController>();
                }
                return _PlayableController;
            }
        }

        protected virtual void OnAction1Down() {}
        protected virtual void OnAction1Up() {}
        protected virtual void OnAction2Down() {}
        protected virtual void OnAction2Up() {}
        protected virtual void UpdateContinuousInput() {}

        public override void UpdateBehaviour() {
            base.UpdateBehaviour();

            UpdateInput();
        }

        public override void FixedUpdateBehaviour() {
            base.FixedUpdateBehaviour();

            if (!_SwitchedOut) {
                UpdateContinuousInput();
            }
        }

        protected override void AppendRequiredComponents(List<Type> requiredComponents) {
            base.AppendRequiredComponents(requiredComponents);

            requiredComponents.Add(typeof(EntPlayableController));
        }

        private void UpdateInput() {
            if (ControllerUtils.Action1Down(PlayableController.PlayerNumber)) {
                OnAction1Down();
            } else if (ControllerUtils.Action1Up(PlayableController.PlayerNumber)) {
                OnAction1Up();
            } else if (ControllerUtils.Action2Down(PlayableController.PlayerNumber)) {
                OnAction2Down();
            } else if (ControllerUtils.Action2Up(PlayableController.PlayerNumber)) {
                OnAction2Up();
            }
        }
    }
}
