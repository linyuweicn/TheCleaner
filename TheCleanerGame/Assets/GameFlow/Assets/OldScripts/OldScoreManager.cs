using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OldScoreManager : MonoBehaviour
{
    #region variables
    [SerializeField] TextMeshProUGUI satBox;
    [SerializeField] TextMeshProUGUI agreeBox;
    [SerializeField] TextMeshProUGUI innoBox;
    [SerializeField] TextMeshProUGUI prodBox;

    float satisfaction;
    float innovation;
    float production;
    float censorFulfillment;
    #endregion
    #region initialization
    void Start()
    {
        satisfaction = 0;
        innovation = 0;
        production = 0;
        censorFulfillment = 0;
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    #region score calculating
    void ResetScore()
    {
        satisfaction = 0.0f;
        innovation = 0.0f;
        production = 0.0f;
        censorFulfillment = 0.0f;
    }
    public void RawAddPoints(OldAnswer ans)
    {
        satisfaction += ans.satisfaction;
        innovation += ans.innovation;
        production += ans.production;
        censorFulfillment += ans.censorFulfillment;
    }

    public void AddPoints(OldAnswer ans)
    {
        RawAddPoints(ans);

        CalculateAgreement();
        Overwrite();
    }

    public void RawTakePoints(OldAnswer ans)
    {
        satisfaction -= ans.satisfaction;
        innovation -= ans.innovation;
        production -= ans.production;
        censorFulfillment -= ans.censorFulfillment;
    }
    public void TakePoints(OldAnswer ans)
    {
        RawTakePoints(ans);

        CalculateAgreement();
        Overwrite();
    }



    public void CalculateScore()
    {
        OldGeneralFlowStateManager.instance.currentPrompt.calculated = true;
        OldPromptManager promptManager = OldGeneralFlowStateManager.instance.promptManager;
        ResetScore();

        foreach (OldPitchTypes pt in Enum.GetValues(typeof(OldPitchTypes)))
        {
            foreach (OldPrompt p in promptManager.promptLists[pt].Values)
            {
                foreach (OldAnswerTypes at in Enum.GetValues(typeof(OldAnswerTypes)))
                {
                    foreach (OldAnswer a in p.answerDictionary[at])
                    {
                        if (a.calculated)
                        {
                            RawAddPoints(a);
                        }
                    }
                }
            }
        }

        CalculateAgreement();
        Overwrite();
    }
    #endregion

    #region edit text
    void CalculateAgreement()
    {
        float agreement = (0.5f * censorFulfillment) + (0.3f * innovation) + (0.1f * satisfaction) - (0.1f * production);
        agreeBox.text = agreement.ToString("F2");
    }
    void Overwrite()
    {
        satBox.text = satisfaction.ToString("F2");
        innoBox.text = innovation.ToString("F2");
        prodBox.text = production.ToString("F2");
    }
    #endregion

}
