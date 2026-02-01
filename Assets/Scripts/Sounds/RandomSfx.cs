using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSfx : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sfx;

    [SerializeField]
    private float intervalMin = 5.0f;
    [SerializeField]
    private float intervalMax = 10.0f;

    [SerializeField]
    private float randomDistance = 0;

    private float intervalLeft;
    private AudioSource sfxSource;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
        intervalLeft = Random.Range(intervalMin, intervalMax);
        sfxSource = gameObject.GetOrAddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, randomDistance);
    }

    private void Update()
    {
        intervalLeft -= Time.deltaTime;
        if(intervalLeft < 0)
        {
            intervalLeft = Random.Range(intervalMin, intervalMax);
            PlaySfx();
        }
    }

    private void PlaySfx()
    {
        if(randomDistance > 0)
        {
            Vector3 offset = new Vector3(Random.Range(-randomDistance, randomDistance), 0.0f, Random.Range(-randomDistance, randomDistance));
            transform.localPosition = startPosition + offset;
        }
        sfxSource.PlayOneShot(sfx[Random.Range(0, sfx.Count)]);
    }
}
