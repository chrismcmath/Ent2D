using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using Ent2D.Utils;

namespace Ent2D.Events {
    [CustomEditor(typeof(EntActionModel))]
    public class EntActionModelEditor : Editor {

        public string NewBucketName = "";

        public override void OnInspectorGUI() {
            EntActionModel actionModel = (EntActionModel) target;

            NewBucketName = EntUtils.SanitiseEventKey(
                    EditorGUILayout.TextField("Event Key", NewBucketName));
            if(GUILayout.Button("Add Bucket")) {
                actionModel.AddBucket(NewBucketName);
            }
        }
    }
}
