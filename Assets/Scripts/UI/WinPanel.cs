using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
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
        GameManager.Instance.OnPlayerWin += GameManager_OnPlayerWin;
        restartButton.onClick.AddListener(Restart_OnClick);
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDie -= GameManager_OnPlayerWin;
        }
        restartButton.onClick.RemoveListener(Restart_OnClick);
    }

    private void GameManager_OnPlayerWin()
    {
        root.SetActive(true);
        restartButton.Select();
    }

    private void Restart_OnClick()
    {
        GameManager.Instance.RestartScene();
    }
}
