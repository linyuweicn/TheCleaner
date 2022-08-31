using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class PromptStatement
{
    //json variables
    public string text;
    public PitchTypes pitchType;
    public int promptNo;

    int slots;
    List<Answer> answers;

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
}
