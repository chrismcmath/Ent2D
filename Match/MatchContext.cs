using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Utils;

namespace Ent2D.Match {
    public class MatchContext : MonoBehaviour {
        public const int MAX_PLAYERS = 8;

        public List<ControllerUtils.PlayerNumbers> PlayerNumbers = new List<ControllerUtils.PlayerNumbers>(); 
        public bool MatchInProgress = false;
        public string MapName;
        public int PlayerCount {
            get {
                return PlayerNumbers.Count;
            } set {
                if (value > 0 && value < MAX_PLAYERS) {
                    LoadPlayers(value);
                }
            }
        }

        public void Reset() {
            MapName = null;
        }

        private void LoadPlayers(int count) {
            for (int i = 0; i < count; i++) {
                PlayerNumbers.Add(ControllerUtils.GetPlayerNumber(i + 1));
            }
        }
    }
}
