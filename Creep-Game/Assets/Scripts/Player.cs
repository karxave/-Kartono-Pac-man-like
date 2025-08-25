using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Camera _camera;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        // Get inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Camera-relative directions
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        // Flatten to avoid moving up/down with camera tilt
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Movement direction
        Vector3 movementDirection = (horizontal * right + vertical * forward).normalized;

        // Preserve Y velocity (gravity, jump, etc.)
        Vector3 velocity = movementDirection * _speed;
        velocity.y = _rigidBody.velocity.y;

        // Apply movement
        _rigidBody.velocity = velocity;
    }
}
