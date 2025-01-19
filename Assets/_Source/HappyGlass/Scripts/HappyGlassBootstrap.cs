using System;
using System.Collections.Generic;
using Include;
using Include.GenericTriggers;
using LineRendererTutorial.Drawer;
using LineRendererTutorial.LineInput;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class HappyGlassBootstrap : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRendererPrefab;
        [SerializeField] private Transform _lineRenderersParent;
        [SerializeField] private LayerMask _stopDrawingLayer;
        [SerializeField] private float _drawingMinDistance = 0.1f;
        [SerializeField] private float _radiusMultiplayer = 0.5f;

        [Header("UI")]
        [SerializeField] private UIMediator _uiMediator;

        [Header("Scene")]
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private Ball _ball;
        [SerializeField] private StayTrigger<Ball> _finalTrigger;

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
            var physicsCreator = new CapsuleColliderPhysicsCreator(_drawingMinDistance, _lineRendererPrefab.startWidth);

            var lineCreationController = new PhysicsLineCreationController(physicsCreator, lineDrawer, lineInput, _stopDrawingLayer);
            var gameplayController = new GameplayController(_uiMediator, _ball, _finalTrigger, lineCreationController, _gameplayConfig);

            gameplayController.Initialize();

            _disposables = new List<IDisposable>() { lineDrawer, lineInput, physicsCreator, lineCreationController, gameplayController };
        }

        private void OnDestroy() => _disposables.ForEach(x => x.Dispose());
    }
}