using System;
using System.Collections.Generic;
using Include;
using LineRendererTutorial.LineInput;
using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class LineRendererBootstrap : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRendererPrefab;
        [SerializeField] private Transform _lineRenderersParent;
        [SerializeField] private float _drawingMinDistance = 0.1f;

        [Header("Input")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _inputDepth;

        private List<IDisposable> _disposables;

        private void Awake()
        {
            var coroutineHandler = new MonoBehaviourCoroutineHandler(this);

            var lineRendererFactory = new LineRendererFactory(_lineRendererPrefab, _lineRenderersParent);
            var lineDrawer = new LineRendererDrawer(lineRendererFactory, _drawingMinDistance);
            var lineInput = new MouseWorldPointsLineInput(_camera, coroutineHandler, _inputDepth);
            var drawingController = new DrawingController(lineDrawer, lineInput);

            _disposables = new List<IDisposable>(){lineDrawer, lineInput, drawingController};
        }

        private void OnDestroy()
        {
            _disposables.ForEach(x => x.Dispose());
        }
    }
}