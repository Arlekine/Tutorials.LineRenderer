using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class EdgeLinePhysicsCreator : ILinePhysicsCreator, IDisposable
    {
        private float _edgeWidth;
        private float _minDrawingDistance;

        private GameObject _targetObject;
        private List<Vector3> _currentPoints = new List<Vector3>();

        public EdgeLinePhysicsCreator(float minDrawingDistance, float edgeWidth)
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

            Rigidbody2D rigidbody = _targetObject.GetComponent<Rigidbody2D>();
            if (rigidbody == false)
            {
                rigidbody = _targetObject.AddComponent<Rigidbody2D>();
                rigidbody.useAutoMass = true;
            }

            var edgeCollider = _targetObject.AddComponent<EdgeCollider2D>();
            edgeCollider.edgeRadius = _edgeWidth;

            var localPoints = new List<Vector2>();

            foreach (var currentPoint in _currentPoints)
                localPoints.Add(_targetObject.transform.InverseTransformPoint(currentPoint));

            edgeCollider.SetPoints(localPoints);
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
    }
}