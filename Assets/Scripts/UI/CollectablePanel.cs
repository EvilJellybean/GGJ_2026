using System;
using TMPro;
using UnityEngine;

public class CollectablePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private TMP_Text collectableLabel;

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerDie += GameManager_OnPlayerDie;
        GameManager.Instance.OnCollectableCollected += GameManager_OnCollectableCollected;
        UpdateUI();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDie -= GameManager_OnPlayerDie;
            GameManager.Instance.OnCollectableCollected -= GameManager_OnCollectableCollected;
        }
    }

    private void UpdateUI()
    {
        collectableLabel.text = $"Cats Found: {GameManager.Instance.CurrentCollectables}/{GameManager.Instance.MaxCollectables}";
    }

    private void GameManager_OnPlayerDie()
    {
        root.SetActive(false);
    }

    private void GameManager_OnCollectableCollected()
    {
        UpdateUI();
    }
}
