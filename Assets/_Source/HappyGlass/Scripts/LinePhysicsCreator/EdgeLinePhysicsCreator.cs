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
        }

        public void UpdateEditing(Vector2 point)
        {
        }

        public void FinishEditing(Vector2 point)
        {

        }

        public void ForceStopCurrent()
        {
        }

        public void Dispose()
        {
            if (_targetObject != false)
                ForceStopCurrent();
        }
    }
}