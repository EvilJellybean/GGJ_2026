using System;
using TMPro;
using UnityEngine;

public class CollectablePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private TMP_Text collectableLabel;
    [SerializeField]
    private TMP_Text goToExitLabel;
    [SerializeField]
    private Color objectiveCompleteColor = Color.green;

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
        collectableLabel.text = $"{GameManager.Instance.CurrentCollectables}/{GameManager.Instance.MaxCollectables}";

        bool allCollectablesFound = GameManager.Instance.AllCollectablesFound;

        collectableLabel.color = allCollectablesFound ? objectiveCompleteColor : Color.white;
        goToExitLabel.gameObject.SetActive(allCollectablesFound);
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
