using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Include.GenericTriggers
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class TypedCollisionHandler2D<T> : MonoBehaviour, ITypedTrigger<T>
    {
        public event Action<T, Collision2D> CollisionEnter;
        public event Action<T, Collision2D> CollisionExit;
        
        public event Action<T> TriggerEnter;
        public event Action<T> TriggerExit;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var targetComponent = collision.collider.GetComponent<T>();

            if (targetComponent != null)
            {
                TriggerEnter?.Invoke(targetComponent);
                CollisionEnter?.Invoke(targetComponent, collision);
                OnEntered(targetComponent);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            var targetComponent = collision.collider.GetComponent<T>();

            if (targetComponent != null)
            {
                TriggerExit?.Invoke(targetComponent);
                CollisionExit?.Invoke(targetComponent, collision);
                OnExit(targetComponent);
            }
        }

        protected virtual void OnEntered(T target)
        {
        }

        protected virtual void OnExit(T target)
        {
        }
    }
}