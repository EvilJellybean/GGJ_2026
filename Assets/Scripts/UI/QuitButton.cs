using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

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
        Application.Quit();
    }
}
