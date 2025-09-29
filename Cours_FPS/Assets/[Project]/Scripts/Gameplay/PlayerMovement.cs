using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    private Rigidbody _rigidbody;
    private Vector3 _inputDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 velocity = _inputDirection * _moveSpeed;
        
        velocity = velocity.z * transform.forward + velocity.x * transform.right;
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _inputDirection = new Vector3(input.x, 0, input.y).normalized;
    }
}
