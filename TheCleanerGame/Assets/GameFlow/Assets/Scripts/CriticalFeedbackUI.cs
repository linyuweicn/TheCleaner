using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CriticalFeedbackUI : FeedbackUI
{
    [SerializeField] GameObject displayBubble;
    [SerializeField] Image criticImage;
    [SerializeField] TextMeshProUGUI feedbackName;
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] List<CriticalUI> CriticalUIList;
    Dictionary<CriticType, CriticalUI> CriticalDictionary;

    [Serializable]
    private class CriticalUI
    {
        public CriticType criticType;
        public Sprite image;
    }

    private void Start()
    {
        CriticalDictionary = new Dictionary<CriticType, CriticalUI>();
        foreach (CriticalUI cu in CriticalUIList)
        {
            CriticalDictionary.Add(cu.criticType, cu);
        }
        feedbackUIContainer.AddToFeedbackDictionary(this);
    }

    public override void Trigger(AnswerObject answer)
    {
        displayBubble.SetActive(true);
        criticImage.sprite = CriticalDictionary[answer.criticType].image;
        feedbackName.text = answer.criticType.ToString();
        feedbackText.text = answer.feedbackText;
        brainstormManager.SwitchFeedbackState(FeedbackType.Critical);
        feedbackUIContainer.AddTriggeredAnswer(answer);
        Show();
    }

    public override void OnClick(FeedbackButton button)
    {
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
        Hide();
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
