using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;

namespace Ent2D.Utils {
    public static class EntUtils {
        public enum ColliderType {GROUND=0, SUPPORT, ATTACK, TARGET, CUSTOM}

        //TODO: are we still using this?
        public static EntBehaviour SwitchTo<T>(GameObject go) where T : EntBehaviour {
            EntBehaviour prevBehaviour = go.GetComponent<EntBehaviour>();

            if (prevBehaviour != null) {
                prevBehaviour.OnSwitchOut();
                GameObject.Destroy(prevBehaviour);
            }

            //TODO: refactor
            return go.AddComponent(typeof(T)) as EntBehaviour;
            //return AvatarUtils.AddNewBehaviour(behaviour, go);
        }

        public static EntForm GetFormFromBehaviour(EntBehaviour behaviour, Dictionary<string, EntForm> formCache) {
            string formKey = behaviour.FormKey;
            if (!formCache.ContainsKey(formKey)) {
                string cachedForms = "";
                foreach (string fk in formCache.Keys.ToList()) {
                    cachedForms += string.Format("{0},", fk);
                }
                Debug.LogErrorFormat("Could load form from behaviour {0}, key: {1}, cachedForms: {2}", behaviour, formKey, cachedForms);
                return null;
            }

            return formCache[formKey];
        }

        public static Vector2 GetForwardVector(EntController cont) {
            Vector2 forwardVector = cont.transform.localRotation * Vector2.right;
            Vector2 calcVector = cont.FacingRight() ? forwardVector : -1 * forwardVector;
            return calcVector.normalized;
        }

        public static Vector2 GetUpVector(EntController cont) {
            return cont.transform.localRotation * Vector2.up;
        }

        public static string SanitiseEventKey(string key) {
            return key.Trim().ToUpper();
        }

        // For up thrust
        /*
        public static Vector2 GetForwardVector(EntController cont) {
            Vector2 forwardVector = cont.transform.localRotation * Vector2.up;
            return forwardVector;
        }
        */
    }
}
