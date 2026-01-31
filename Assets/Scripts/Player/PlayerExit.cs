using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GameManager.Instance.AllCollectablesFound)
        {
            GameManager.Instance.WinGame();
        }
    }
}
