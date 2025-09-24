using UnityEngine;

public class SimpleMovementController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    
    [Header("Rotation")]
    public float rotationSpeed = 100f; 
    
    [Header("Input")]
    public VariableJoystick variableJoystick;
    
    [Header("Components")]
    public CharacterController characterController;

    public void Update()
    {
        // Поворот вокруг оси Y (горизонтальная ось джойстика)
        float rotationInput = variableJoystick.Horizontal;
        if (Mathf.Abs(rotationInput) > 0.1f)
        {
            transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f);
        }
        
        // Движение вперед/назад (вертикальная ось джойстика)
        float moveInput = variableJoystick.Vertical;
        Vector3 moveDirection = transform.forward * moveInput;
        
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}