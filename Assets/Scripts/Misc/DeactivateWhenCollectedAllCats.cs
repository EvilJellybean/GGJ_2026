using UnityEngine;

public class DeactivateWhenCollectedAllCats : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnCollectableCollected += GameManager_OnCollectableCollected;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCollectableCollected -= GameManager_OnCollectableCollected;
        }
    }

    private void GameManager_OnCollectableCollected()
    {
        if(GameManager.Instance.AllCollectablesFound)
        {
            gameObject.SetActive(false);
        }
    }
}
