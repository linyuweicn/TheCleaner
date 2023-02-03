using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PromptUIContainer : BrainstormPanelUI
{
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] GameObject promptHeader;
    [SerializeField] Animator animator;

    void UpdatePromptText()
    {
        Debug.LogError("Called");
        BrainstormGeneralManager.Instance.Prompt.FillInPromptPlaceholders();
        promptText.text = BrainstormGeneralManager.Instance.Prompt.Text;
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if ((oldState == BrainstormState.Rank && newState == BrainstormState.Decision))
        {
            UpdatePromptText();
        } else if (newState == BrainstormState.Rank || newState == BrainstormState.Decision)
        {
            StartCoroutine(Enter());
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        UpdatePromptText();
        promptHeader.SetActive(true);
    }

    public override void Hide()
    {
        promptHeader.SetActive(false);
    }

    IEnumerator Enter()
    {
        animator.SetTrigger("Enter");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
        Show();
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
    }
}
