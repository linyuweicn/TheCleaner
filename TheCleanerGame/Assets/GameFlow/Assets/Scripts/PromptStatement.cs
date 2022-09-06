using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class PromptStatement
{
    #region variables
    //json variables
    public string text;
    public PitchTypes pitchType; //the prompt it corresponds to
    public int promptNo;

    //private variables
    int slots;
    List<Answer> answers;
    #endregion

    #region constructor
    public void Construct()
    {
        slots = 0;
        answers = new List<Answer>();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                slots++;
                answers.Add(null);
            }
        }
    }
    #endregion
}
