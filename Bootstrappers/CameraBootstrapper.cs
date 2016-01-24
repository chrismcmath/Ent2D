using System;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Camera;

namespace Ent2D.Bootsrappers {
    public class CameraBootstrapper : Bootstrapper {
        protected override void DeclareRequiredComponents(List<Type> components) {
            components.Add(typeof(UnityEngine.Camera));
            components.Add(typeof(AudioListener));
            components.Add(typeof(MatchCameraController));
            components.Add(typeof(EntGrid));
        }
    }
}
