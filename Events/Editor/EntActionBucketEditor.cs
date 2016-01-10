using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace Ent2D.Events {
    [CustomEditor(typeof(EntActionBucket))]
    public class EntActionBucketEditor : Editor {

        public override void OnInspectorGUI() {
            EntActionBucket bucket = (EntActionBucket) target;

            if(GUILayout.Button("Add Action")) {
                bucket.AddAction();
            }
        }
    }
}
