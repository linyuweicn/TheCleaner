using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable] public class Prompt
{
    #region Variables
    //json variables
    public string text;
    public PitchTypes pitchType;
    public int promptNo;

    //non-jsonable variables
    public Dictionary<AnswerTypes, List<Answer>> answerDictionary;
    public bool visited = false;
    public bool calculated = false;

    #endregion
    #region Constructor
    public Prompt()
    {
        answerDictionary = new Dictionary<AnswerTypes, List<Answer>>();
        foreach (AnswerTypes t in Enum.GetValues(typeof(AnswerTypes)))
        {
            answerDictionary.Add(t, new List<Answer>());
        }
    }
    public Prompt(string text)
    {
        this.text = text;

        answerDictionary = new Dictionary<AnswerTypes, List<Answer>>();
        foreach (AnswerTypes t in Enum.GetValues(typeof(AnswerTypes)))
        {
            answerDictionary.Add(t, new List<Answer>());
        }
    }
    #endregion

}
