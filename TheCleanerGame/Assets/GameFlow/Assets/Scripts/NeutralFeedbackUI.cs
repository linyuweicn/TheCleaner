using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NeutralFeedbackUI : FeedbackUI
{
    [SerializeField] List<NeutralUI> neutralUI;
    Dictionary<CriticType, NeutralUI> neutralUIDictionary;

    [Serializable]
    private class NeutralUI
    {
        public CriticType criticType;
        public GameObject displayBubble;
        public GameObject buttonBubble;
        public TextMeshProUGUI feedbackText;
        public TextMeshProUGUI feedbackName;
        public NeutralFeedbackState state;
        public AnswerObject answer;
    }

    private void Start()
    {
        neutralUIDictionary = new Dictionary<CriticType, NeutralUI>();
        feedbackUIContainer.AddToFeedbackDictionary(this);

        foreach (NeutralUI n in neutralUI)
        {
            neutralUIDictionary.Add(n.criticType, n);
        }

        feedbackUIContainer.OnFeedbackHasTriggered += Hide;
    }

    public override void Trigger(AnswerObject answer)
    {
        if (neutralUIDictionary.ContainsKey(answer.criticType))
        {
            Trigger(neutralUIDictionary[answer.criticType], answer);
        }
    }

    private void Trigger(NeutralUI n, AnswerObject answer)
    {
        n.feedbackName.text = answer.criticType.ToString();
        n.feedbackText.text = answer.feedbackText;
        n.buttonBubble.SetActive(true);
        n.answer = answer;
        n.state = NeutralFeedbackState.Triggered;
    }

    public override void Show()
    {
        foreach (NeutralUI n in neutralUI)
        {
            Show(n);
        }
    }

    private void Show(NeutralUI n)
    {
        n.displayBubble.SetActive(true);
        n.buttonBubble.SetActive(true);
        n.state = NeutralFeedbackState.Active;
        feedbackUIContainer.AddTriggeredAnswer(n.answer);
    }

    public override void Hide()
    {
        foreach(NeutralUI n in neutralUI)
        {
            Hide(n);
        }
    }

    private void Hide(NeutralUI n)
    {
        n.displayBubble.SetActive(false);
        n.buttonBubble.SetActive(false);
        n.state = NeutralFeedbackState.Inactive;
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
    }

    public override void OnClick(FeedbackButton button)
    {
        if (brainstormManager.MyFeedbackState == FeedbackType.Null || brainstormManager.MyFeedbackState == FeedbackType.Neutral)
        {
            brainstormManager.SwitchFeedbackState(FeedbackType.Neutral);

            switch (neutralUIDictionary[button.M_CriticType].state)
            {
                case NeutralFeedbackState.Triggered:
                    Show(neutralUIDictionary[button.M_CriticType]);
                    neutralUIDictionary[button.M_CriticType].buttonBubble.SetActive(false);
                    break;
                case NeutralFeedbackState.Active:
                    Hide(neutralUIDictionary[button.M_CriticType]);
                    break;
            }
        }
    }
}
