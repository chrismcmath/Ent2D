using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace Ent2D.Events.Listeners {
    [CustomEditor(typeof(EntListener), true)]
    public class EntListenerEditor : Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EntListener listener = (EntListener) target;
            listener.EnsureModel();
        }
    }
}
