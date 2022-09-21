using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PromptNextButton : MonoBehaviour
{
    #region variables
    [SerializeField] Color orange;
    [SerializeField] Color blue;
    [SerializeField] Color purple;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;
    PromptButtonStates state;
    #endregion

    #region initialization
    void Start()
    {
        Hide();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {

    }
    #region visual state changer
    public void Show()
    {
        img.enabled = true;
        text.enabled = true;

        ChangeStateTo(PromptButtonStates.Next);

        if (GeneralFlowStateManager.instance.currentPrompt != null)
        {
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

    #endregion

    #region handles when clicked
    public void ClickedOn()
    {
        switch (state)
        {
            case PromptButtonStates.Next:
                GeneralFlowStateManager.instance.answerManager.CullAnswers();
                GeneralFlowStateManager.instance.ChangePromptText(GeneralFlowStateManager.instance.currentPrompt.statement.text);
                ChangeStateTo(PromptButtonStates.Finish);
                break;
            case PromptButtonStates.Finish:
                GeneralFlowStateManager.instance.scoreManager.CalculateScore();
                GeneralFlowStateManager.instance.TransitionToDefault(GeneralFlowStateManager.instance.focusedContainer);
                break;
        }
    }
    #endregion

}
