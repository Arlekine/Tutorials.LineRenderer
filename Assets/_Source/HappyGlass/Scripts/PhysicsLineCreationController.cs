using System;
using LineRendererTutorial.Drawer;
using LineRendererTutorial.LineInput;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class PhysicsLineCreationController : IDisposable
    {
        private ILinePhysicsCreator _linePhysics;
        private ILineDrawer _lineDrawer;
        private ILineInput _lineInput;
        
        private LayerMask _layer;

        public PhysicsLineCreationController(ILinePhysicsCreator linePhysics, ILineDrawer lineDrawer, ILineInput lineInput, LayerMask layer)
        {
            _linePhysics = linePhysics;
            _lineDrawer = lineDrawer;
            _lineInput = lineInput;
            _layer = layer;
        }

        public void Enable()
        {
            _lineInput.Started += OnDrawingStarted;
        }

        public void Disable()
        {
            _lineInput.Started -= OnDrawingStarted;
            _lineInput.Updated -= OnDrawingUpdate;
            _lineInput.Finished -= OnDrawingFinished;
        }

        public void Clear()
        {
            _lineDrawer.ForceStopCurrent();
            _linePhysics.ForceStopCurrent();

            _lineDrawer.Clear();
        }

        public void Dispose() => Disable();

        private void OnDrawingStarted(Vector2 point)
        {
            if (Physics2D.Raycast(point, Vector3.forward, 5f, _layer))
                return;

            _lineInput.Finished += OnDrawingFinished;
            _lineInput.Updated += OnDrawingUpdate;

            var renderer = _lineDrawer.StartDrawing(point);
            _linePhysics.StartEditing(renderer.gameObject, point);
        }

        private void OnDrawingUpdate(Vector2 point)
        {
            if (Physics2D.Raycast(point, Vector3.forward, 5f, _layer))
            {
                _lineInput.Finished -= OnDrawingFinished;
                _lineInput.Updated -= OnDrawingUpdate;

                _lineDrawer.ForceStopCurrent();
                _linePhysics.ForceStopCurrent();
                return;
            }

            _linePhysics.UpdateEditing(point);
            _lineDrawer.UpdateDrawing(point);
        }

        private void OnDrawingFinished(Vector2 point)
        {
            _lineInput.Finished -= OnDrawingFinished;
            _lineInput.Updated -= OnDrawingUpdate;

            _linePhysics.FinishEditing(point);
            _lineDrawer.FinishDrawing(point);
        }
    }
}