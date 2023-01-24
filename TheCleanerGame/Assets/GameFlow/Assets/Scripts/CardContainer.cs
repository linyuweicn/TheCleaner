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
    [SerializeField] TextMeshProUGUI mouseOverPrompt;
    [SerializeField] TextMeshProUGUI mouseOverName;
    [SerializeField] GameObject card;
    [SerializeField] SpriteRenderer image;
    [SerializeField] Animator translationAnimator;
    [SerializeField] Animator mouseAnimator;

    AudioManager audioManager;
    BrainstormTutorial brainstormTutorial;
    Vector3 recordedPos;

    [SerializeField] bool mouseOver;
    private bool clickedOn;

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

        mouseOverName.text = associatedPrompt.Type.ToString();
        mouseOverPrompt.text = associatedPrompt.Text;

        recordedPos = transform.localPosition;
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
            clickedOn = false;
            brainstormManager.EventManager.OnAnswerRankedTop -= ReassignImage;
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
        recordedPos = transform.localPosition;
        translationAnimator.SetTrigger("Exit");
        yield return new WaitUntil(() => translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        yield return new WaitUntil(() => !translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        //Hide();
        transform.localPosition = new Vector3(3000, 3000);
    }

    IEnumerator Enter()
    {
        translationAnimator.SetTrigger("Enter");
        yield return new WaitUntil(() => translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
        Show();
        transform.localPosition = recordedPos;
        yield return new WaitUntil(() => !translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
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
        mouseAnimator.SetTrigger("MouseEnter");
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        mouseAnimator.SetTrigger("MouseExit");
    }

    public void ClickedOn()
    {
        if (brainstormManager.MyBrainstormState == BrainstormState.Menu)
        {
            brainstormManager.AssignPrompt(associatedPrompt);
            clickedOn = true;

            if (associatedPrompt.completed)
            {
                brainstormManager.SwitchState(BrainstormState.Decision);
            }
            else
            {
                brainstormManager.SwitchState(BrainstormState.Rank);
            }

            brainstormManager.EventManager.OnAnswerRankedTop += ReassignImage;
        }

        if (audioManager != null)
        {
            audioManager.PlayUiSound("ui_confirm");
        }

        if (brainstormTutorial) { brainstormTutorial.StartPanelTutorial(); }
    }

    private void ReassignImage(AnswerBox answerBox)
    {
        AnswerObject answer = answerBox.GetAnswer();

        if (answer != null && answer.image != null)
        {
            image.sprite = answer.image;
        }
    }

    #endregion
}
