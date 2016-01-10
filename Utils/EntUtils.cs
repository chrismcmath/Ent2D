using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D;

//TODO: get rid 
using Cockateers.Utils; 

namespace Ent2D.Utils {
    public static class EntUtils {
        public enum ColliderType {GROUND=0, SUPPORT, ATTACK, TARGET}

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

        public static EntForm LookUpForm<T>(Dictionary<string, EntForm> formCache) where T : EntBehaviour {
            //TODO: refactor
            string formName = AvatarUtils.LookUpFormName<T>();
            if (!formCache.ContainsKey(formName)) {
                Debug.LogErrorFormat("Could load form from behaviour {0}", typeof(T));
                return null;
            }

            return formCache[formName];
        }

        public static Vector2 GetForwardVector(EntController cont) {
            Vector2 forwardVector = cont.transform.localRotation * Vector2.right;
            Vector2 calcVector = cont.FacingRight() ? forwardVector : -1 * forwardVector;
            return calcVector;
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
