using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5.0f;

    [Space]

    [SerializeField]
    private InputActionReference moveInput;
    [SerializeField]
    private Rigidbody rigidbody;

    private void OnEnable()
    {
        moveInput.action.Enable();
    }

    private void Update()
    {
        Vector2 moveVector = moveInput.action.ReadValue<Vector2>();
        Vector3 moveAmount = new Vector3(moveVector.x, 0.0f, moveVector.y);

        Debug.Log(moveVector);

        rigidbody.MovePosition(rigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }
}
