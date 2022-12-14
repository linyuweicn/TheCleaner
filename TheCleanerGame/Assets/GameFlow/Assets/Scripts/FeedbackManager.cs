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

    [SerializeField] GameObject neutralFeedbackBubble;
    [SerializeField] TextMeshProUGUI neutralFeedbackText;
    [SerializeField] TextMeshProUGUI neutralFeedbackName;

    [SerializeField] GameObject criticalFeedbackBubble;
    [SerializeField] Image criticalFeedbackImage;
    [SerializeField] TextMeshProUGUI criticalFeedbackName;
    [SerializeField] TextMeshProUGUI criticalFeedbackText;
    [HideInInspector] public float censorFulfillmentScore;
    [SerializeField] TextMeshProUGUI censorFulfillmentScoreText;

    [SerializeField] List<criticInfo> criticPairs;
    Dictionary<CriticType, criticInfo> CriticDictionary;
    HashSet<AnswerObject> triggeredFeedbackAnswers;

    //for calculation scores
    
    #region private struct
    [Serializable]
    struct criticInfo
    {
        public CriticType type;
        public Sprite image;
        public GameObject redFeedbackBubble;
        public Transform neutralConversationBubbleLocation;
    }
    #endregion
    #endregion

    #region initialization
    void Start()
    {
        CriticDictionary = new Dictionary<CriticType,criticInfo>();
        triggeredFeedbackAnswers = new HashSet<AnswerObject>();
        foreach (criticInfo c in criticPairs)
        {
            CriticDictionary.Add(c.type, c);
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
        if (!triggeredFeedbackAnswers.Contains(answer))
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
            triggeredFeedbackAnswers.Add(answer);
        }
    }
    public void TriggerCriticalFeedback(CriticType type, string feedbackText)
    {
        criticalFeedbackName.text = type.ToString();
        criticalFeedbackImage.sprite = CriticDictionary[type].image;
        criticalFeedbackText.text = feedbackText;
        OpenCriticalFeedback();
    }
    public void TriggerNeutralFeedback(CriticType type, string feedbackText)
    {
        neutralFeedbackText.text = feedbackText;
        neutralFeedbackName.text = type.ToString();
        OpenRedBubble(CriticDictionary[type].redFeedbackBubble, CriticDictionary[type].neutralConversationBubbleLocation.position);
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
        foreach (criticInfo info in CriticDictionary.Values)
        {
            if (info.redFeedbackBubble != null)
            {
                info.redFeedbackBubble.SetActive(false);
            }
        }
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

    public float GetScores(AnswerObject answer)
    {
        //totalScore = 0.1f*answer.satisfaction + 0.3f*answer.innovation + 0.5f*answer.censorFulfillment - 0.1f*answer.production;
        //totalScoreText.text = totalScore.ToString();
        censorFulfillmentScore = answer.censorFulfillment;
        Debug.Log(censorFulfillmentScore);
        return censorFulfillmentScore;
    }

    #endregion

    #region helper functions

    void OpenRedBubble(GameObject redFeedbackBubble, Vector3 conversationBubblePostion)
    {
        neutralFeedbackBubble.transform.position = conversationBubblePostion;
        redFeedbackBubble.SetActive(true);
    }

    void OpenCriticalFeedback()
    {
        rankPanelManager.State = RankPanelState.Feedback;
        criticalFeedbackBubble.SetActive(true);
    }
    #endregion
}
