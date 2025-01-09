using System;
using UnityEngine;
using UnityEngine.UI;

namespace LineRendererTutorial.HappyGlass
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _restartButton;

        public event Action PlayClicked;
        public event Action RestartClicked;

        public void SetPlayMode()
        {
            _playButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(true);
        }

        public void SetPrepareMode()
        {
            _playButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnPlayClicked() => PlayClicked?.Invoke();
        private void OnRestartClicked() => RestartClicked?.Invoke();
    }
}