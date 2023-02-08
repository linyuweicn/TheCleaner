using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreProgressProduction: BrainstormPanelUI
{
    private Slider slider;
    private float targetProgress =0f;
    [SerializeField] float fillSpeed;
    [SerializeField] GameObject FillColor;
    private Image image;

    // inside of your update or animation method


    private void Awake()
    {
        
    }

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        image = FillColor.GetComponent<Image>();

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
        targetProgress = newProgress;

        if (targetProgress < 50)
        {
            image.color = new Color32(219, 62, 47, 255);
        }
        else if (targetProgress < 70 && targetProgress > 51)
        {

            image.color = new Color32(226, 231, 17, 255);
        }
        else if (targetProgress > 70)
        {
            image.color = new Color32(148, 203, 72, 255);
           
        }
    }

    public float GetScoreFromAnswer(AnswerObject answer)
    {
        //totalScore = 0.1f*answer.satisfaction + 0.3f*answer.innovation + 0.5f*answer.censorFulfillment - 0.1f*answer.production;
        //totalScoreText.text = totalScore.ToString();
        float productionScore = answer.production;
        //Debug.Log("productionScore is " + productionScore);
        return productionScore;
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
            case PromptType.Narration:
                if (answerBox.GetColumn() == 1) { BrainstormGeneralManager.AveScore[5] = tempScores; j = 5; } else { BrainstormGeneralManager.AveScore[6] = tempScores; j = 6; };
                break;
        };

        float count = 1.0f;

        for (int i = 0; i < 7; i++)
        {
            if (BrainstormGeneralManager.AveScore[i] != 0.0f && i != j)
            {
                count = count + 1.0f;
                tempScores = tempScores + BrainstormGeneralManager.AveScore[i];
            }
        };

        float totalScores = (tempScores / count);
        //float reverseTotalScore = 100 - totalScores;

        Debug.Log(" production is " + totalScores);

        //Debug.Log(tempScores.ToString());

        //increment the likeness bar
        IncrementProgress(totalScores);
        //Debug.Log("productionScore is " + totalScores);
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
