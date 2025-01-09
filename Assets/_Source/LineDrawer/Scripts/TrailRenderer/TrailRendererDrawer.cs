using System;
using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class TrailRendererDrawer : ILineDrawer, IDisposable
    {
        private ITrailRenderFactory _factory;

        private TrailRenderer _currentRenderer;

        public TrailRendererDrawer(ITrailRenderFactory factory)
        {
            _factory = factory;
        }

        public Renderer StartDrawing(Vector2 point)
        {
            _currentRenderer = _factory.Create();
            _currentRenderer.transform.position = point;

            return _currentRenderer;
        }

        public void UpdateDrawing(Vector2 point)
        {
            if (_currentRenderer == null)
                throw new Exception("Drawer isn't draw right now");

            _currentRenderer.transform.position = point;
        }

        public void FinishDrawing(Vector2 point)
        {
            _currentRenderer.transform.position = point;
            _currentRenderer.autodestruct = true;
            _currentRenderer = null;
        }

        public void ForceStopCurrent()
        {
            if (_currentRenderer == false)
                throw new Exception("Drawer doesn't draw anything");
            
            _factory.Destroy(_currentRenderer);
            _currentRenderer = null;
        }

        public void Clear()
        {
            if ( _currentRenderer != null)
                ForceStopCurrent();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}