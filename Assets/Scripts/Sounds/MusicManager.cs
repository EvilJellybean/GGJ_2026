using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicNormal;
    [SerializeField]
    private AudioSource musicChase;
    [SerializeField]
    private float blendAmount = 0.5f;
    [SerializeField]
    private float transitionSpeed = 1.0f;

    private float chaseAmount;

    private void Update()
    {
        chaseAmount = Mathf.Lerp(chaseAmount, Mathf.Clamp01(GameManager.Instance.DangerAmount * blendAmount), transitionSpeed * Time.deltaTime);
        musicNormal.volume = 1 - chaseAmount;
        musicChase.volume = chaseAmount;
    }
}
