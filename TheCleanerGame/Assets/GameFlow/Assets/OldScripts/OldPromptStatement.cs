using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class OldPromptStatement
{
    #region variables
    //json variables
    public string text;
    public OldPitchTypes pitchType; //the prompt it corresponds to
    public int promptNo;

    //private variables
    int slots;
    List<OldAnswer> answers;
    #endregion

    #region constructor
    public void Construct()
    {
        slots = 0;
        answers = new List<OldAnswer>();

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
