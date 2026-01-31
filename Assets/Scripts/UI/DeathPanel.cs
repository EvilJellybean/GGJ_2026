using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerDie += GameManager_OnPlayerDie;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDie -= GameManager_OnPlayerDie;
        }
    }

    private void GameManager_OnPlayerDie()
    {
        root.SetActive(true);
    }
}
