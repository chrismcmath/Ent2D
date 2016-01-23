using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Ent2D.Events.Actions;
using Ent2D.Utils;

namespace Ent2D.Events {
    public class EntActionModel : MonoBehaviour {
        private Dictionary<string, EntActionBucket> _Buckets =
            new Dictionary<string, EntActionBucket>();

        public List<EntAction> this[string key] {
            get {
                return _Buckets[key].Actions;
            }
        }

        public bool ContainsKey(string key) {
            return _Buckets.ContainsKey(key);
        }

        public void AddBucket(string eventKey) {
            Transform childT = UnityUtils.CreateEmptyChild(eventKey, transform, true);
        }

        public void Load() {
            _Buckets.Clear();
            EntActionBucket[] buckets = GetComponentsInChildren<EntActionBucket>();

            foreach (EntActionBucket bucket in buckets) {
                bucket.Load();
                _Buckets.Add(bucket.name, bucket);
            }
        }

        public string ListEvents() {
            string s = "";
            foreach (string evtKey in _Buckets.Keys.ToList()) {
                s += string.Format("{0}, ", evtKey);
            }
            return s;
        }
    }
}
