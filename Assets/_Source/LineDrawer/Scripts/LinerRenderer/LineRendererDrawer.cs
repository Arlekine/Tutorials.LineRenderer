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
            return _currentRenderer;
        }

        public void UpdateDrawing(Vector2 point)
        {}

        public void FinishDrawing(Vector2 point)
        {}

        public void ForceStopCurrent()
        {}

        public void Clear()
        {}

        public void Dispose()
        {
            Clear();
            _currentRenderer = null;
        }
    }
}