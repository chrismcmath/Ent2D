using UnityEngine;
using System.Collections;

using Ent2D.ViewControllers;
using Ent2D.Config;
using Ent2D.Conflict;
using Ent2D.Utils;

namespace Ent2D {
    public abstract class EntBehaviour : MonoBehaviour {
        protected EntConfig _Config;
        protected EntController _Controller;
        protected Rigidbody2D _Rigidbody;

        protected bool _SwitchedOut = false;
        private bool _Initted = false;

        public virtual int ConflictIndex {
            get { return 0; }
        }

        protected float _TimeActive = 0f;
        public float TimeActive {
            get { return _TimeActive; }
        }

        public virtual void OnSwitchOut() {}
        public virtual void OnWinConflict(BattleConflictResolution r) {}
        public virtual void OnLoseConflict(BattleConflictResolution r) {}
        public virtual void OnDrawConflict(BattleConflictResolution r) {}

        protected abstract void UpdateContinuousInput();
        protected abstract void UpdateRigidbody();

        protected virtual void OnAction1Down() {}
        protected virtual void OnAction1Up() {}
        protected virtual void OnAction2Down() {}
        protected virtual void OnAction2Up() {}

        protected virtual void OnSwitchIn() {}

        public void Start() {
            Init();
            OnSwitchIn();
        }

        public virtual void UpdateBehaviour() {
            if (SetupError()) {
                return;
            }

            _TimeActive += Time.deltaTime;

            UpdateInput();
        }

        public void FixedUpdateBehaviour() {
            if (!IsSetup()) {
                return;
            }

            if (!_SwitchedOut) {
                UpdateRigidbody();
            }

            if (!_SwitchedOut) {
                UpdateContinuousInput();
            }
        }

        public virtual bool ResolveConflictTie(EntBehaviour a, EntBehaviour b) {
            return false;
        }

        protected virtual void Init() {
            if (SetupError()) {
                return;
            }

            _Initted = true;
        }

        protected void FireEvent(string evtKey) {
            if (_Controller != null) {
                _Controller.FireEvent(evtKey);
            } else {
                Debug.LogErrorFormat("[EntBehaviour] Controller not setup in {0}", this.GetType());
            }
        }

        protected void SwitchBehaviour<T>() where T : EntBehaviour {
            if (_Controller != null) {
                _Controller.SwitchBehaviour<T>(); 
                _SwitchedOut = true;
            } else {
                Debug.LogErrorFormat("[EntBehaviour] Controller not setup in {0}", this.GetType());
            }
        }

        protected bool SetupError() {
            bool hasError = !HasRigidbody() || !HasConfig() || !HasController();
            if (hasError) {
                Debug.LogErrorFormat(@"[EntBehaviour] Could not load necessary controllers
                        HasRigidbody: {0},
                        HasConfig: {1},
                        HasController: {2}",
                        HasRigidbody(), HasConfig(), HasController());

            }
            return hasError;
        }

        protected bool HasConfig() {
            if (_Config != null) {
                return true;
            }

            return TryGetConfig();
        }

        protected bool TryGetConfig() {
            _Config = GetComponentInChildren<EntConfig>();
            if (_Config == null) {
                Debug.LogError(string.Format("Ent {0} does not have an EntConfig attached", gameObject.name));
                return false;
            }
            return true;
        }

        protected bool HasRigidbody() {
            if (_Rigidbody != null) {
                return true;
            }

            return TryGetRigidbody();
        }

        protected bool TryGetRigidbody() {
            _Rigidbody = GetComponent<Rigidbody2D>();
            return _Rigidbody != null;
        }

        protected bool HasController() {
            if (_Controller != null) {
                return true;
            }

            return TryGetController();
        }

        protected bool TryGetController() {
            _Controller = GetComponent<EntController>();
            return _Controller != null;
        }

        private bool IsSetup() {
            return _Initted && !SetupError();
        }

        private void UpdateInput() {
            if (ControllerUtils.Action1Down(_Controller.PlayerNumber)) {
                OnAction1Down();
            } else if (ControllerUtils.Action1Up(_Controller.PlayerNumber)) {
                OnAction1Up();
            } else if (ControllerUtils.Action2Down(_Controller.PlayerNumber)) {
                OnAction2Down();
            } else if (ControllerUtils.Action2Up(_Controller.PlayerNumber)) {
                OnAction2Up();
            }
        }
    }
}
