using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;

namespace Ent2D.Events.Listeners {
    [RequireComponent(typeof(Animator))]
    public class EntAnimatorListener : EntListener {
		private Animator _Animator;

        public void Awake() {
			_Animator = GetComponentInChildren<Animator>();
			if (_Animator != null) {
                _Animator.enabled = true;
            }
        }

        public override Type GetActionType() {
            return typeof(EntAnimatorAction);
        }

        protected override void OnAction(EntAction action) {
            EntAnimatorAction aniAction = (EntAnimatorAction) action;
            Play(aniAction);
        }

        public void Play(EntAnimatorAction action) {
            Play(action.State, action.Forced);
        }
        public void Play(string state, bool forced) {
            if (_Animator == null) {
                return;
            }

            if (forced || !_Animator.GetCurrentAnimatorStateInfo(0).IsName(state)) {
                //Debug.Log("[Animator] Play " + state);
                _Animator.Play(state);
            } else {
                //Debug.Log("[Animator] REFUSED Play " + state);
            }
        }
    }
}
