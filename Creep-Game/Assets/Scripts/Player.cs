using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player: MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 10f;   // Higher = faster acceleration
    [SerializeField] private float _deceleration = 10f;   // Higher = faster stop
    [SerializeField] private Camera _camera;

    private Rigidbody _rigidBody;
    private Vector3 _currentVelocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        HideAndLockCursor();
    }

    private void FixedUpdate()
    {
        // --- INPUT ---
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // --- CAMERA-RELATIVE DIRECTIONS ---
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // --- TARGET MOVEMENT DIRECTION ---
        Vector3 targetDirection = (horizontal * right + vertical * forward).normalized;
        Vector3 targetVelocity = targetDirection * _speed;

        // --- PRESERVE Y VELOCITY ---
        targetVelocity.y = _rigidBody.velocity.y;

        // --- SMOOTH ACCEL/DECEL ---
        float lerpSpeed = (targetDirection.magnitude > 0.1f) ? _acceleration : _deceleration;
        _currentVelocity = Vector3.Lerp(_rigidBody.velocity, targetVelocity, lerpSpeed * Time.fixedDeltaTime);

        // --- APPLY VELOCITY ---
        _rigidBody.velocity = _currentVelocity;
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
