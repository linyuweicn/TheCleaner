using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class FeedbackManager : MonoBehaviour
{
    #region variables
    [SerializeField] RankPanelManager rankPanelManager;

    [SerializeField] GameObject redFeedbackBubble;
    [SerializeField] GameObject neutralFeedbackBubble;
    [SerializeField] TextMeshProUGUI neutralFeedbackText;
    [SerializeField] TextMeshProUGUI neutralFeedbackName;

    [SerializeField] GameObject criticalFeedbackBubble;
    [SerializeField] Image criticalFeedbackImage;
    [SerializeField] TextMeshProUGUI criticalFeedbackName;
    [SerializeField] TextMeshProUGUI criticalFeedbackText;

    [SerializeField] List<criticPair> criticPairs;
    Dictionary<CriticType, Sprite> CriticDictionary;

    #region private struct
    [Serializable]
    struct criticPair
    {
        public CriticType type;
        public Sprite image;
    }
    #endregion
    #endregion

    #region initialization
    void Start()
    {
        CriticDictionary = new Dictionary<CriticType, Sprite>();
        foreach (criticPair c in criticPairs)
        {
            CriticDictionary.Add(c.type, c.image);
        }
        ResetFeedback();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    #region Feedback Triggers
    public void TriggerFeedback(AnswerObject answer)
    {
        ResetFeedback();
        switch (answer.feedbackType)
        {
            case FeedbackType.Positive:
                TriggerCriticalFeedback(answer.criticType, answer.feedbackText);
                break;
            case FeedbackType.Neutral:
                TriggerNeutralFeedback(answer.criticType, answer.feedbackText);
                break;
            case FeedbackType.Negative:
                TriggerCriticalFeedback(answer.criticType, answer.feedbackText);
                break;
        }
    }
    public void TriggerCriticalFeedback(CriticType type, string feedbackText)
    {
        criticalFeedbackName.text = type.ToString();
        criticalFeedbackImage.sprite = CriticDictionary[type];
        criticalFeedbackText.text = feedbackText;
        OpenCriticalFeedback();
    }
    public void TriggerNeutralFeedback(CriticType type, string feedbackText)
    {
        neutralFeedbackText.text = feedbackText;
        neutralFeedbackName.text = type.ToString();
        OpenRedBubble();
    }
    public void CloseCriticalFeedback()
    {
        rankPanelManager.State = RankPanelState.Ranking;
        criticalFeedbackBubble.SetActive(false);
    }

    public void CloseNeutralBubble()
    {
        rankPanelManager.State = RankPanelState.Ranking;
        neutralFeedbackBubble.SetActive(false);
    }

    public void CloseNeutralRedBubble()
    {
        redFeedbackBubble.SetActive(false);
    }

    public void ResetFeedback()
    {
        CloseCriticalFeedback();
        CloseNeutralBubble();
        CloseNeutralRedBubble();
    }

    public void OpenNeutralFeedbackResponse()
    {
        CloseNeutralRedBubble();
        neutralFeedbackBubble.SetActive(true);
    }


    #endregion

    #region helper functions

    void OpenRedBubble()
    {
        redFeedbackBubble.SetActive(true);
    }

    void OpenCriticalFeedback()
    {
        rankPanelManager.State = RankPanelState.Feedback;
        criticalFeedbackBubble.SetActive(true);
    }
    #endregion
}
