using System;
using System.Collections.Generic;

using UnityEngine;

namespace Ent2D.Bootsrappers {
    public abstract class Bootstrapper : MonoBehaviour {
        protected List<Type> _RequiredComponents = new List<Type>();

        public void Awake() {
            DeclareRequiredComponents(_RequiredComponents);
            BootstrapComponents();
        }

        protected abstract void DeclareRequiredComponents(List<Type> components);

        private void BootstrapComponents() {
            foreach (Type type in _RequiredComponents) {
                gameObject.AddComponent(type);
            }
        }
    }
}
