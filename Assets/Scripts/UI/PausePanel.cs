using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private InputActionReference togglePause;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void OnEnable()
    {
        togglePause.action.Enable();
    }

    private void Update()
    {
        if(togglePause.action.WasPressedThisFrame())
        {
            if(root.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Show()
    {
        root.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        root.SetActive(false);
        Time.timeScale = 1;
    }
}
