using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Utils;

namespace Ent2D.Match {
    public class MatchContext : MonoBehaviour {
        public List<ControllerUtils.PlayerNumbers> PlayerNumbers = new List<ControllerUtils.PlayerNumbers>(); 
        public string MapKey = null;

        public void Awake() {
            DummyData();
        }

        public void DummyData() {
            return;
            PlayerNumbers.Add(ControllerUtils.PlayerNumbers.PLAYER1);
            PlayerNumbers.Add(ControllerUtils.PlayerNumbers.PLAYER2);
            //PlayerNumbers.Add(ControllerUtils.PlayerNumbers.PLAYER3);
            //PlayerNumbers.Add(ControllerUtils.PlayerNumbers.PLAYER4);
        }
    }
}
