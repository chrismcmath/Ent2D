using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;

namespace Ent2D.Events.Listeners {
    public class EntGameObjectListener : EntListener {
        public override Type GetActionType() {
            return typeof(EntGameObjectAction);
        }

        protected override void OnAction(EntAction action) {
            EntGameObjectAction goAction = (EntGameObjectAction) action;
            gameObject.SetActive(goAction.Active);
        }
    }
}
