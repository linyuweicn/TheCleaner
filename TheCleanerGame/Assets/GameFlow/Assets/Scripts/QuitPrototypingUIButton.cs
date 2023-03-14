using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitPrototypingUIButton : BrainstormPanelUI
{
    [SerializeField] Image image;
    [SerializeField] Animator translationAnimator;
    Vector3 recordedPos;

    private void Start()
    {
        Hide();
    }
    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (newState == BrainstormState.Menu)
        {
            if (BrainstormGeneralManager.Instance.CheckIfAllDailyPromptsHaveBeenCompleted())
            {
                StartCoroutine(Enter());
            }
            else
            {
                Hide();
            }
        }
        else if (oldState == BrainstormState.Menu)
        {
            if (BrainstormGeneralManager.Instance.CheckIfAllDailyPromptsHaveBeenCompleted())
            {
                StartCoroutine(Exit());
            }
            else
            {
                Hide();
            }
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        image.enabled = true;
    }

    public override void Hide()
    {
        image.enabled = false;
    }

    IEnumerator Exit()
    {
        recordedPos = image.transform.localPosition;
        translationAnimator.SetTrigger("Exit");
        yield return new WaitUntil(() => translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        yield return new WaitUntil(() => !translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        //Hide();
        image.transform.localPosition = new Vector3(3000, 3000);
    }

    IEnumerator Enter()
    {
        //Debug.LogError("Attempted to Enter");
        translationAnimator.SetTrigger("Enter");
        yield return new WaitUntil(() => translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
        //Debug.LogError("Attempted to Try To Enter");
        Show();
        image.transform.localPosition = recordedPos;
        yield return new WaitUntil(() => !translationAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
    }
}
