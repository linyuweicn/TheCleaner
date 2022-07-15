using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnswerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextAsset themeAnswers;
    [SerializeField] TextAsset detailAnswers;
    [SerializeField] TextAsset characterAnswers;
    Dictionary<PitchTypes, List<List<HashSet<Answer>>>> answers;
    PromptManager pMang;
    //Each answer belongs to a pitch type, a certain prompt, and which slot for that prompt
    void Start()
    {
        pMang = FindObjectOfType<PromptManager>();
        answers = new Dictionary<PitchTypes, List<List<HashSet<Answer>>>>();

        foreach (PitchTypes type in Enum.GetValues(typeof(PitchTypes)))
        {
            answers[type] = new List<List<HashSet<Answer>>>();

            for (int pNo = 0; pNo < pMang.prompts[type].Count; pNo++)
            {
                answers[type].Add(new List<HashSet<Answer>>());

                for (int sNo = 0; sNo < pMang.prompts[type][pNo].Slots; sNo++)
                {
                    answers[type][pNo].Add(new HashSet<Answer>());
                }
            }
        }

        ReadAnswers(themeAnswers.text, PitchTypes.Theme);
        ReadAnswers(characterAnswers.text, PitchTypes.Character);
        ReadAnswers(detailAnswers.text, PitchTypes.Detail);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable] private class AnswerHolder
    {
        public List<Answer> answers;

        public AnswerHolder()
        {
            answers = new List<Answer>();
        }
    }

    public void ReadAnswers(string text, PitchTypes type)
    {

        AnswerHolder ansHolder = JsonUtility.FromJson<AnswerHolder>(text);

        foreach (Answer a in ansHolder.answers)
        {
            answers[type][a.promptNo][a.slotNo].Add(a);
        }
    }

    public HashSet<Answer> getAnswerSet(PitchTypes type, int promptNo, int slotNo)
    {
        if (answers.ContainsKey(type))
        {
            if (0 <= promptNo && answers[type].Count > promptNo)
            {
                if (0 <= slotNo && answers[type][promptNo].Count > slotNo)
                {
                    return answers[type][promptNo][slotNo];
                }
            }
        }
        return null;
    }

    public void GenerateAnswers(PitchTypes type, int promptNo, int slotNo)
    {
        List<Answer> answerSet = new List<Answer>(getAnswerSet(type, promptNo, slotNo));
        for (int i = 0; i < GameFlowData.instance.texts.Count && i < answerSet.Count; i++)
        {
            GameFlowData.instance.texts[i].AssignAnswer(answerSet[i]);
        }
    }
}
