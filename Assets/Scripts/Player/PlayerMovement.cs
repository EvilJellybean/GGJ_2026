using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMovingCharacter
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [Space]

    [SerializeField]
    private InputActionReference moveInput;
    [SerializeField]
    private Rigidbody rigidbody;

    public Vector2 MoveDirection { get;private set; }

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

        MoveDirection = moveInput.action.ReadValue<Vector2>();
        Vector3 moveAmount = new Vector3(MoveDirection.x, 0.0f, MoveDirection.y);

        rigidbody.MovePosition(rigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }
}
