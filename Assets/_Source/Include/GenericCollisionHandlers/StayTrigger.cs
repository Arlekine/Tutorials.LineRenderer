using System;
using System.Collections.Generic;
using UnityEngine;

namespace Include.GenericTriggers
{
    public abstract class StayTrigger<T> : MonoBehaviour
    {
        public interface ITimeTrigger
        {}

        private class TimeTrigger : ITimeTrigger
        {
            private Action<T> _action;

            public TimeTrigger(float timeToTrigger, Action<T> action)
            {
                TimeToTrigger = timeToTrigger;
                _action = action;
            }

            public float TimeToTrigger { get; }
            public void Trigger(T collidedObject) => _action(collidedObject);
        }

        private class CollisionHolder
        {
            private T _collidedObject;
            private float _collsionStartTime;

            private List<TimeTrigger> _triggeredTriggers = new List<TimeTrigger>();

            public CollisionHolder(T collidedObject, float collsionStartTime)
            {
                _collidedObject = collidedObject;
                _collsionStartTime = collsionStartTime;
            }

            public T CollidedObject => _collidedObject;

            public void TryTrigger(TimeTrigger timeTrigger)
            {
                if (_triggeredTriggers.Contains(timeTrigger))
                    return;

                if (Time.time - _collsionStartTime > timeTrigger.TimeToTrigger)
                {
                    _triggeredTriggers.Add(timeTrigger);
                    timeTrigger.Trigger(_collidedObject);
                }
            }
        }

        private ITypedTrigger<T> _typedTrigger;

        private List<TimeTrigger> _timeTriggers = new List<TimeTrigger>();
        private List<CollisionHolder> _collisionHolders = new List<CollisionHolder>();

        private ITypedTrigger<T> TypedTrigger 
        {
            get
            {
                if (_typedTrigger == null)
                    _typedTrigger = GetComponent<ITypedTrigger<T>>();
                
                return _typedTrigger;
            }
        }

        public ITimeTrigger AddTimeTrigger(float timeTriggerInSeconds, Action<T> action)
        {
            var newTimeTrigger = new TimeTrigger(timeTriggerInSeconds, action);
            _timeTriggers.Add(newTimeTrigger);
            return newTimeTrigger;
        }

        public bool RemoveTimeTrigger(ITimeTrigger timeTrigger)
        {
            return _timeTriggers.Remove((TimeTrigger)timeTrigger);
        }

        private void OnEntered(T target)
        {
            var newCollisionHolder = new CollisionHolder(target, Time.time);
            _collisionHolders.Add(newCollisionHolder);
        }

        private void OnExit(T target)
        {
            _collisionHolders.RemoveAll(x => x.CollidedObject.Equals(target));
        }

        private void OnEnable()
        {
            TypedTrigger.TriggerEnter += OnEntered;
            TypedTrigger.TriggerExit += OnExit;
        }

        private void OnDisable()
        {
            TypedTrigger.TriggerEnter -= OnEntered;
            TypedTrigger.TriggerExit -= OnExit;
        }

        private void Update()
        {
            _collisionHolders.ForEach(x => _timeTriggers.ForEach(x.TryTrigger));
        }
    }
}