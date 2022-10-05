using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class OldAnswerManager : MonoBehaviour //reads answers and stores them within prompts, also processes and stores Answer Text Boxes
{
    #region variables
    [SerializeField] TextAsset themeAnswersTxtFile;
    [SerializeField] TextAsset detailAnswersTxtFile;
    [SerializeField] TextAsset characterAnswersTxtFile;
    [SerializeField] public Transform answerTextParent; //where we spawn the answer
    [SerializeField] public Transform shadowParent; //where we place the shadows of answers
    [SerializeField] public Transform superParent; //where we place the answer when we click on it, so it goes over the other answers

    [Header("Answer Positions")]
    [SerializeField] Vector3 topOrangeTransform; //the place where we store the best orange answer
    [SerializeField] Vector3 topBlueTransform; //the place where we store the best blue answer
    [SerializeField] Vector3 topPurpleTransform; //the place where we store the best purple answer
    [SerializeField] float initialHeightDifference; //after we spawn the best answer, we need to spawn the answers below it by this amount
    [SerializeField] float subsequentHeightDifference; //the height difference between each subsequent non-best answer
    [SerializeField] int answerCap; //the maximum answers to spawn per color


    [Header("Answer Prefabs")]
    [SerializeField] OldAnswerTextBox answerBox; //what we're spawning
    [SerializeField] OldShadowTextBox shadowAnswerBox; //the shadow we're spawning

    //private variables
    Dictionary<OldAnswerTypes, Vector3> topAnswerTransforms; //stores locations of the best answers by color
    public Dictionary<OldAnswerTypes, Dictionary<int, OldAnswerTextBox>> generatedAnswers; //stores all the answer text boxes
    public OldCensorshipUI censorshipUI;
    [HideInInspector] public bool areAnswersClickable = true;
    //Each answer belongs to a pitch type, a certain prompt
    #endregion

    #region private classes
    [Serializable] private class AnswerHolder
    {
        public List<OldAnswer> answers;

        public AnswerHolder()
        {
            answers = new List<OldAnswer>();
        }
    }
    #endregion 

    #region initialization

    void Awake()
    {
        censorshipUI = FindObjectOfType<OldCensorshipUI>();
    }
    void Start()
    {
        //read answers from json file
        ReadAnswers(themeAnswersTxtFile);
        ReadAnswers(characterAnswersTxtFile);
        ReadAnswers(detailAnswersTxtFile);
  
        //initialize dictionary
        topAnswerTransforms = new Dictionary<OldAnswerTypes, Vector3>();
        topAnswerTransforms.Add(OldAnswerTypes.Orange, topOrangeTransform);
        topAnswerTransforms.Add(OldAnswerTypes.Blue, topBlueTransform);
        topAnswerTransforms.Add(OldAnswerTypes.Purple, topPurpleTransform);

        Physics.queriesHitTriggers = true; //so you can click on trigger hitboxes.
    }
    public void ReadAnswers(TextAsset text)
    {
        AnswerHolder ansHolder = JsonUtility.FromJson<AnswerHolder>(text.text);

        foreach (OldAnswer a in ansHolder.answers)
        {
            OldGeneralFlowStateManager.instance.promptManager.promptLists[a.pitchType][a.promptNo].answerDictionary[a.answerType].Add(a);
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    #region answerTextBox
    void InitializeGeneratedAnswerDictionary()
    {
        generatedAnswers = new Dictionary<OldAnswerTypes, Dictionary<int, OldAnswerTextBox>>();

        foreach (OldAnswerTypes a in Enum.GetValues(typeof(OldAnswerTypes)))
        {
            generatedAnswers.Add(a, new Dictionary<int, OldAnswerTextBox>());
        }
    }

    public void GenerateAnswers(OldPrompt p)
    {
        InitializeGeneratedAnswerDictionary();

        foreach (OldAnswerTypes t in Enum.GetValues(typeof(OldAnswerTypes)))
        {
            for (int i = 0; i < p.answerDictionary[t].Count && i < answerCap; i++)
            {
                OldAnswerTextBox a = Instantiate(answerBox, answerTextParent);

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
        var answerTextBoxes = FindObjectsOfType<OldAnswerTextBox>();

        foreach (OldAnswerTextBox a in answerTextBoxes)
        {
            a.SelfDestruct();
        }
    }

    public OldShadowTextBox SpawnShadow(Vector3 position)
    {
        return Instantiate(shadowAnswerBox, position, Quaternion.identity, shadowParent);
    }

    public void CullAnswers()
    {
        foreach (OldAnswerTypes t in Enum.GetValues(typeof(OldAnswerTypes)))
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

    public void CheckForCensorship(OldAnswer answer)
    {
        if (answer.disliked)
        {
            censorshipUI.SetCensorshipUI(answer);
            censorshipUI.Show();
        }
    }
    #endregion

    public void SetAreAnswersClickable(bool areAnswersClickable)
    {
        this.areAnswersClickable = areAnswersClickable;
    }
}
