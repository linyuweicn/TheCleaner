using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CardContainer : BrainstormPanelUI
{
    #region variables
    [SerializeField] PromptObject associatedPrompt;
    [SerializeField] TextMeshProUGUI fractionText;
    [SerializeField] GameObject card;
    [SerializeField] Animator animator;

    AudioManager audioManager;
    BrainstormTutorial brainstormTutorial;

    [SerializeField] bool mouseOver;

    public PromptObject Prompt { get { return associatedPrompt; } }
    public int PromptID { get { return associatedPrompt.ID.y; } }
    #endregion

    #region initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        brainstormTutorial = FindObjectOfType<BrainstormTutorial>();

        BrainstormGeneralManager.Instance.AddContainerToGeneralManager(this);
        PromptManager.Instance.AddPrompt(Prompt);

        UpdateText();
    }
    #endregion

    #region Update

    private void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickedOn();
            }
        }
    }

    #endregion

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        UpdateText();

        if (newState == BrainstormState.Menu)
        {
            StartCoroutine(Enter());
        }
        else
        {
            StartCoroutine(Exit());
        }
    }

    public override void Hide()
    {
        card.SetActive(false);
    }

    public override void Show()
    {
        card.SetActive(true);
    }

    #region helper functions

    IEnumerator Exit()
    {
        animator.SetTrigger("Exit");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        Hide();
    }

    IEnumerator Enter()
    {
        animator.SetTrigger("Enter");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
        Show();
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
    }

    void UpdateText()
    {
        fractionText.text = PromptManager.Instance.GetCompletedPromptCount(Prompt.Type) + " / " + PromptManager.Instance.GetTotalPromptCount(Prompt.Type);
    }

    #endregion

    #region MouseClick Interface

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    public void ClickedOn()
    {
        if (brainstormManager.MyBrainstormState == BrainstormState.Menu)
        {
            brainstormManager.AssignPrompt(associatedPrompt);

            if (associatedPrompt.completed)
            {
                brainstormManager.SwitchState(BrainstormState.Decision);
            }
            else
            {
                brainstormManager.SwitchState(BrainstormState.Rank);
            }
        }

        if (audioManager != null)
        {
            audioManager.PlayUiSound("ui_confirm");
        }

        if (brainstormTutorial) { brainstormTutorial.StartPanelTutorial(); }
    }

    #endregion
}
