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
    public static Prompt active;
    public List<Answer> answers;

    public PitchTypes type;
    public int promptNo;

    //non-jsonable variables
    public Dictionary<AnswerTypes, List<Answer>> answerDictionary;
    #endregion
    #region Constructor
    public Prompt()
    {
        answers = new List<Answer>();
    }
    public Prompt(string text)
    {
        answers = new List<Answer>();

        this.text = text;

        foreach (AnswerTypes t in Enum.GetValues(typeof(AnswerTypes)))
        {
            answerDictionary.Add(t, new List<Answer>());
        }
    }
    #endregion

}
