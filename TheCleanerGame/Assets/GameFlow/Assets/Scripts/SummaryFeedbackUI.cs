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
    private List<int> usedNumbers = new List<int>();

    [Serializable]
    private class SummaryUI
    {
        public Vector2 scoreRange;
        //public string feedbackText;
        public string[]feedbackTextSU;// I changed to array because I want to randomize some comments.
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
        /* PromptObject prompt = BrainstormGeneralManager.Instance.Prompt;
         for (int i = 0; i < prompt.Answers.Count; i++)
         {
             totalScore += prompt.Answers[i][0].totalscore;
             Debug.Log(totalScore + "total score in summary feedback");
         }*/

       
        int count = 0;

        foreach (Dictionary<int, PromptObject> p in PromptManager.Instance.PromptDictionary.Values)
        {
            foreach (PromptObject o in p.Values)
            {
                foreach (List<AnswerObject> a in o.Answers)
                {
                    if (a[0] != null)
                    {
                        totalScore += a[0].totalscore;
                        count++;

                        
                    }
                }
            }
        }

        if (count == 0) return;

        foreach (SummaryUI su in summaryUIList)
        {
            if ((totalScore/count) >= su.scoreRange.x && (totalScore/count) <= su.scoreRange.y)
            {
                
                feedbackText.text = su.feedbackTextSU[GetRandomNumber()]; // have 3 comments in total and randomly select them so it doesn't feel repetitive
                   
                brainstormManager.SwitchFeedbackState(FeedbackType.Summary);
                Show();
                Debug.Log((totalScore/count) + "total score in summary feedback");
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

    

    public int GetRandomNumber()
    {
        int number;

        do
        {
            number = UnityEngine.Random.Range(0, 3);
        }
        while (usedNumbers.Contains(number));

        usedNumbers.Add(number);

        if (usedNumbers.Count >= 3)
        {
            usedNumbers.Clear();
        }

        return number;
    }
}
