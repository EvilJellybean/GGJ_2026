using UnityEngine;

public class FixLightingDirection : MonoBehaviour
{
    private Transform player;
    private Transform cachedTransform;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cachedTransform = transform;
    }

    private void Update()
    {
        bool flip = player.position.z > cachedTransform.position.z;

        Vector3 newScale = transform.localScale;
        newScale.z = flip ? -1 : 1;
        transform.localScale = newScale;
    }
}
