using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BrainstormEventManager : MonoBehaviour
{
    public Action OnEnterMenuState;
    public Action OnExitMenuState;
    public Action OnEnterRankingState;
    public Action OnExitRankingState;
    public Action OnEnterDecisionState;
    public Action OnExitDecisionState;

    public Action OnOpenFeedback;
    public Action OnCloseFeedback;

    public delegate void CardContainerAction(CardContainer cardContainer);

    public delegate void PromptObjectAction(PromptObject promptObject);
    public PromptObjectAction OnSelectPromptObject;

    public delegate void AnswerBoxAction(AnswerBox answerObject);
    public AnswerBoxAction OnAnswerRankedTop;

    public void OnEnterMenuStateEvent()
    {
        if (OnEnterMenuState != null) { OnEnterMenuState(); }
    }

    public void OnExitMenuStateEvent()
    {
        if (OnExitMenuState != null) { OnExitMenuState(); }
    }

    public void OnEnterRankingStateEvent()
    {
        if (OnEnterRankingState != null) { OnEnterRankingState(); }
    }

    public void OnExitRankingStateEvent()
    {
        if (OnExitRankingState != null) { OnExitRankingState(); }
    }

    public void OnEnterDecisionStateEvent()
    {
        if (OnEnterDecisionState != null) { OnEnterDecisionState(); }
    }

    public void OnExitDecisionStateEvent()
    {
        if (OnExitDecisionState != null) { OnExitDecisionState(); }
    }

    public void OnOpenFeedbackEvent()
    {
        if (OnOpenFeedback != null) { OnOpenFeedback(); }
    }

    public void OnCloseFeedbackEvent()
    {
        if (OnCloseFeedback != null) { OnCloseFeedback(); }
    }

    public void OnSelectPromptObjectEvent(PromptObject promptObject)
    {
        if (OnSelectPromptObject != null) { OnSelectPromptObject(promptObject); }
    }

    public void OnAnswerRankedTopEvent(AnswerBox answerBox)
    {
        if (OnAnswerRankedTop != null) { OnAnswerRankedTop(answerBox); }
    }
}
