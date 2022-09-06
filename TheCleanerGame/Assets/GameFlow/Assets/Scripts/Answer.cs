using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable] public class Answer //constructed using Jsonfile in AnswerManager
{
    #region Variables
    //json variables
    public string text;

    public float satisfaction;
    public float censorFulfillment;
    public float innovation;
    public float production;

    public int promptNo; //which prompt it belongs to
    public PitchTypes pitchType;
    public AnswerTypes answerType;

    //non-json variables
    public int ranking;
    public bool calculated; //is it computed in ScoreManager
    #endregion

    #region Constructor
    public Answer()
    {
        ranking = -1;
        calculated = false;
    }
    #endregion
}

