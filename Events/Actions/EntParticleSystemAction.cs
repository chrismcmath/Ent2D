using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ent2D.Events.Actions {
    [System.Serializable]
    public class EntParticleSystemAction : EntAction {
        public bool ChangeEmissionRate = false;
        [SerializeField]
        private float _EmissionRate;
        public virtual float EmissionRate {
            get { return _EmissionRate; }
        }

        public bool ChangeSize = false;
        [SerializeField]
        private float _StartSize;
        public virtual float StartSize {
            get { return _StartSize; }
        }

        public bool ChangeSpeed = false;
        [SerializeField]
        private float _StartSpeed;
        public virtual float StartSpeed {
            get { return _StartSpeed; }
        }

        public bool ChangeColor = false;
        [SerializeField]
        private Color _StartColor;
        public virtual Color StartColor {
            get { return _StartColor; }
        }

        public bool ChangeMultiplyColor = false;
        [SerializeField]
        private float _MultiplyColor;
        public virtual float MultiplyColor {
            get { return _MultiplyColor; }
        }
    }
}
