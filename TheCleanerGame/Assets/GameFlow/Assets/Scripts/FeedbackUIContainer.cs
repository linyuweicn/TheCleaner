using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class FeedbackUIContainer : BrainstormPanelUI
{
    #region variables
    [HideInInspector] public float censorFulfillmentScore;
    Dictionary<FeedbackType, FeedbackUI> feedbackDictionary;
    HashSet<AnswerObject> triggeredAnswers;

    public Action OnFeedbackHasTriggered;

    //for calculation scores

    #region private struct
    [Serializable]
    struct criticInfo
    {
        public CriticType type;
        public Sprite image;
    }

    [Serializable]
    struct TotalFeedbackStruct
    {
        public Vector2 range;
        public string feedbackText;
        public FeedbackType feedbackType;
    }
    #endregion
    #endregion

    #region initialization
    private void Awake()
    {
        AddToBrainstormGeneralManager();
        feedbackDictionary = new Dictionary<FeedbackType, FeedbackUI>();
    }

    void Start()
    {
        triggeredAnswers = new HashSet<AnswerObject>();
        brainstormManager.EventManager.OnAnswerRankedTop += TriggerFeedback;

    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        
    }

    public override void Hide()
    {
        foreach (FeedbackUI f in feedbackDictionary.Values)
        {
            f.Hide();
        }
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
    }

    public override void Show()
    {
        
    }

    public void TriggerFeedback(AnswerBox answerBox)
    {
        OnFeedbackHasTriggeredEvent();
        AnswerObject answer = answerBox.GetAnswer();
        if (answer != null && !triggeredAnswers.Contains(answer))
        {
            if (feedbackDictionary.ContainsKey(answer.feedbackType))
            {
                feedbackDictionary[answer.feedbackType].Trigger(answer);
            }
        }
    }

    public void AddToFeedbackDictionary(FeedbackUI feedbackUI)
    {
        feedbackDictionary.Add(feedbackUI.M_FeedbackType, feedbackUI);
    }

    public void AddTriggeredAnswer(AnswerObject answer)
    {
        triggeredAnswers.Add(answer);
    }

    public void OnFeedbackHasTriggeredEvent()
    {
        if (OnFeedbackHasTriggered != null) { OnFeedbackHasTriggered(); }
    }
}
