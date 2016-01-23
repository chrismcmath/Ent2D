using UnityEngine;
using System.Collections;

using Ent2D.Core;

namespace Ent2D.Match {
    public abstract class MatchComponent : MonoBehaviour {
        protected MatchController _Controller; 
        public MatchController Controller {
            get {
                if (_Controller == null) {
                    _Controller = Global.Instance.Context.GetComponent<MatchController>();
                }
                return _Controller;
            }
        }

        public void Start() {
            Init();
        }

        protected abstract void Init();
    }
}
