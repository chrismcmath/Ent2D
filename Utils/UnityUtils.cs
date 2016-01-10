using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D;

namespace Ent2D.Utils {
    public static class UnityUtils {
        public static Transform CreateEmptyChild(string name, Transform parent, bool isStatic = false) {
            GameObject go = new GameObject(name);
            Transform t = go.transform;
            t.parent = parent;
            t.localPosition = Vector3.zero;
            t.localScale = Vector3.one;
            t.localRotation = Quaternion.identity;
            t.gameObject.isStatic = isStatic;
            return t;
        }

        public static Transform InstantiatePrefab(string path, Transform parent, string name, Vector3 pos = default(Vector3)) {
            GameObject prefab = Resources.Load(path) as GameObject;
            return InstantiatePrefab(prefab, parent, name, pos);
        }

        public static Transform InstantiatePrefab(GameObject prefab, Transform parent, string name, Vector3 pos = default(Vector3)) {
            GameObject go = GameObject.Instantiate(prefab);
            Transform t = go.transform;
            t.name = name;
            t.parent = parent;
            t.localPosition = pos;
            t.localScale = Vector3.one;
            t.localRotation = Quaternion.identity;
            return t;
        }
    }
}
