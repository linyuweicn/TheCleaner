using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreProgress : BrainstormPanelUI
{
    private Slider slider;
    private float targetProgress =0f;
    [SerializeField] float fillSpeed;


    // inside of your update or animation method
    

    private void Awake()
    {
        
    }

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        brainstormManager.EventManager.OnAnswerRankedTop += UpdateScoreWhenAnswerRanked;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (slider.value < targetProgress)
        {
            
            slider.value += fillSpeed * Time.deltaTime;
        }
        else if (slider.value > targetProgress + fillSpeed * Time.deltaTime)
        {
            slider.value -= fillSpeed * Time.deltaTime;
        }
        else
        {
            slider.value = targetProgress;
        }

    }

    public void IncrementProgress(float newProgress)
    {
        //newProgress = answer.totalscore;
        /*if (newProgress >= targetProgress)
        {
            targetProgress = slider.value + newProgress;
        }
        else
        {
            targetProgress = newProgress;
        }*/

        targetProgress = newProgress;

        Debug.Log("new progress is "+ newProgress);
    }

    public float GetScoreFromAnswer(AnswerObject answer)
    {
        //totalScore = 0.1f*answer.satisfaction + 0.3f*answer.innovation + 0.5f*answer.censorFulfillment - 0.1f*answer.production;
        //totalScoreText.text = totalScore.ToString();
        float censorFulfillmentScore = answer.censorFulfillment;
        //Debug.Log(censorFulfillmentScore);
        return censorFulfillmentScore;
    }

    public void UpdateScoreWhenAnswerRanked(AnswerBox answerBox)
    {
        float tempScores = GetScoreFromAnswer(answerBox.GetAnswer());

        PromptType type = BrainstormGeneralManager.Instance.Prompt.Type;
        int j = 0;

        switch (type)
        {
            case PromptType.Theme:
                BrainstormGeneralManager.AveScore[0] = tempScores;
                j = 0;
                break;
            case PromptType.Character:
                if (answerBox.GetColumn() == 1) { BrainstormGeneralManager.AveScore[1] = tempScores; j = 1; } else { BrainstormGeneralManager.AveScore[2] = tempScores; j = 2; };
                break;
            case PromptType.Setting:
                if (answerBox.GetColumn() == 1) { BrainstormGeneralManager.AveScore[3] = tempScores; j = 3; } else { BrainstormGeneralManager.AveScore[4] = tempScores; j = 4; };
                break;
        };

        float count = 1.0f;

        for (int i = 0; i < 5; i++)
        {
            if (BrainstormGeneralManager.AveScore[i] != 0.0f && i != j)
            {
                count = count + 1.0f;
                tempScores = tempScores + BrainstormGeneralManager.AveScore[i];
            }
        };

        float totalScores = tempScores / count;

        //Debug.Log(tempScores.ToString());

        //increment the likeness bar
        IncrementProgress(totalScores);
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        
    }

    public override void Show()
    {
        slider.enabled = true;
    }

    public override void Hide()
    {
        slider.enabled = false;
    }
}
