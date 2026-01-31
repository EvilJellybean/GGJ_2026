using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditiveScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "";

    private void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
