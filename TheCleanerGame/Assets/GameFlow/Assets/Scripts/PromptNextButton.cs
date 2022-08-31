using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PromptNextButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Color orange;
    [SerializeField] Color blue;
    [SerializeField] Color purple;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;
    PromptButtonStates state;
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        img.enabled = true;
        text.enabled = true;

        ChangeStateTo(PromptButtonStates.Next);

        switch (GeneralFlowStateManager.instance.currentPrompt.pitchType)
        {
            case PitchTypes.Theme:
                img.color = orange;
                break;
            case PitchTypes.Character:
                img.color = blue;
                break;
            case PitchTypes.Detail:
                img.color = purple;
                break;
        }
    }

    public void Hide()
    {
        img.enabled = false;
        text.enabled = false;
    }

    public void ChangeStateTo(PromptButtonStates state)
    {
        this.state = state;
        switch (state)
        {
            case PromptButtonStates.Next:
                text.text = "Next";
                break;
            case PromptButtonStates.Finish:
                text.text = "Finish";
                break;
            default:
                break;
        }
    }

    public void ClickedOn()
    {
        switch (state)
        {
            case PromptButtonStates.Next:
                GeneralFlowStateManager.instance.answerManager.CullAnswers();
                ChangeStateTo(PromptButtonStates.Finish);
                break;
            case PromptButtonStates.Finish:
                GeneralFlowStateManager.instance.scoreManager.CalculateScore();
                GeneralFlowStateManager.instance.TransitionToDefault(GeneralFlowStateManager.instance.focusedContainer);
                break;
        }
    }
}
