using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Prompt", menuName = "Prompt", order = 1)]
public class PromptObject : ScriptableObject
{
    #region variables
    [Header("Prompt Identification")]
    [Tooltip("First value is which day, second value is the nth prompt of that day")]
    [SerializeField] Vector2Int id;
    [SerializeField] PromptType promptType;
    [SerializeField] [TextArea(2, 20)] string text; //don't modify this
    string displayText;
    public bool completed;

    [Header("Answers")]
    [SerializeField] public List<NestedList> AnswersStorage; //don't modify this
    public List<List<AnswerObject>> Answers;
    public List<string> segments;

    //Answers.count
    #region private class
    [Serializable]
    public class NestedList
    {
        public List<AnswerObject> answerColumn;
    }
    #endregion

    public Vector2Int ID
    {
        get { return id; }
    }
    public PromptType Type
    {
        get { return promptType; }
    }
    public string Text
    {
        get { return displayText; }
    }
    #endregion

    #region initialization

    private void OnEnable()
    {
        Answers = new List<List<AnswerObject>>();
        for (int i = 0; i < AnswersStorage.Count; i++)
        {
            Answers.Add(new List<AnswerObject>());
            Answers[i].Add(null); //0 is null to indicate ranking
            for (int j = 0; j < AnswersStorage[i].answerColumn.Count; j++)
            {
                Answers[i].Add(AnswersStorage[i].answerColumn[j]);
            }
        }
        completed = false;
        displayText = text;
        ParseSegment();
    }

    void ParseSegment()
    {
        int start = 0;
        segments = new List<string>();
        for (int c = 0; c < text.Length; c++)
        {
            if (text[c] == '[')
            {
                segments.Add(text.Substring(start, c - start));
                start = c + 1;
            }
            else if (text[c] == ']')
            {
                start = c + 1;
                
            }
        }
        segments.Add(text.Substring(start, text.Length - start));
    }

    #endregion

    #region public functions
    public void SwapRankings(int column, int here, int other)
    {
        AnswerObject temp = Answers[column][here];
        Answers[column][here] = Answers[column][other];
        Answers[column][other] = temp;
    }

    public void FillInPromptPlaceholders()
    {
        displayText = segments[0];
        for (int i = 1; i < segments.Count; i++)
        {
            if (i - 1 < Answers.Count)
            {
                displayText += Answers[i - 1][0].text;
            }

            displayText += (segments[i]);
        }
    }
    #endregion

    #region Get/Set Functions
    
    #endregion
}
