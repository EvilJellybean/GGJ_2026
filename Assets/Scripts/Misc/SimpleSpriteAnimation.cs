using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    private float fps = 9;
    [SerializeField]
    private List<Sprite> frames = new List<Sprite>();
    [SerializeField]
    private List<Sprite> endLoopFrames = new List<Sprite>();
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Image image;
    [SerializeField]
    private bool loop = true;

    private float enableTime;
    private List<Sprite> currentFrames;

    private void OnEnable()
    {
        currentFrames = frames;

        enableTime = Time.time;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
        if(image != null)
        {
            image.enabled = true;
        }
    }

    private void Update()
    {
        int index = Mathf.FloorToInt((Time.time - enableTime) * fps);
        if(index >=  currentFrames.Count)
        {
            index = index % currentFrames.Count;

            if(endLoopFrames.Count != 0 && currentFrames != endLoopFrames)
            {
                currentFrames = endLoopFrames;
                index = Mathf.FloorToInt((Time.time - enableTime) * fps);
                index = index % endLoopFrames.Count;
            }

            if(!loop)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
                if (image != null)
                {
                    image.enabled = false;
                }
            }
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = currentFrames[index];
        }
        else
        {
            image.sprite = currentFrames[index];
        }
    }
}
