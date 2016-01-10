using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cockateers.Avatars.Behaviours;
using Cockateers.Avatars.Forms;

using Cockateers.Events;
using Cockateers.Utils;

namespace Ent2D.ViewControllers {
    public class EntAnimationController : MonoBehaviour {
		private Animator _Animator;

        public void Awake() {
			_Animator = GetComponentInChildren<Animator>();
			if (_Animator != null) {
                _Animator.enabled = true;
            }
        }

        /*
        public override void OnEvent(EventUtils.Event evt) {
            //TODO: might need an indirect lookup here,
            //such as iff has animation for this evt, play it (forced or nay)
            //
            if (HasState(evt.ToString())) {
                Play(evt.ToString());
            }
        }
        */

        //TODO: get rid
        protected bool HasState(string state) {
            return true;
        }

        public void Play(string state) {
            Play(state, false);
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
