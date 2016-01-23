using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Ent2D;
using Ent2D.Core;
using Ent2D.Conflict;
using Ent2D.Maps;
using Ent2D.Utils;

namespace Ent2D.Match {
    public class MatchController : MonoBehaviour {
        public const float AVATAR_SPACING = 10f;
        public const string PLAYER_PREFAB_PATH = "Avatars/PhysAvatar";

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
            get {
                if (_Map == null) {
                    _Map = FindObjectOfType<MapConfig>();
                }
                return _Map;
            }
        }


        public void Start() {
            _Context = Global.Instance.Context;
        }

        public void Update() {
            if (_Context.MatchInProgress) {
                UpdateOutOfBoundsPoints();
            }
        }

        public void LateUpdate() {
            ConflictClinic.ResolveConflicts();
        }

        public void LoadMatch() {
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
                Transform avatarT = UnityUtils.InstantiatePrefab(PLAYER_PREFAB_PATH, root, playerNumber.ToString(), GetStartPosition(i++, count));
                EntPlayableController entCont = avatarT.GetComponent<EntPlayableController>();
                AddAvatar(playerNumber, entCont);

                //avatCont.SetPlayerIdentity(new Color());
            }
        }

        private void CreateMap() {
            Transform root = UnityUtils.CreateEmptyChild("Map", transform);

            SceneManager.LoadScene(_Context.MapName, LoadSceneMode.Additive);
        }

        private void AddAvatar(ControllerUtils.PlayerNumbers playerNumber, EntPlayableController cont) {
            cont.PlayerNumber = playerNumber;
            _Avatars.Add(playerNumber, cont);
        }

        private Vector2 GetStartPosition(int i, int count) {
            float x = i * (AVATAR_SPACING / (float) count) - (AVATAR_SPACING / 2f);
            return new Vector2(x, 0f);
        }

        private void UpdateOutOfBoundsPoints() {
            foreach (EntController avatarCont in _Avatars.Values.ToList()) {

                //TODO: Timing issue here, resolve when match logic more robust
                if (Map == null) {
                    return;
                }

                if (Map.OutOfBounds(avatarCont.transform.position)) {
                    avatarCont.transform.position = Vector2.zero;

                    avatarCont.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        }

        private int GetIndexFromAvatarController(EntController avatarCont) {
            return _Avatars.Values.ToArray().ToList().IndexOf(avatarCont);
        }
    }
}
