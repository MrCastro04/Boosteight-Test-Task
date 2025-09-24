using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public enum PlayerControlMode { FirstPerson, ThirdPerson}
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

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    
    private void Awake(){
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
    }

    private void OnEnable()
    {
        _jumpButton.OnJump += () =>
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        };
    }

    private void OnDisable()
    {
        _jumpButton.OnJump -= () =>
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }; 
    }


    private void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        if (mode == PlayerControlMode.ThirdPerson) {

            // Get the initial angle for the camera pole
            cameraPitch = cameraPole.localRotation.eulerAngles.x;

            // Set max camera distance to the distance the camera is from the player in the editor
            maxCameraDistance = tpCameraTransform.localPosition.z;
        }
    }

    private void Update()
    {
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1) 
        {
            LookAround();
        }

        if (leftFingerId != -1)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        
        if (mode == PlayerControlMode.ThirdPerson) MoveCamera();
    }

    private void GetTouchInput() {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        //Debug.Log("Stopped tracking left finger");
                        IsMoving = false;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId) {

                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
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
                // vertical (pitch) rotation is applied to the first person camera
                cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
                fpCameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
                break;
            case PlayerControlMode.ThirdPerson:
                // vertical (pitch) rotation is applied to the third person camera pole
                cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
                cameraPole.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
                break;
        }

        if (mode == PlayerControlMode.ThirdPerson && !IsMoving)
        {
            // Rotate the graphics in the opposite direction when stationary
            graphics.Rotate(graphics.up, -lookInput.x);
        }
        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    private void MoveCamera() {

        Vector3 rayDir = tpCameraTransform.position - cameraPole.position;

        Debug.DrawRay(cameraPole.position, rayDir, Color.red);
        // Check if the camera would be colliding with any obstacle
        if (Physics.Raycast(cameraPole.position, rayDir, out RaycastHit hit, Mathf.Abs(maxCameraDistance), cameraObstacleLayers)){
            // Move the camera to the impact point
            tpCameraTransform.position = hit.point;
        } else {
            // Move the camera to the max distance on the local z axis
            tpCameraTransform.localPosition = new Vector3(0, 0, maxCameraDistance);
        }
    }

    private void Move()
    {
        // Не двигаемся, если ввод находится в "мертвой зоне"
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            IsMoving = false;
            return;
        }

        if (!IsMoving) {
            // Устанавливаем rotation графики только один раз при начале движения
            graphics.localRotation = Quaternion.Euler(0, 0, 0);
            IsMoving = true;
        }
        
        Vector3 moveDirection = transform.right * moveInput.normalized.x + transform.forward * moveInput.normalized.y;
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
