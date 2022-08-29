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

    public PitchTypes type;
    public int promptNo;
    public AnswerTypes answerType;

    //non-json variables
    public int ranking;
    #endregion

    #region Constructor
    public Answer(string text, float satisfaction, float censorFulfillment, float innovation, float production, int type, int promptNo, int slotNo)
    {
        this.text = text;
        this.satisfaction = satisfaction;
        this.censorFulfillment = censorFulfillment;
        this.innovation = innovation;
        this.production = production;
        this.type = (PitchTypes)type;
        this.promptNo = promptNo;
    }
    #endregion
}

