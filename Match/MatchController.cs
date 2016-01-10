using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Conflict;
using Ent2D.Maps;
using Ent2D.Utils;

namespace Ent2D.Match {
    public class MatchController : MonoBehaviour {
        public const float AVATAR_SPACING = 10f;

        public GameObject PlayerPrefab;

        private MatchContext _Context;

        private Dictionary<ControllerUtils.PlayerNumbers, EntController> _Avatars =
            new Dictionary<ControllerUtils.PlayerNumbers, EntController>();

        public List<EntController> Avatars {
            get {
                return  _Avatars.Values.ToList();
            }
        }

        private MapConfig _Map;
        public MapConfig Map {
            get { return _Map; }
        }

        public void Awake() {
            _Context = GetComponent<MatchContext>();
        }

        public void Start() {
            ResetAll();
        }

        public void Update() {
            UpdateOutOfBoundsPoints();
        }

        public void LateUpdate() {
            ConflictClinic.ResolveConflicts();
        }

        public void ResetAll() {
            _Avatars = new Dictionary<ControllerUtils.PlayerNumbers, EntController>();
            RemoveAllChildren();

            CreateAvatars();
            CreateMap();
        }

        private void RemoveAllChildren() {
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }
        }

        private void CreateAvatars() {
            Transform root = UnityUtils.CreateEmptyChild("Avatars", transform);

            int i = 0;
            int count = _Context.PlayerNumbers.Count;
            foreach (ControllerUtils.PlayerNumbers playerNumber in _Context.PlayerNumbers) {
                Transform avatarT = UnityUtils.InstantiatePrefab(PlayerPrefab, root, playerNumber.ToString(), GetStartPosition(i++, count));
                EntController entCont = avatarT.GetComponent<EntController>();
                AddAvatar(playerNumber, entCont);

                //avatCont.SetPlayerIdentity(new Color());
            }
        }

        private void CreateMap() {
            Transform root = UnityUtils.CreateEmptyChild("Map", transform);

            Transform mapPrefab = UnityUtils.InstantiatePrefab(_Context.MapKey, root, _Context.MapKey);
            _Map = mapPrefab.GetComponent<MapConfig>();
            if (_Map == null) {
                Debug.LogError("Map " + _Context.MapKey + " does not have a MapConfig attached");
            }
        }

        private void AddAvatar(ControllerUtils.PlayerNumbers playerNumber, EntController cont) {
            cont.PlayerNumber = playerNumber;
            _Avatars.Add(playerNumber, cont);
        }

        private Vector2 GetStartPosition(int i, int count) {
            float x = i * (AVATAR_SPACING / (float) count) - (AVATAR_SPACING / 2f);
            return new Vector2(x, 0f);
        }

        private void UpdateOutOfBoundsPoints() {
            foreach (EntController avatarCont in _Avatars.Values.ToList()) {
                if (_Map.OutOfBounds(avatarCont.transform.position)) {
                    avatarCont.transform.position = Vector2.zero;

                    avatarCont.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    //TODO: coupled
                    //avatarCont.SwitchBehaviour<InvulnerableBehaviour>(); 
                    //avatarCont.Aggressor = null;
                }
            }
        }

        private int GetIndexFromAvatarController(EntController avatarCont) {
            return _Avatars.Values.ToArray().ToList().IndexOf(avatarCont);
        }
    }
}
