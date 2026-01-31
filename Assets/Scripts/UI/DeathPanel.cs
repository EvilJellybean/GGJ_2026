using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private Button restartButton;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerDie += GameManager_OnPlayerDie;
        restartButton.onClick.AddListener(Restart_OnClick);
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDie -= GameManager_OnPlayerDie;
        }
        restartButton.onClick.RemoveListener(Restart_OnClick);
    }

    private void GameManager_OnPlayerDie()
    {
        root.SetActive(true);
        restartButton.Select();
    }

    private void Restart_OnClick()
    {
        GameManager.Instance.RestartScene();
    }
}
