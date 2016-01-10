using UnityEngine;
using System.Collections;

namespace Ent2D.Match {
    public class MatchComponent : MonoBehaviour {
        protected MatchController _Controller; 

        public void Awake() {
            Init();
        }

        protected virtual void Init() {
            _Controller = FindObjectOfType(typeof(MatchController)) as MatchController;

            if (_Controller == null) {
                Debug.LogError("Could not find a MatchController in the scene");
            }
        }
    }
}
