using System;
using System.Collections.Generic;
using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class LineRendererDrawer : ILineDrawer, IDisposable
    {
        private ILineRenderFactory _factory;
        private float _minDrawingDistance;

        private LineRenderer _currentRenderer;
        private List<LineRenderer> _renderers = new List<LineRenderer>();

        public LineRendererDrawer(ILineRenderFactory factory, float minDrawingDistance)
        {
            _factory = factory;
            _minDrawingDistance = minDrawingDistance;
        }

        public Renderer StartDrawing(Vector2 point)
        {
            _currentRenderer = _factory.Create();
            _currentRenderer.useWorldSpace = false;
            _renderers.Add(_currentRenderer);

            _currentRenderer.positionCount = 1;
            _currentRenderer.SetPosition(0, _currentRenderer.transform.InverseTransformPoint(point));

            return _currentRenderer;
        }

        public void UpdateDrawing(Vector2 point)
        {
            if (_currentRenderer == null)
                throw new Exception("Drawer isn't draw right now");

            if (Vector3.Distance(_currentRenderer.GetPosition(_currentRenderer.positionCount - 1), point) > _minDrawingDistance)
            {
                _currentRenderer.positionCount++;
                _currentRenderer.SetPosition(_currentRenderer.positionCount - 1, _currentRenderer.transform.InverseTransformPoint(point));
            }
        }

        public void FinishDrawing(Vector2 point)
        {
            _currentRenderer.positionCount++;
            _currentRenderer.SetPosition(_currentRenderer.positionCount - 1, _currentRenderer.transform.InverseTransformPoint(point));
            _currentRenderer = null;
        }

        public void ForceStopCurrent()
        {
            if (_currentRenderer == false)
                return;

            _renderers.Remove(_currentRenderer);
            _factory.Destroy(_currentRenderer);
            _currentRenderer = null;
        }

        public void Clear()
        {
            _renderers.ForEach(x =>
            {
                if (x != null)
                    _factory.Destroy(x);
            });
            _renderers.Clear();
        }

        public void Dispose()
        {
            Clear();
            _currentRenderer = null;
        }
    }
}