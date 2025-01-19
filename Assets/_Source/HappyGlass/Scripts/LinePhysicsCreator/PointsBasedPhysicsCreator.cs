using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public abstract class PointsBasedPhysicsCreator : ILinePhysicsCreator, IDisposable
    {
        protected float _edgeWidth;
        private float _minDrawingDistance;

        private GameObject _targetObject;
        private List<Vector3> _currentPoints = new List<Vector3>();

        public PointsBasedPhysicsCreator(float minDrawingDistance, float edgeWidth)
        {
            _minDrawingDistance = minDrawingDistance;
            _edgeWidth = edgeWidth;
        }

        public void StartEditing(GameObject targetObject, Vector2 point)
        {
            _targetObject = targetObject;
            _currentPoints.Add(point);
        }

        public void UpdateEditing(Vector2 point)
        {
            if (_targetObject == null)
                throw new Exception("Physics creator isn't draw right now");

            if (Vector3.Distance(_currentPoints.Last(), point) > _minDrawingDistance)
            {
                _currentPoints.Add(point);
            }
        }

        public void FinishEditing(Vector2 point)
        {
            if (_targetObject == null)
                throw new Exception("Physics creator isn't draw right now");

            _currentPoints.Add(point);

            Rigidbody2D rigidbody = _targetObject.GetComponent<Rigidbody2D>();
            if (rigidbody == false)
            {
                rigidbody = _targetObject.AddComponent<Rigidbody2D>();
                rigidbody.useAutoMass = true;
            }

            var localPoints = new List<Vector2>();

            foreach (var currentPoint in _currentPoints)
                localPoints.Add(_targetObject.transform.InverseTransformPoint(currentPoint));

            CreateCollider(localPoints, rigidbody);
            rigidbody.centerOfMass = localPoints[localPoints.Count / 2];
            ForceStopCurrent();

        }

        public void ForceStopCurrent()
        {
            if (_targetObject == false)
                return;

            _targetObject = null;
            _currentPoints.Clear();
        }

        public void Dispose()
        {
            if (_targetObject != false)
                ForceStopCurrent();
        }

        protected abstract void CreateCollider(List<Vector2> localPoints, Rigidbody2D rigidbody);
    }
}