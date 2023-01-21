using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SummaryFeedbackUI : FeedbackUI
{
    [SerializeField] GameObject displayBubble;
    [SerializeField] Image criticImage;
    [SerializeField] TextMeshProUGUI feedbackName;
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] List<SummaryUI> summaryUIList;

    [Serializable]
    private class SummaryUI
    {
        public Vector2 scoreRange;
        public string feedbackText;
    }

    void Start()
    {
        feedbackUIContainer.AddToFeedbackDictionary(this);
        brainstormManager.EventManager.OnEnterDecisionState += Trigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger()
    {
        float totalScore = 0;
        PromptObject prompt = BrainstormGeneralManager.Instance.Prompt;
        for (int i = 0; i < prompt.Answers.Count; i++)
        {
            totalScore += prompt.Answers[i][0].totalscore;
            Debug.Log(totalScore);
        }

        foreach (SummaryUI su in summaryUIList)
        {
            if (totalScore >= su.scoreRange.x && totalScore <= su.scoreRange.y)
            {
                feedbackText.text = su.feedbackText;
                brainstormManager.SwitchFeedbackState(FeedbackType.Summary);
                Show();
                break;
            }
        }
    }

    public override void OnClick(FeedbackButton button)
    {
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
        Hide();
    }
    public override void Trigger(AnswerObject answer)
    {
        
    }
    public override void Show()
    {
        displayBubble.SetActive(true);
    }
    public override void Hide()
    {
        displayBubble.SetActive(false);
    }
}
