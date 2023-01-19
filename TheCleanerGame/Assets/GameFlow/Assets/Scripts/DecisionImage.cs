using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionImage : BrainstormPanelUI
{
    [SerializeField] private Image image;
    [SerializeField] private Animator animator;

    private void Start()
    {
        brainstormManager.EventManager.OnAnswerRankedTop += SetImageFromTopAnswer;
    }

    public override void Hide()
    {
        image.enabled = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public override void Show()
    {
        image.enabled = true;
    }

    IEnumerator SlideUpAnimation()
    {
        image.enabled = true;
        animator.SetTrigger("SlideUp");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SlideUp"));
        Show();
    }

    IEnumerator FadeInAnimation()
    {
        image.enabled = true;
        animator.SetTrigger("FadeIn");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"));
        Show();
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (oldState == BrainstormState.Menu && newState == BrainstormState.Decision)
        {
            StartCoroutine(SlideUpAnimation());
        }
        else if (oldState == BrainstormState.Rank && newState == BrainstormState.Decision)
        {
            StartCoroutine(FadeInAnimation());
        }
        else
        {
            Hide();
        }
    }

    public void SetImageFromTopAnswer(AnswerBox answerbox)
    {
        AnswerObject answer = answerbox.GetAnswer();
        if (answer != null && answer.image != null) { image.sprite = answer.image; }
    }
}
