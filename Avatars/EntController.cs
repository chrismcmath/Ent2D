using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Camera;
using Ent2D.Config;
using Ent2D.Conflict;
using Ent2D.Events;
using Ent2D.Events.Listeners;
using Ent2D.Utils;

//TODO: Event Listeners
namespace Ent2D {
    public abstract class EntController : MonoBehaviour {
        public const string CONFIG_RECIPE_PATH_FORMAT = "Avatars/Recipes/{0}";

        //TODO: Improve this system
        public bool Logging = false;
        public string ConfigRecipe = "Chris";
        public EntController Aggressor;

        private EntConfig _Config;
        public EntConfig Config {
            get {
                if (_Config == null) {
                    LoadConfig();
                }
                return _Config;
            }
        }


        private Vector2 _FacingVector = Vector2.right;
        public Vector2 FacingVector {
            get { return _FacingVector; }
            set {
                if (_FacingVector == value || (value != -1 * Vector2.right && value != Vector2.right)) {
                    return;
                }

                if (Logging) {
                    Debug.Log("Changing Facing Vector to " + value);
                }
                _FacingVector = value;

                transform.localScale = new Vector3(
                        _FacingVector.x,
                        transform.localScale.y,
                        transform.localScale.z);
            }
        }

        private EntBehaviour _CurrentBehaviour;
        public EntBehaviour CurrentBehaviour {
            get { return _CurrentBehaviour; }
            set {
                if (_CurrentBehaviour == value) {
                    return;
                }

                if (Logging) {
                    Debug.Log("Changing Current Behaviour to " + value);
                }
                _CurrentBehaviour = value;
            }
        }

        private EntForm _CurrentForm;
        public EntForm CurrentForm {
            get { return _CurrentForm; }
            set {
                if (_CurrentForm == value) {
                    return;
                }

                if (Logging) {
                    Debug.Log("Changing Current Form to " + value);
                }
                _CurrentForm = value;
            }
        }

        protected Dictionary<string, EntForm> _FormCache;
        public Dictionary<string, EntForm> FormCache {
            get {
                if (_FormCache == null) {
                    _FormCache = new Dictionary<string, EntForm>();
                    foreach (EntForm form in GetComponentsInChildren<EntForm>()) {
                        _FormCache.Add(form.name, form);
                        form.gameObject.SetActive(false);
                    }
                }
                return _FormCache;
            }
        }

        private List<EntListener> _Listeners;
        public List<EntListener> Listeners {
            get {
                if (_Listeners == null) {
                    _Listeners = new List<EntListener>();
                    _Listeners.AddRange(GetComponentsInChildren<EntListener>());
                }
                return _Listeners;
            }
        }

        public void Start() {
            LoadInitialBehaviour();

            //TODO: Should be set elsewhere
            GetComponent<Rigidbody2D>().mass = Config.Mass;

            MatchCameraController.GetPointsOfInterest += AppendPosition;
        }

        public void OnDestroy() {
            MatchCameraController.GetPointsOfInterest -= AppendPosition;
        }

        public void Update() {
            if (CurrentBehaviour != null) {
                CurrentBehaviour.UpdateBehaviour();
            }
		}

        public void FixedUpdate() {
            if (CurrentBehaviour != null) {
                CurrentBehaviour.FixedUpdateBehaviour();
            }
        }

        public virtual void OnCustomCollision(EntController otherController) {}
        protected abstract void LoadInitialBehaviour();

        //TODO: Something smarter here
        // Take in a string, cache old objects in a dictionary (use reset instead of awake)
        // Then on ent awake cache all possible behaviours based off strings in codebase
        public void SwitchBehaviour<T>() where T : EntBehaviour {
            if (ConflictClinic.HasConflict(this)) {
                if (Logging) {
                    Debug.Log("[EntController] Preventing behaviour switch due to conflict on " + this.name);
                }
                return;
            }

            EntForm prevForm = CurrentForm;

            EventUtils.FireSwitchOut(CurrentBehaviour);
            CurrentBehaviour = EntUtils.SwitchTo<T>(gameObject);
            CurrentBehaviour.Init();
            EventUtils.FireSwitchIn(CurrentBehaviour);

            CurrentForm = EntUtils.GetFormFromBehaviour(CurrentBehaviour, FormCache);

            if (prevForm != null && prevForm != CurrentForm) {
                prevForm.gameObject.SetActive(false);
            }
            CurrentForm.gameObject.SetActive(true);
        }

        public bool FacingRight() {
            return FacingVector == Vector2.right;
        }

        public void FireEvent(string key) {
            foreach (EntListener listener in Listeners) {
                listener.OnEvent(key);
            }
        }

        private void AppendPosition(List<Vector2> positions) {
            positions.Add(transform.position);
        }

        private void LoadConfig() {
            GameObject configGO = GameObject.Instantiate(
                    Resources.Load(
                        string.Format(CONFIG_RECIPE_PATH_FORMAT, ConfigRecipe)) as GameObject);
            configGO.transform.parent = transform;
            _Config = configGO.GetComponent<EntConfig>();

            if (_Config == null) {
                Debug.LogError("[AVATAR] Couldn't find config with name " + ConfigRecipe);
            }
        }
    }
}
