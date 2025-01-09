using System;
using Include;
using Include.GenericTriggers;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class GameplayController : IDisposable
    {
        private UIMediator _uiMediator;
        private Ball _mainBall;
        private StayTrigger<Ball> _finalTrigger;
        private PhysicsLineCreationController _lineController;
        private GameplayConfig _gameplayConfig;

        private StayTrigger<Ball>.ITimeTrigger _timeTrigger;

        public GameplayController(UIMediator uiMediator, Ball mainBall, StayTrigger<Ball> finalTrigger, PhysicsLineCreationController lineController, GameplayConfig config)
        {
            _uiMediator = uiMediator;
            _mainBall = mainBall;
            _finalTrigger = finalTrigger;
            _lineController = lineController;
            _gameplayConfig = config;
        }

        public void Initialize()
        {
            _mainBall.Initialize();

            SetPrepareMode();
            
            _uiMediator.PlayClicked += OnPlayClicked;
            _uiMediator.RestartClicked += OnRestartClicked;

            _timeTrigger = _finalTrigger.AddTimeTrigger(_gameplayConfig.WinTriggerTime, OnTriggered);
        }

        public void Dispose()
        {
            _uiMediator.PlayClicked -= OnPlayClicked;
            _uiMediator.RestartClicked -= OnRestartClicked;

            _finalTrigger.RemoveTimeTrigger(_timeTrigger);
        }

        private void SetPlayMode()
        {
            _uiMediator.SetPlayMode();
            _mainBall.Activate();
            _lineController.Disable();
        }

        private void SetPrepareMode()
        {
            _uiMediator.SetPrepareMode();
            _mainBall.Deactivate();
            _mainBall.ResetPosition();
            _lineController.Enable();
            _lineController.Clear();
        }

        private void OnPlayClicked()
        {
            SetPlayMode();
        }

        private void OnRestartClicked()
        {
            SetPrepareMode();
        }

        private void OnTriggered(Ball ball)
        {
            Debug.Log("Game FINISHED!");
        }
    }
}