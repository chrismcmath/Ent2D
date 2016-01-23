using System;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Config;
using Ent2D.Conflict;
using Ent2D.Utils;

namespace Ent2D {
    public abstract class EntBehaviour : MonoBehaviour {
        private EntConfig _Config;
        public EntConfig Config {
            get {
                if (_Config == null) {
                    _Config = Controller.Config;
                }
                return _Config;
            }
        }

        private EntController _Controller;
        public EntController Controller {
            get {
                if (_Controller == null) {
                    _Controller = GetComponent<EntController>();
                }
                return _Controller;
            }
        }

        private Rigidbody2D _Rigidbody;
        public Rigidbody2D Rigidbody {
            get {
                if (_Rigidbody == null) {
                    _Rigidbody = GetComponent<Rigidbody2D>();
                }
                return _Rigidbody;
            }
        }

        protected float _TimeActive = 0f;
        public float TimeActive {
            get { return _TimeActive; }
        }

        protected bool _SwitchedOut = false;
        protected List<Type> _RequiredComponents = new List<Type>();

        //TODO: Not sure I like this
        private bool _Initted = false;

        public virtual int ConflictIndex {
            get { return 0; }
        }

        public virtual string FormKey {
            get { 
                Debug.LogErrorFormat("[Behaviour] FormKey not set.");
                return "";
            }
        }

        public virtual void OnWinConflict(BattleConflictResolution r) {}
        public virtual void OnLoseConflict(BattleConflictResolution r) {}
        public virtual void OnDrawConflict(BattleConflictResolution r) {}

        public virtual void OnSwitchOut() {}
        protected virtual void OnSwitchIn() {}

        protected virtual void UpdateRigidbody() {}

        public void Start() {
            AppendRequiredComponents(_RequiredComponents);
            OnSwitchIn();
        }

        public virtual void Init() {
            if (SetupError()) {
                return;
            }

            _Initted = true;
        }

        public virtual void UpdateBehaviour() {
            if (!IsSetup()) return;

            _TimeActive += Time.deltaTime;
        }

        public virtual void FixedUpdateBehaviour() {
            if (!IsSetup()) return;

            if (!_SwitchedOut) {
                UpdateRigidbody();
            }
        }

        public virtual bool ResolveConflictTie(EntBehaviour a, EntBehaviour b) {
            return false;
        }

        public void FireEvent(string evtKey) {
            if (Controller != null) {
                Controller.FireEvent(evtKey);
            } else {
                Debug.LogErrorFormat("[EntBehaviour] Controller not setup in {0}", this.GetType());
            }
        }

        protected virtual void AppendRequiredComponents(List<Type> requiredComponents) {
            requiredComponents.Add(typeof(EntController));
            requiredComponents.Add(typeof(Rigidbody2D));
        }

        protected void SwitchBehaviour<T>() where T : EntBehaviour {
            if (Controller != null) {
                Controller.SwitchBehaviour<T>(); 
                _SwitchedOut = true;
            } else {
                Debug.LogErrorFormat("[EntBehaviour] Controller not setup in {0}", this.GetType());
            }
        }

        protected bool SetupError() {
            foreach (Type type in _RequiredComponents) {
                if (GetComponent(type) == null) {
                    Debug.LogErrorFormat("[Ent2D] {0} requires type {1}, update the prefab.",
                            gameObject.name, type.ToString());
                    return true;
                }
            }
            return false;
        }

        private bool IsSetup() {
            return _Initted && !SetupError();
        }
    }
}
