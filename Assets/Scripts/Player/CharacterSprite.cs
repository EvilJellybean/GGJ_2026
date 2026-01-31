using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField]
    private float fps = 12;

    [SerializeField]
    private Sprite[] upMask;
    [SerializeField]
    private Sprite[] rightMask;
    [SerializeField]
    private Sprite[] downMask;

    [Space]

    [SerializeField]
    private Sprite[] upNoMask;
    [SerializeField]
    private Sprite[] rightNoMask;
    [SerializeField]
    private Sprite[] downNoMask;
    

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerMask playerMask;

    private bool characterHasMask;
    private float frameInterval;

    private void Awake()
    {
        characterHasMask = playerMask != null;
        frameInterval = 1.0f / fps;
    }

    private void Update()
    {
        if(!characterHasMask || playerMask.MaskMode)
        {
            UpdateAnimation(upMask, rightMask, downMask);
        }
        else
        {
            UpdateAnimation(upNoMask, rightNoMask, downNoMask);
        }
    }

    private void UpdateAnimation(Sprite[] up, Sprite[] right, Sprite[] down)
    {
        Vector2 direction = playerMovement.MoveDirection;
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
        transform.localScale = direction.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
    }
}
