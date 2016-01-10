using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Utils;

namespace Ent2D {
    public class EntControllerHUD : MonoBehaviour {
        private EntController _Controller;

        public void Awake() {
            _Controller = GetComponent<EntController>();
        }
        public void Update() {
            if (_Controller != null) {
                DebugUtils.ForDebug(transform.position, EntUtils.GetForwardVector(_Controller));
            }
        }

        public void OnDrawGizmos() {
            if (_Controller != null) {
                DebugUtils.ForGizmo(transform.position, EntUtils.GetForwardVector(_Controller));
            }
        }
    }
}
