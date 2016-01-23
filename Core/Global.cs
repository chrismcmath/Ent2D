using UnityEngine;
using System.Collections;

using Ent2D.Bootsrappers;
using Ent2D.Match;
using Ent2D.Utils;

namespace Ent2D.Core {
    public class Global : Singleton<Global> {
        public GlobalData Data;
        public MatchContext Context;
        public System.Type Type;

        public void Awake() {
            //TODO: I mean, really, what is this for?
            Data = ForceGetComponent<GlobalData>();

            AddBootstrapper<MatchBootstrapper>("Match");
            AddBootstrapper<CameraBootstrapper>("Camera");

            Context = GetComponentInChildren<MatchContext>();

            DontDestroyOnLoad(gameObject);
        }

        private void AddBootstrapper<T>(string name) where T : MonoBehaviour {
            Transform t = UnityUtils.CreateEmptyChild(name, transform);
            t.gameObject.AddComponent<T>();
        }

        private T ForceGetComponent<T>() where T : MonoBehaviour {
            return UnityUtils.ForceGetComponent<T>(gameObject);
        }
    }
}
