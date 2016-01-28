using UnityEngine;
using System.Collections;

using Ent2D;

namespace Ent2D.Events {
    public static class EventUtils {
        public const string EVENT_INIT = "INIT";
        public const string EVENT_SWITCH_IN_FORMAT = "ON_SWITCH_IN_{0}";
        public const string EVENT_SWITCH_OUT_FORMAT = "ON_SWITCH_OUT_{0}";

        public static void FireSwitchIn(EntBehaviour behaviour) {
            FireBehaviourEvent(behaviour, EVENT_SWITCH_IN_FORMAT);
        }

        public static void FireSwitchOut(EntBehaviour behaviour) {
            FireBehaviourEvent(behaviour, EVENT_SWITCH_OUT_FORMAT);
        }

        private static void FireBehaviourEvent(EntBehaviour behaviour, string format) {
            if (behaviour == null) return;

            string evtStr = string.Format(format, behaviour.GetType().Name).ToUpper();
            behaviour.FireEvent(evtStr);
        }
    }
}
