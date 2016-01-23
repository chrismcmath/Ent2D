using UnityEngine;
using System.Collections;

using Ent2D.Utils;

namespace Ent2D.Core {
    public class GlobalInit {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitGlobal() {
            Transform t = UnityUtils.CreateEmptyChild("EntGlobal", null, true);
            t.gameObject.AddComponent<Global>();
        }
    }
}
