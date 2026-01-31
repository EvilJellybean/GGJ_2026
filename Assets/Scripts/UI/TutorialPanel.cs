using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPanel : MonoBehaviour
{
    private enum State
    {
        PutOnMask,
        Explanation,
        None
    }

    [SerializeField]
    private GameObject root;

    [SerializeField]
    private GameObject tutorialPutOnMask;
    [SerializeField]
    private InputActionReference toggleMaskInput;
    [SerializeField]
    private GameObject tutorialMaskExplanation;
    [SerializeField]
    private float explanationSeconds = 5;

    private State state = State.PutOnMask;
    private float explanationSecondsLeft;

    private void Awake()
    {
        tutorialPutOnMask.SetActive(true);
        tutorialMaskExplanation.SetActive(false);
    }

    private void Update()
    {
        switch(state)
        {
            case State.PutOnMask:
                if(toggleMaskInput.action.WasPressedThisFrame())
                {
                    state = State.Explanation;
                    tutorialPutOnMask.SetActive(false);
                    tutorialMaskExplanation.SetActive(true);
                    explanationSecondsLeft = explanationSeconds;
                }
                break;
            case State.Explanation:
                explanationSeconds -= Time.unscaledDeltaTime;
                if(explanationSeconds <= 0)
                {
                    state = State.None;
                    tutorialMaskExplanation.SetActive(false);
                }
                break;
        }
    }
}
