using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OldPromptNextButton : MonoBehaviour
{
    #region variables
    [SerializeField] Color orange;
    [SerializeField] Color blue;
    [SerializeField] Color purple;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;
    OldPromptButtonStates state;
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

        ChangeStateTo(OldPromptButtonStates.Next);

        if (OldGeneralFlowStateManager.instance.currentPrompt != null)
        {
            switch (OldGeneralFlowStateManager.instance.currentPrompt.pitchType)
            {
                case OldPitchTypes.Theme:
                    img.color = orange;
                    break;
                case OldPitchTypes.Character:
                    img.color = blue;
                    break;
                case OldPitchTypes.Detail:
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

    public void ChangeStateTo(OldPromptButtonStates state)
    {
        this.state = state;
        switch (state)
        {
            case OldPromptButtonStates.Next:
                text.text = "Next";
                break;
            case OldPromptButtonStates.Finish:
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
            case OldPromptButtonStates.Next:
                OldGeneralFlowStateManager.instance.answerManager.CullAnswers();
                OldGeneralFlowStateManager.instance.ChangePromptText(OldGeneralFlowStateManager.instance.currentPrompt.statement.text);
                ChangeStateTo(OldPromptButtonStates.Finish);
                break;
            case OldPromptButtonStates.Finish:
                OldGeneralFlowStateManager.instance.scoreManager.CalculateScore();
                OldGeneralFlowStateManager.instance.TransitionToDefault(OldGeneralFlowStateManager.instance.focusedContainer);
                break;
        }
    }
    #endregion

}
