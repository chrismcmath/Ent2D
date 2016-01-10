using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Config;
using Ent2D.Conflict;
using Ent2D.Events.Listeners;
using Ent2D.Utils;


//TODO: get rid - dependancy is in SwitchBehaviour
using Cockateers.Utils; 

//TODO: Event Listeners
namespace Ent2D {
    public abstract class EntController : MonoBehaviour {
        public const string CONFIG_RECIPE_PATH_FORMAT = "Avatars/Recipes/{0}";

        //TODO: Improve this system
        public bool Logging = false;
        public string ConfigRecipe = "Chris";
        public ControllerUtils.PlayerNumbers PlayerNumber;
        public EntController Aggressor;

        protected EntConfig _Config;

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

        protected Dictionary<string, EntForm> _FormCache
            = new Dictionary<string, EntForm>();

        private List<EntListener> _Listeners =
            new List<EntListener>();

        public void Awake() {
            LoadConfig();
        }

        public void Start() {
            CacheForms();
            LoadInitialBehaviour();

            _Listeners.AddRange(GetComponentsInChildren<EntListener>());

            //TODO: Should be set elsewhere
            GetComponent<Rigidbody2D>().mass = _Config.Mass;
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

            CurrentForm = EntUtils.LookUpForm<T>(_FormCache);
            CurrentBehaviour = EntUtils.SwitchTo<T>(gameObject);

            if (prevForm != null && prevForm != CurrentForm) {
                prevForm.gameObject.SetActive(false);
            }
            CurrentForm.gameObject.SetActive(true);
        }

        public bool FacingRight() {
            return FacingVector == Vector2.right;
        }

        public void FireEvent(string key) {
            foreach (EntListener listener in _Listeners) {
                listener.OnEvent(key);
            }
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

        private void CacheForms() {
            EntForm[] forms = GetComponentsInChildren<EntForm>();

            foreach (EntForm form in forms) {
                _FormCache.Add(form.name, form);
                form.gameObject.SetActive(false);
            }
        }
    }
}
