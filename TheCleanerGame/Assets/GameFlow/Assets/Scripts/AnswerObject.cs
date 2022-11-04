using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnswerObject
{
    [Header("Answer Variables")]
    [SerializeField] public string text;
/*    [SerializeField] public float satisfaction;
    [SerializeField] public float censorFulfillment;
    [SerializeField] public float innovation;
    [SerializeField] public float production;*/
    [SerializeField] public float totalscore; // see the narration master sheet for details - in google drive

    [Header("Feedback Variables")]
    [SerializeField] public FeedbackType feedbackType;
    [SerializeField] [TextArea(2, 20)] public string feedbackText;
    [SerializeField] public CriticType criticType;
}
