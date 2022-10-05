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

    [Header("Answers")]
    [SerializeField] List<AnswerObject> firstColumnAnswers;
    [SerializeField] List<AnswerObject> secondColumnAnswers;
    [SerializeField] List<AnswerObject> thirdColumnAnswers;

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

    #region answerobject class
    [Serializable]
    private class AnswerObject
    {
        [Header("Answer Variables")]
        [SerializeField] public string text;
        [SerializeField] public float satisfaction;
        [SerializeField] public float censorFulfillment;
        [SerializeField] public float innovation;
        [SerializeField] public float production;

        [Header("Feedback Variables")]
        [SerializeField] public FeedbackType feedbackType;
        [SerializeField] public string feedbackText;
        [SerializeField] public CriticType criticType;
    }
    #endregion


}
