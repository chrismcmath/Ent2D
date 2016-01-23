using System;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Match;

namespace Ent2D.Bootsrappers {
    public class MatchBootstrapper : Bootstrapper {
        protected override void DeclareRequiredComponents(List<Type> components) {
            components.Add(typeof(UnityEngine.Camera));
            components.Add(typeof(MatchContext));
            components.Add(typeof(MatchController));
        }
    }
}
