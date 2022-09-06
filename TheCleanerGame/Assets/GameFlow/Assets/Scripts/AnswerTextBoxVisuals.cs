using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerTextBoxVisuals : MonoBehaviour //handles Visuals of AnswerTextBox
{
    #region variables
    //references
    [SerializeField] AnswerTextBox ATBehavior;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image img;
    ShadowTextBox spawnedShadow;

    //colors
    [SerializeField] Color orange;
    [SerializeField] Color blue;
    [SerializeField] Color purple;
    [SerializeField] Color darkOrange;
    [SerializeField] Color darkBlue;
    [SerializeField] Color darkPurple;

    //bool
    bool darkenedColor = false;

    #endregion

    #region initialization
    void Start()
    {
        
    }

    public void Construct()
    {
        text.text = ATBehavior.answer.text;

        BecomeOriginalColor();
        GenerateShadow();
    }
    #endregion

    #region Color Changing
    public void BecomeOriginalColor()
    {
        switch (ATBehavior.answer.answerType)
        {
            case AnswerTypes.Orange:
                img.color = orange;
                break;
            case AnswerTypes.Blue:
                img.color = blue;
                break;
            case AnswerTypes.Purple:
                img.color = purple;
                break;
        }
    }

    public void BecomeDarkenedColor()
    {
        switch (ATBehavior.answer.answerType)
        {
            case AnswerTypes.Orange:
                img.color = darkOrange;
                break;
            case AnswerTypes.Blue:
                img.color = darkBlue;
                break;
            case AnswerTypes.Purple:
                img.color = darkPurple;
                break;
        }
    }
    #endregion

    #region Shadow Management
    public void GenerateShadow()
    {
        spawnedShadow = ATBehavior.answerManager.SpawnShadow(transform.position);
        spawnedShadow.Construct(ATBehavior.answer.ranking);
    }
    public void DeSpawnShadow()
    {
        if (spawnedShadow != null)
        {
            spawnedShadow.SelfDestruct();
            spawnedShadow = null;
        }
    }
    #endregion

    #region Update
    void Update()
    {

    }
    #endregion
}
