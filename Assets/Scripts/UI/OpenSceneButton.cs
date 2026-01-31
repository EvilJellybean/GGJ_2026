using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSceneButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private string sceneName = "PlayScene";

    private void OnEnable()
    {
        button.onClick.AddListener(Button_OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Button_OnClick);
    }

    private void Button_OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
