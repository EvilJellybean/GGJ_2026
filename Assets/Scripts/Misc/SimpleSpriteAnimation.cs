using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    private float fps = 9;
    [SerializeField]
    private List<Sprite> frames = new List<Sprite>();
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Update()
    {
        int index = Mathf.FloorToInt(Time.time * fps) % frames.Count;
        spriteRenderer.sprite = frames[index];
    }
}
