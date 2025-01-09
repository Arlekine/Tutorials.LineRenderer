using System;
using System.Collections.Generic;
using Include;
using LineRendererTutorial.LineInput;
using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class TrailRendererBootstrap : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRendererPrefab;
        [SerializeField] private Transform _trailRenderersParent;

        [Header("Input")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _inputDepth;

        private List<IDisposable> _disposables;

        private void Awake()
        {
            var coroutineHandler = new MonoBehaviourCoroutineHandler(this);

            var lineRendererFactory = new TrailRendererFactory(_trailRendererPrefab, _trailRenderersParent);
            var lineDrawer = new TrailRendererDrawer(lineRendererFactory);
            var lineInput = new MouseWorldPointsLineInput(_camera, coroutineHandler, _inputDepth);
            var drawingController = new DrawingController(lineDrawer, lineInput);

            _disposables = new List<IDisposable>() { lineDrawer, lineInput, drawingController };
        }

        private void OnDestroy()
        {
            _disposables.ForEach(x => x.Dispose());
        }
    }
}