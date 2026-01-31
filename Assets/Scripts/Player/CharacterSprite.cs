using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField]
    private float fps = 9;

    [SerializeField]
    private Sprite[] upStaticMask;
    [SerializeField]
    private Sprite[] rightStaticMask;
    [SerializeField]
    private Sprite[] downStaticMask;

    [SerializeField]
    private Sprite[] upMovingMask;
    [SerializeField]
    private Sprite[] rightMovingMask;
    [SerializeField]
    private Sprite[] downMovingMask;

    [Space]

    [SerializeField]
    private Sprite[] upStaticNoMask;
    [SerializeField]
    private Sprite[] rightStaticNoMask;
    [SerializeField]
    private Sprite[] downStaticNoMask;

    [SerializeField]
    private Sprite[] upMovingNoMask;
    [SerializeField]
    private Sprite[] rightMovingNoMask;
    [SerializeField]
    private Sprite[] downMovingNoMask;


    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerMask playerMask;

    private ILookingCharacter movingCharacter;
    private bool characterHasMask;
    private float frameInterval;

    private void Awake()
    {
        movingCharacter = GetComponentInParent<ILookingCharacter>();
        if(movingCharacter == null)
        {
            Debug.LogError("No character move script found");
        }
        characterHasMask = playerMask != null;
        frameInterval = 1.0f / fps;
    }

    private void Update()
    {
        if(!characterHasMask || playerMask.MaskMode)
        {
            if (movingCharacter.IsMoving)
            {
                UpdateAnimation(upMovingMask, rightMovingMask, downMovingMask);
            }
            else
            {
                UpdateAnimation(upStaticMask, rightStaticMask, downStaticMask);
            }
        }
        else
        {
            if (movingCharacter.IsMoving)
            {
                UpdateAnimation(upMovingNoMask, rightMovingNoMask, downMovingNoMask);
            }
            else
            {
                UpdateAnimation(upStaticNoMask, rightStaticNoMask, downStaticNoMask);
            }
        }
    }

    private void UpdateAnimation(Sprite[] up, Sprite[] right, Sprite[] down)
    {
        Vector2 direction = movingCharacter.LookDirection;
        if(direction.magnitude < 0.01f)
        {
            return;
        }
        

        Sprite[] targetFrames = up;
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            targetFrames = right;
        }
        else if(direction.y < 0)
        {
            targetFrames = down;
        }

        int frameIndex = Mathf.FloorToInt(Time.time / frameInterval) % targetFrames.Length;
        spriteRenderer.sprite = targetFrames[frameIndex];
        Vector3 newScale = transform.localScale;
        newScale.x = direction.x > 0 ? 1 : -1;
        transform.localScale = newScale;
    }
}
