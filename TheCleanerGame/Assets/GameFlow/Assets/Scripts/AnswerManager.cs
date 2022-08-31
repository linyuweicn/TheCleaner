using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AnswerManager : MonoBehaviour
{
    [SerializeField] TextAsset themeAnswersTxtFile;
    [SerializeField] TextAsset detailAnswersTxtFile;
    [SerializeField] TextAsset characterAnswersTxtFile;
    [SerializeField] public Transform answerTextParent;
    [SerializeField] public Transform shadowParent;
    [SerializeField] public Transform superParent;

    [Header("Answer Positions")]
    [SerializeField] Vector3 topOrangeTransform;
    [SerializeField] Vector3 topBlueTransform;
    [SerializeField] Vector3 topPurpleTransform;
    [SerializeField] float initialHeightDifference;
    [SerializeField] float subsequentHeightDifference;
    [SerializeField] int answerCap;


    [Header("Answer Prefabs")]
    [SerializeField] AnswerTextBox answerBox;
    [SerializeField] ShadowTextBox shadowAnswerBox;

    //private variables
    Dictionary<AnswerTypes, Vector3> topAnswerTransforms;
    public Dictionary<AnswerTypes, Dictionary<int, AnswerTextBox>> generatedAnswers;

    //Each answer belongs to a pitch type, a certain prompt, and which slot for that prompt
    void Start()
    {
        ReadAnswers(themeAnswersTxtFile);
        ReadAnswers(characterAnswersTxtFile);
        ReadAnswers(detailAnswersTxtFile);
  
        topAnswerTransforms = new Dictionary<AnswerTypes, Vector3>();
        topAnswerTransforms.Add(AnswerTypes.Orange, topOrangeTransform);
        topAnswerTransforms.Add(AnswerTypes.Blue, topBlueTransform);
        topAnswerTransforms.Add(AnswerTypes.Purple, topPurpleTransform);
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

    public void ReadAnswers(TextAsset text)
    {
        AnswerHolder ansHolder = JsonUtility.FromJson<AnswerHolder>(text.text);

        foreach (Answer a in ansHolder.answers)
        {
            GeneralFlowStateManager.instance.promptManager.promptLists[a.pitchType][a.promptNo].answerDictionary[a.answerType].Add(a);
        }
    }

    void InitializeGeneratedAnswerDictionary()
    {
        generatedAnswers = new Dictionary<AnswerTypes, Dictionary<int, AnswerTextBox>>();

        foreach (AnswerTypes a in Enum.GetValues(typeof(AnswerTypes)))
        {
            generatedAnswers.Add(a, new Dictionary<int, AnswerTextBox>());
        }
    }

    public void GenerateAnswers(Prompt p)
    {
        InitializeGeneratedAnswerDictionary();

        foreach (AnswerTypes t in Enum.GetValues(typeof(AnswerTypes)))
        {
            for (int i = 0; i < p.answerDictionary[t].Count && i < answerCap; i++)
            {
                AnswerTextBox a = Instantiate(answerBox, answerTextParent);

                if (i == 0)
                {
                    a.transform.localPosition = topAnswerTransforms[t];
                } else
                {
                    a.transform.localPosition = topAnswerTransforms[t] + Vector3.up * (initialHeightDifference + (i - 1) * subsequentHeightDifference);
                }

                a.Construct(p, p.answerDictionary[t][i], i);

                generatedAnswers[t].Add(a.answer.ranking, a);
            }
        }
        
    }

    public void DeSpawnAnswers()
    {
        var answerTextBoxes = FindObjectsOfType<AnswerTextBox>();

        foreach (AnswerTextBox a in answerTextBoxes)
        {
            a.SelfDestruct();
        }
    }

    public ShadowTextBox SpawnShadow(Vector3 position)
    {
        return Instantiate(shadowAnswerBox, position, Quaternion.identity, shadowParent);
    }

    public void CullAnswers()
    {
        foreach (AnswerTypes t in Enum.GetValues(typeof(AnswerTypes)))
        {
            int count = generatedAnswers[t].Count;
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    generatedAnswers[t][i].answer.calculated = false;
                    generatedAnswers[t][i].SelfDestruct();
                    generatedAnswers[t].Remove(i);
                } else
                {
                    generatedAnswers[t][i].SetToStone();
                    generatedAnswers[t][i].answer.calculated = true;
                }

            }
        }
    }
}
