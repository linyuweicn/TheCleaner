using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScorePanelUI : BrainstormPanelUI
{
    [Tooltip("True == global, False == daily")][SerializeField] bool global;
    [SerializeField] List<ScoreFiller> m_list;
    Dictionary<ScoreType, ScoreFiller> scoreDictionary;
    [SerializeField] GameObject m_object;
    [SerializeField] float sliderFillSpeed;
    [SerializeField] float imageFillSpeed;
    [Serializable]
    private class ScoreFiller
    {
        public ScoreType type;
        public float targetValue;
        public Image image;
        public Slider slider;
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        
    }

    public override void Hide()
    {
        m_object.SetActive(false);
    }

    public override void Show()
    {
        m_object.SetActive(true);
    }

    private void Awake()
    {
        scoreDictionary = new Dictionary<ScoreType, ScoreFiller>();
        foreach (ScoreFiller s in m_list)
        {
            scoreDictionary.Add(s.type, s);
        }
    }

    private void Start()
    {
        if (global)
        {
            UpdateGlobalScore(null);
            brainstormManager.EventManager.OnAnswerRankedTop += UpdateGlobalScore;
        }
        else
        {
            brainstormManager.EventManager.OnAnswerRankedTop += UpdateDailyScore;

        }
    }

    private void Update()
    {
        foreach (ScoreFiller s in scoreDictionary.Values)
        {
            OnUpdateScoreFiller(s);
        }
    }

    public void UpdateGlobalScore(AnswerBox answer)
    {
        float totalCensorship = 0, totalProduction = 0, totalSatisfaction = 0, totalCreativity = 0;
        int count = 0;
        foreach (Dictionary<int, PromptObject> p in PromptManager.Instance.PromptDictionary.Values)
        {
            foreach (PromptObject o in p.Values)
            {
                foreach (List<AnswerObject> a in o.Answers)
                {
                    if (a[0] != null)
                    {
                        totalCensorship += a[0].censorFulfillment;
                        totalCreativity += a[0].innovation;
                        totalProduction += a[0].production;
                        totalSatisfaction += a[0].satisfaction;
                        count++;
                    }
                }
            }
        }

        if (count == 0) { return; }

        if (scoreDictionary.ContainsKey(ScoreType.Censorship)) { SetUpScoreFiller(scoreDictionary[ScoreType.Censorship], totalCensorship / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Creativity)) { SetUpScoreFiller(scoreDictionary[ScoreType.Creativity], totalCreativity / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Satisfaction)) { SetUpScoreFiller(scoreDictionary[ScoreType.Satisfaction], totalSatisfaction / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Production)) { SetUpScoreFiller(scoreDictionary[ScoreType.Production], totalProduction / count); }


        Debug.LogError("Values " + totalCensorship + " " + totalCreativity + " " + totalSatisfaction + " " + totalProduction + " " + count);

    }

    public void UpdateDailyScore(AnswerBox answer)
    {
        float totalCensorship = 0, totalProduction = 0, totalSatisfaction = 0, totalCreativity = 0;
        int count = 0;

        foreach (List<AnswerObject> a in BrainstormGeneralManager.Instance.Prompt.Answers)
        {
            if (a[0] != null)
            {
                totalCensorship += a[0].censorFulfillment;
                totalCreativity += a[0].innovation;
                totalProduction += a[0].production;
                totalSatisfaction += a[0].satisfaction;
                count++;
            }
        }

        if (count == 0) { return; }

        if (scoreDictionary.ContainsKey(ScoreType.Censorship)) { SetUpScoreFiller(scoreDictionary[ScoreType.Censorship], totalCensorship / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Creativity)) { SetUpScoreFiller(scoreDictionary[ScoreType.Creativity], totalCreativity / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Satisfaction)) { SetUpScoreFiller(scoreDictionary[ScoreType.Satisfaction], totalSatisfaction / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Production)) { SetUpScoreFiller(scoreDictionary[ScoreType.Production], totalProduction / count); }

    }

    private void SetUpScoreFiller(ScoreFiller s, float range)
    {
        s.targetValue = range;
    }

    private void OnUpdateScoreFiller(ScoreFiller s)
    {
        if (s.image != null)
        {
            if (s.image.fillAmount + 0.05f > s.targetValue / 100.0f && s.image.fillAmount - 0.05f < s.targetValue / 100.0f)
            {
                s.image.fillAmount = s.targetValue / 100.0f;
            }
            else if (s.image.fillAmount < s.targetValue / 100.0f)
            {
                s.image.fillAmount += imageFillSpeed * Time.deltaTime;
            }
            else if (s.image.fillAmount > (s.targetValue / 100.0f))
            {

                s.image.fillAmount -= imageFillSpeed * Time.deltaTime;
            }
        }
        if (s.slider != null)
        {
            if (s.slider.value + 5 > s.targetValue && s.slider.value - 5 < s.targetValue)
            {
                s.slider.value = s.targetValue;
            }
            else if (s.slider.value < s.targetValue)
            {
                s.slider.value += sliderFillSpeed * Time.deltaTime;
                
            }
            else if (s.slider.value > s.targetValue)
            {
                s.slider.value -= sliderFillSpeed * Time.deltaTime;
            }
        }
    }
}
