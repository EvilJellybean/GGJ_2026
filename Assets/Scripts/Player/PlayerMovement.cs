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

    [SerializeField]
    private AudioSource footsteps;

    public Vector2 LookDirection { get;private set; }

    public bool IsMoving { get; private set; }

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

        Vector2 moveVector = moveInput.action.ReadValue<Vector2>();
        IsMoving = moveVector.magnitude > 0.01f;
        footsteps.volume = IsMoving ? 1 : 0;
        if (!IsMoving)
        {
            return;
        }

        LookDirection = moveVector;
        Vector3 moveAmount = new Vector3(LookDirection.x, 0.0f, LookDirection.y);

        rigidbody.MovePosition(rigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }
}
