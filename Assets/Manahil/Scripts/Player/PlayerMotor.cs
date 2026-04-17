using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public Transform cameraPivot;
    public float walkSpeed = 4f;
    public float gravity = -9.81f;
    public float lookSensitivity = 2f;

    private CharacterController controller;
    private float verticalVelocity;
    private float pitch;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        HandleLook();
        HandleMovement();
    }
    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -75f, 75f);

        if (cameraPivot != null)
        {
            cameraPivot.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }

    }
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        float currentSpeed = walkSpeed;

        if (intoxicationSystem != null)
        {
            currentSpeed *= Mathf.Lerp(1f, 0.05f, intoxicationSystem.intoxication);
        }
        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}