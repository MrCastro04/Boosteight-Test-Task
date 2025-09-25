using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public enum PlayerControlMode { FirstPerson, ThirdPerson }
    public PlayerControlMode mode;

    // References
    [Space(20)]
    [SerializeField] private JumpButton _jumpButton;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("First person camera")]
    [SerializeField] private Transform fpCameraTransform;
    [Header("Third person camera")]
    [SerializeField] private Transform cameraPole;
    [SerializeField] private Transform tpCameraTransform;
    [SerializeField] private Transform graphics;
    [Space(20)]

    // Player settings
    [Header("Settings")]
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;
    [SerializeField] private float _jumpForce;
    public float PlayerAcceleration;

    [Header("Third person camera settings")]
    [SerializeField] private LayerMask cameraObstacleLayers;
    private float maxCameraDistance;
    public bool IsMoving;

    // Jump Logic
    [Header("Jump Settings")]
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = true;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void OnEnable()
    {
        _jumpButton.OnJump += () =>
        {
            TryJump();
        };
    }

    private void OnDisable()
    {
        _jumpButton.OnJump -= () =>
        {
            TryJump();
        };
    }

    private void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;
        halfScreenWidth = Screen.width / 2;
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        if (mode == PlayerControlMode.ThirdPerson)
        {
            cameraPitch = cameraPole.localRotation.eulerAngles.x;
            maxCameraDistance = tpCameraTransform.localPosition.z;
        }
    }

    private void Update()
    {
        GetTouchInput();

        if (rightFingerId != -1)
        {
            LookAround();
        }

        if (leftFingerId != -1)
        {
            Move();
        }

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        if (mode == PlayerControlMode.ThirdPerson) MoveCamera();
    }

    private void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        IsMoving = false;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                    }
                    break;
                case TouchPhase.Moved:
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {
                        moveInput = t.position - moveTouchStartPosition;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private void LookAround()
    {
        switch (mode)
        {
            case PlayerControlMode.FirstPerson:
                cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
                fpCameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
                break;
            case PlayerControlMode.ThirdPerson:
                cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
                cameraPole.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
                break;
        }

        if (mode == PlayerControlMode.ThirdPerson && !IsMoving)
        {
            graphics.Rotate(graphics.up, -lookInput.x);
        }
        transform.Rotate(transform.up, lookInput.x);
    }

    private void MoveCamera()
    {
        Vector3 rayDir = tpCameraTransform.position - cameraPole.position;
        Debug.DrawRay(cameraPole.position, rayDir, Color.red);
        if (Physics.Raycast(cameraPole.position, rayDir, out RaycastHit hit, Mathf.Abs(maxCameraDistance), cameraObstacleLayers))
        {
            tpCameraTransform.position = hit.point;
        }
        else
        {
            tpCameraTransform.localPosition = new Vector3(0, 0, maxCameraDistance);
        }
    }

    private void Move()
    {
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            IsMoving = false;
            return;
        }

        if (!IsMoving)
        {
            graphics.localRotation = Quaternion.Euler(0, 0, 0);
            IsMoving = true;
        }
        
        Vector3 moveDirection = transform.right * moveInput.normalized.x + transform.forward * moveInput.normalized.y;
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void CheckIfGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void TryJump()
    {
        if (isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}