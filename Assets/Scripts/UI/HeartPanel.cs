using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPanel : MonoBehaviour
{
    [SerializeField]
    private float speedMin = 1.0f;
    [SerializeField]
    private float speedMax = 2.0f;

    [SerializeField]
    private float lineSpeedMin = 1.0f;
    [SerializeField]
    private float lineSpeedMax = 2.0f;

    [SerializeField]
    private float pitchMin = 0.6f;
    [SerializeField]
    private float pitchMax = 1.5f;

    [SerializeField]
    private List<AudioSource> audioSources;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Image pulseLine;
    [SerializeField]
    private string pulseTimeParameter = "_PulseTime";

    private Material imageMaterial;
    float pulseTime;

    private void Awake()
    {
        imageMaterial = pulseLine.material;
    }

    private void Update()
    {
        float danger = GameManager.Instance.DangerAmount;

        float lineSpeed = Mathf.LerpUnclamped(lineSpeedMin, lineSpeedMax, danger);
        pulseTime += lineSpeed * Time.deltaTime;

        float newPitch = Mathf.LerpUnclamped(pitchMin, pitchMax, danger);
        for(int i = 0; i< audioSources.Count; i++)
        {
            audioSources[i].pitch = newPitch;
        }

        imageMaterial.SetFloat(pulseTimeParameter, pulseTime);
        animator.speed = Mathf.LerpUnclamped(speedMin, speedMax, danger);
    }
}
