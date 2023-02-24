using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreProgressCensorship: BrainstormPanelUI
{
    
    private float targetProgress = 0f;
    [SerializeField] float fillSpeed;
    
    private Image image;

    // inside of your update or animation method


    private void Awake()
    {

    }

    void Start()
    {
        
        image = gameObject.GetComponent<Image>();

        //brainstormManager.EventManager.OnAnswerRankedTop += UpdateScoreWhenAnswerRanked;

    }

    // Update is called once per frame
    void Update()
    {

        if (image.fillAmount < targetProgress)
        {

            image.fillAmount += fillSpeed * Time.deltaTime;
        }
        else if (image.fillAmount > targetProgress + fillSpeed * Time.deltaTime)
        {
            image.fillAmount -= fillSpeed * Time.deltaTime;
        }
        else
        {
            image.fillAmount = targetProgress;
        }

    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = newProgress;

        if (targetProgress < 0.5)
        {
            image.color = new Color32(219, 62, 47, 255);//red


        }
        else if (targetProgress < 0.7 && targetProgress > 0.51)
        {

            image.color = new Color32(226, 231, 17, 255);
        }
        else if (targetProgress > 0.7)
        {
            
            image.color = new Color32(148, 203, 72, 255);
        }
    }

    public float GetScoreFromAnswer(AnswerObject answer)
    {
        //totalScore = 0.1f*answer.satisfaction + 0.3f*answer.innovation + 0.5f*answer.censorFulfillment - 0.1f*answer.production;
        //totalScoreText.text = totalScore.ToString();
        float censorshipScore = answer.censorFulfillment; 
        Debug.Log("censorshipScore is " + censorshipScore);
        return censorshipScore;
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

        float totalScores = tempScores / count;

        //Debug.Log(tempScores.ToString());

        //increment the likeness bar
        IncrementProgress(totalScores/100); // because the censorship ring uses image.fill which is from 0-1
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {

    }

    public override void Show()
    {
        image.enabled = true;
    }

    public override void Hide()
    {
        image.enabled = false;
    }
}
