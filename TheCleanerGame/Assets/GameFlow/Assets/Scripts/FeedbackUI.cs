using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedbackUI : BrainstormPanelUI
{
    [SerializeField] protected FeedbackType feedbackType;
    [SerializeField] protected FeedbackUIContainer feedbackUIContainer;

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (newState != oldState) { Hide(); }
    }
    public abstract void Trigger(AnswerObject answer);
    public abstract void OnClick(FeedbackButton button);

    public FeedbackType M_FeedbackType { get { return feedbackType; } }
}
