using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;

namespace Ent2D.Events.Listeners {
    [RequireComponent(typeof(ParticleSystem))]
    public class EntParticleSystemListener : EntListener {
        private Color _Color;
        private ParticleSystem _ParticleSystem;

        public void Awake() {
            _ParticleSystem = GetComponent<ParticleSystem>();
        }

        public override Type GetActionType() {
            return typeof(EntParticleSystemAction);
        }

        protected override void OnAction(EntAction action) {
            EntParticleSystemAction particleAction = (EntParticleSystemAction) action;
            Set(particleAction);
        }

        private void Set(EntParticleSystemAction action) {
            if (action.ChangeEmissionRate) {
                _ParticleSystem.emissionRate = action.EmissionRate;
            }
            
            if (action.ChangeSize) {
                _ParticleSystem.startSize = action.StartSize;
            }

            if (action.ChangeSpeed) {
                _ParticleSystem.startSpeed = action.StartSpeed;
            }

            if (action.ChangeColor) {
                _Color = action.StartColor;
                _ParticleSystem.startColor = _Color;
            }

            if (action.ChangeMultiplyColor) {
                _ParticleSystem.startColor = (_Color * action.MultiplyColor) + new Color(0f, 0f, 0f, 1f);
            }
        }
    }
}
