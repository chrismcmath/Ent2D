using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ent2D.Utils;

namespace Ent2D {
    //NOTE: A model of the current form
    public class EntForm : MonoBehaviour {
        public List<Collider2D> Attack = new List<Collider2D>();
        public List<Collider2D> Ground = new List<Collider2D>();
        public List<Collider2D> Target = new List<Collider2D>();
        public List<Collider2D> Support = new List<Collider2D>();
        public List<Collider2D> Custom = new List<Collider2D>();

        public EntUtils.ColliderType Type = EntUtils.ColliderType.GROUND;

        public void Awake() {
            CacheColliders();
        }

        private void CacheColliders() {
            //TODO: seperate routing from typing?
            AvatarCollisionRouter[] routers = gameObject.GetComponentsInChildren<AvatarCollisionRouter>();
            foreach (AvatarCollisionRouter router in routers) {
                switch (router.Type) {
                    case EntUtils.ColliderType.ATTACK:
                        AddColliderFromRouter(Attack, router);
                        break;
                    case EntUtils.ColliderType.GROUND:
                        AddColliderFromRouter(Ground, router);
                        break;
                    case EntUtils.ColliderType.SUPPORT:
                        AddColliderFromRouter(Support, router);
                        break;
                    case EntUtils.ColliderType.TARGET:
                        AddColliderFromRouter(Target, router);
                        break;
                    case EntUtils.ColliderType.CUSTOM:
                        AddColliderFromRouter(Custom, router);
                        break;
                }
            }
        }

        private void AddColliderFromRouter(List<Collider2D> list, AvatarCollisionRouter router) {
            Collider2D[] colliders = router.GetComponentsInChildren<Collider2D>();
            list.AddRange(colliders);
        }
    }
}
