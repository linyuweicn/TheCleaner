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
    [SerializeField] string text;
    public bool completed;

    [Header("Answers")]
    [SerializeField] public List<NestedList> AnswersStorage; //don't modify this
    public List<List<AnswerObject>> Answers;

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
        get { return text; }
    }
    #endregion

    #region initialization

    private void OnEnable()
    {
        Answers = new List<List<AnswerObject>>();
        for (int i = 0; i < AnswersStorage.Count; i++)
        {
            Answers.Add(new List<AnswerObject>());
            for (int j = 0; j < AnswersStorage[i].answerColumn.Count; j++)
            {
                Answers[i].Add(AnswersStorage[i].answerColumn[j]);
            }
        }
        completed = false;
    }

    #endregion

    #region public functions
    public void SwapRankings(int column, int here, int other)
    {
        AnswerObject temp = Answers[column][here];
        Answers[column][here] = Answers[column][other];
        Answers[column][other] = temp;
    }
    #endregion

    #region Get/Set Functions
    
    #endregion
}
