using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, ILookingCharacter
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [Space]

    [SerializeField]
    private InputActionReference moveInput;
    [SerializeField]
    private Rigidbody rigidbody;

    public Vector2 LookDirection { get;private set; }

    private void OnEnable()
    {
        moveInput.action.Enable();
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing)
        {
            return;
        }

        LookDirection = moveInput.action.ReadValue<Vector2>();
        Vector3 moveAmount = new Vector3(LookDirection.x, 0.0f, LookDirection.y);

        rigidbody.MovePosition(rigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }
}
