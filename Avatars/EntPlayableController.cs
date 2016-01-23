using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Config;
using Ent2D.Conflict;
using Ent2D.Events;
using Ent2D.Events.Listeners;
using Ent2D.Utils;

//TODO: Event Listeners
namespace Ent2D {
    public abstract class EntPlayableController : EntController {
        public const string CONFIG_RECIPE_PATH_FORMAT = "Avatars/Recipes/{0}";

        public ControllerUtils.PlayerNumbers PlayerNumber;
    }
}
