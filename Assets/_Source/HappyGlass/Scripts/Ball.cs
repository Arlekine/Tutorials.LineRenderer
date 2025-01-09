using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Vector3 _startPosition;

        public void Initialize() => _startPosition = transform.position;

        public void Activate() => _rigidbody2D.isKinematic = false;
        public void Deactivate() => _rigidbody2D.isKinematic = true;

        public void ResetPosition()
        {
            transform.position = _startPosition;
            _rigidbody2D.velocity = Vector3.zero;
        }

        private void OnValidate()
        {
            if (_rigidbody2D == null)
                _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}