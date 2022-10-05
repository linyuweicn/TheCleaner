using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable] public class OldPrompt
{
    #region Variables
    //json variables
    public string text;
    public OldPitchTypes pitchType; //we store prompts with pitchType and promptNo
    public int promptNo;

    //non-jsonable variables
    public Dictionary<OldAnswerTypes, List<OldAnswer>> answerDictionary;
    public bool visited = false; //did we visit this prompt?
    public bool calculated = false; //did we claculate this prompt
    public OldPromptStatement statement; //its corresponding promptStatement

    #endregion
    #region Constructor
    public OldPrompt()
    {
        answerDictionary = new Dictionary<OldAnswerTypes, List<OldAnswer>>();
        foreach (OldAnswerTypes t in Enum.GetValues(typeof(OldAnswerTypes)))
        {
            answerDictionary.Add(t, new List<OldAnswer>());
        }
    }
    public OldPrompt(string text)
    {
        this.text = text;

        answerDictionary = new Dictionary<OldAnswerTypes, List<OldAnswer>>();
        foreach (OldAnswerTypes t in Enum.GetValues(typeof(OldAnswerTypes)))
        {
            answerDictionary.Add(t, new List<OldAnswer>());
        }
    }
    #endregion

}
