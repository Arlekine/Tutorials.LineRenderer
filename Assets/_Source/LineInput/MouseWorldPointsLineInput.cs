using System;
using System.Collections;
using Include;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LineRendererTutorial.LineInput
{
    public class MouseWorldPointsLineInput : ILineInput, IDisposable
    {
        private const int MOUSE_BUTTON_INDEX = 0;

        private Camera _camera;
        private ICoroutineHandler _coroutineHandler;
        private float _depth;

        private bool _isPressed = false;
        private Coroutine _updateRoutine;

        public event Action<Vector2> Started;
        public event Action<Vector2> Updated;
        public event Action<Vector2> Finished;

        public MouseWorldPointsLineInput(Camera camera, ICoroutineHandler coroutineHandler, float depth)
        {
            _camera = camera;
            _coroutineHandler = coroutineHandler;
            _depth = depth;

            _updateRoutine = _coroutineHandler.StartCoroutine(UpdateRoutine());
        }

        public void Dispose()
        {
            _coroutineHandler.StopCoroutine(_updateRoutine);
        }

        private IEnumerator UpdateRoutine()
        {
            while (true)
            {
                if (_isPressed)
                {
                    if (Input.GetMouseButtonUp(MOUSE_BUTTON_INDEX))
                    {
                        _isPressed = false;
                        Finished?.Invoke(GetCurrentMouseWorldPosition());
                    }
                    else
                    {
                        Updated?.Invoke(GetCurrentMouseWorldPosition());
                    }
                }
                else if (Input.GetMouseButtonDown(MOUSE_BUTTON_INDEX) && EventSystem.current.IsPointerOverGameObject() == false)
                {
                    _isPressed = true;
                    Started?.Invoke(GetCurrentMouseWorldPosition());
                }

                yield return null;
            }
        }

        private Vector2 GetCurrentMouseWorldPosition()
        {
            var screenPos = Input.mousePosition;
            return _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _depth));
        }
    }
}