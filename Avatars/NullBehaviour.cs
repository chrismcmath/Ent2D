using UnityEngine;
using System.Collections;

using Ent2D.ViewControllers;
using Ent2D.Config;
using Ent2D.Utils;

namespace Ent2D {
    public class NullBehaviour : EntBehaviour {
        protected override void UpdateContinuousInput() {}
        protected override void UpdateRigidbody() {}
    }
}
