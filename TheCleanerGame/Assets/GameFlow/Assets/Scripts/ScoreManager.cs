using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI satBox;
    [SerializeField] TextMeshProUGUI agreeBox;
    [SerializeField] TextMeshProUGUI innoBox;
    [SerializeField] TextMeshProUGUI prodBox;

    float satisfaction;
    float innovation;
    float production;
    float censorFulfillment;
    void Start()
    {
        satisfaction = 0;
        innovation = 0;
        production = 0;
        censorFulfillment = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetScore()
    {
        satisfaction = 0.0f;
        innovation = 0.0f;
        production = 0.0f;
        censorFulfillment = 0.0f;
    }
    public void RawAddPoints(Answer ans)
    {
        satisfaction += ans.satisfaction;
        innovation += ans.innovation;
        production += ans.production;
        censorFulfillment += ans.censorFulfillment;
    }

    public void AddPoints(Answer ans)
    {
        RawAddPoints(ans);

        CalculateAgreement();
        Overwrite();
    }

    public void RawTakePoints(Answer ans)
    {
        satisfaction -= ans.satisfaction;
        innovation -= ans.innovation;
        production -= ans.production;
        censorFulfillment -= ans.censorFulfillment;
    }
    public void TakePoints(Answer ans)
    {
        RawTakePoints(ans);

        CalculateAgreement();
        Overwrite();
    }

    void Overwrite()
    {
        satBox.text = satisfaction.ToString("F2");
        innoBox.text = innovation.ToString("F2");
        prodBox.text = production.ToString("F2");
    }

    void CalculateAgreement()
    {
        float agreement = (0.5f * censorFulfillment) + (0.3f * innovation) + (0.1f * satisfaction) - (0.1f * production);
        agreeBox.text = agreement.ToString("F2");
    }

    public void CalculateScore()
    {
        GeneralFlowStateManager.instance.currentPrompt.calculated = true;
        PromptManager promptManager = GeneralFlowStateManager.instance.promptManager;
        ResetScore();

        foreach (PitchTypes pt in Enum.GetValues(typeof(PitchTypes)))
        {
            foreach (Prompt p in promptManager.promptLists[pt].Values)
            {
                foreach (AnswerTypes at in Enum.GetValues(typeof(AnswerTypes)))
                {
                    foreach (Answer a in p.answerDictionary[at])
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
}
