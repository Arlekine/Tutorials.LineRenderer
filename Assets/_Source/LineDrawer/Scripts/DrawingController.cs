using System;
using LineRendererTutorial.LineInput;
using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class DrawingController : IDisposable
    {
        private ILineDrawer _lineDrawer;
        private ILineInput _lineInput;

        public DrawingController(ILineDrawer lineDrawer, ILineInput lineInput)
        {
            _lineDrawer = lineDrawer;
            _lineInput = lineInput;

            _lineInput.Started += OnDrawingStarted;
            _lineInput.Updated += OnDrawingUpdate;
            _lineInput.Finished += OnDrawingFinished;
        }

        public void Dispose()
        {
            _lineInput.Started -= OnDrawingStarted;
            _lineInput.Updated -= OnDrawingUpdate;
            _lineInput.Finished -= OnDrawingFinished;
        }

        private void OnDrawingStarted(Vector2 point) => _lineDrawer.StartDrawing(point);
        private void OnDrawingUpdate(Vector2 point) => _lineDrawer.UpdateDrawing(point);
        private void OnDrawingFinished(Vector2 point) => _lineDrawer.FinishDrawing(point);
    }
}