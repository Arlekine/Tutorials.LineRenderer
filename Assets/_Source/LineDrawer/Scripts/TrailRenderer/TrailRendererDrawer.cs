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
            return _currentRenderer;
        }

        public void UpdateDrawing(Vector2 point)
        {
        }

        public void FinishDrawing(Vector2 point)
        {
        }

        public void ForceStopCurrent()
        { }

        public void Clear()
        {
        }

        public void Dispose()
        {
            Clear();
        }
    }
}