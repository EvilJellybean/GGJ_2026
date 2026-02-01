using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverClickSfx : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    [SerializeField]
    private float hoverVolume = 0.1f;
    [SerializeField]
    private float clickVolume = 0.9f;
    [SerializeField]
    private List<AudioClip> hoverSfx = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> clickSfx;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetOrAddComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(clickSfx.Count >0)
        {
            audioSource.PlayOneShot(clickSfx[Random.Range(0, clickSfx.Count)], clickVolume);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSfx.Count > 0)
        {
            audioSource.PlayOneShot(hoverSfx[Random.Range(0, hoverSfx.Count)], hoverVolume);
        }
    }
}
