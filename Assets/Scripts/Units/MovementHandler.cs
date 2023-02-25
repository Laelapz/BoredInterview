using UnityEngine;

namespace Ship.Units
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private float _rotateSpeed = 0.1f;
        private Rigidbody2D _boatRigidBody;

        private void Awake()
        {
            _boatRigidBody = GetComponentInChildren<Rigidbody2D>();
        }

        public void Move()
        {
            if (!_boatRigidBody) return;

            if (_boatRigidBody.velocity.x < 10)
            {
                _boatRigidBody.AddForce(_moveSpeed * -1 * Time.deltaTime * _boatRigidBody.transform.up, ForceMode2D.Impulse);
            }
        }

        public void Rotate(Vector3 direction)
        {
            Quaternion newDirection = Quaternion.LookRotation(Vector3.forward, direction);
            _boatRigidBody.transform.rotation = Quaternion.RotateTowards(_boatRigidBody.transform.rotation, newDirection, _rotateSpeed * Time.deltaTime);
        }
    }
}
