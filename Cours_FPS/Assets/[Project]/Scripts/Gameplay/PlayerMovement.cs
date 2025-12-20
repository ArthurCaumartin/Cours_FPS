using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [Header("Movement : ")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;
    [Header("Jump : ")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fallingSpeed = 4f;
    [SerializeField] private float _fallingAcceleration = 5;
    [SerializeField] private float _gravityScale = 0.5f;
    private Rigidbody _rigidbody;
    private Vector3 _inputDirection;
    private bool _hasJump = false;
    private float _additionalFallingVelocity = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        SetYRigidbodyVelocity();
        SetXZRigidbodyVelocity();

        _additionalFallingVelocity =
        _groundDetector.IsGrounded ? 0 : _additionalFallingVelocity - Time.deltaTime * _fallingAcceleration;
    }

    private void SetYRigidbodyVelocity()
    {
        Vector3 velocity = _rigidbody.velocity;
        float velToSet;
        if (_hasJump)
            velToSet = _jumpForce;
        else
        {
            velToSet = Mathf.Lerp(velocity.y, Physics.gravity.y * _gravityScale, Time.deltaTime * _fallingSpeed);
            velToSet += _additionalFallingVelocity;
        }

        velocity.y = velToSet;
        _rigidbody.velocity = velocity;

        _hasJump = false;
    }

    private void SetXZRigidbodyVelocity()
    {
        Vector3 velocity = _inputDirection * _moveSpeed;
        velocity = velocity.z * transform.forward + velocity.x * transform.right;
        velocity = Vector3.Lerp(_rigidbody.velocity, velocity, Time.fixedDeltaTime * _acceleration);
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _inputDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    private void OnJump(InputValue value)
    {
        if (!_groundDetector.IsGrounded) return;
        print("erghui");
        _hasJump = value.Get<float>() > .5f;
    }
}
