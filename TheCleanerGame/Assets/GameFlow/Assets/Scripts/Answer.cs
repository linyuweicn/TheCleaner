using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable] public class Answer
{
    #region Variables
    //json variables
    public string text;

    public float satisfaction;
    public float censorFulfillment;
    public float innovation;
    public float production;

    public int promptNo;
    public PitchTypes pitchType;
    public AnswerTypes answerType;

    //non-json variables
    public int ranking;
    public bool calculated;
    #endregion

    #region Constructor
    public Answer(string text, float satisfaction, float censorFulfillment, float innovation, float production, int type, int promptNo, int slotNo)
    {
        this.text = text;
        this.satisfaction = satisfaction;
        this.censorFulfillment = censorFulfillment;
        this.innovation = innovation;
        this.production = production;
        this.pitchType = (PitchTypes)type;
        this.promptNo = promptNo;

        ranking = -1;
        calculated = false;
    }

    public Answer()
    {
        ranking = -1;
        calculated = false;
    }
    #endregion
}

