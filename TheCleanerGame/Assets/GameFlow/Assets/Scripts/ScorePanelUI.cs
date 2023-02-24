using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScorePanelUI : MonoBehaviour
{
    [Tooltip("True == global, False == daily")][SerializeField] bool global;
    [SerializeField] List<ScoreFiller> m_list;
    Dictionary<ScoreType, ScoreFiller> scoreDictionary;
    [Serializable]
    private class ScoreFiller
    {
        public ScoreType type;
        public float range;
        public Image image;
        public Slider slider;
    }

    private void Awake()
    {
        scoreDictionary = new Dictionary<ScoreType, ScoreFiller>();
        foreach (ScoreFiller s in m_list)
        {
            scoreDictionary.Add(s.type, s);
        }

        if (global)
        {
            UpdateGlobalScore();
        }
        else
        {
            UpdateDailyScore();
        }
    }

    public void UpdateGlobalScore()
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

        if (scoreDictionary.ContainsKey(ScoreType.Censorship)) { SetUpScoreFiller(scoreDictionary[ScoreType.Censorship], totalCensorship / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Creativity)) { SetUpScoreFiller(scoreDictionary[ScoreType.Creativity], totalCreativity / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Satisfaction)) { SetUpScoreFiller(scoreDictionary[ScoreType.Satisfaction], totalSatisfaction / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Production)) { SetUpScoreFiller(scoreDictionary[ScoreType.Production], totalProduction / count); }

    }

    public void UpdateDailyScore()
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

        if (scoreDictionary.ContainsKey(ScoreType.Censorship)) { SetUpScoreFiller(scoreDictionary[ScoreType.Censorship], totalCensorship / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Creativity)) { SetUpScoreFiller(scoreDictionary[ScoreType.Creativity], totalCreativity / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Satisfaction)) { SetUpScoreFiller(scoreDictionary[ScoreType.Satisfaction], totalSatisfaction / count); }
        if (scoreDictionary.ContainsKey(ScoreType.Production)) { SetUpScoreFiller(scoreDictionary[ScoreType.Production], totalProduction / count); }
    }

    private void SetUpScoreFiller(ScoreFiller s, float range)
    {
        s.range = range;
        if (s.image != null) { s.image.fillAmount = range; }
        if (s.slider != null) { s.slider.value = range * 100; }
    }


}
