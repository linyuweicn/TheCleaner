using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Prompt", menuName = "Prompt", order = 1)]
public class PromptObject : ScriptableObject
{
    #region variables
    [Header("Prompt Identification")]
    [Tooltip("First value is which day, second value is the nth prompt of that day")]
    [SerializeField] Vector2Int id;
    [SerializeField] PromptType promptType;
    [SerializeField] [TextArea(2, 20)] string text; //don't modify this
    [SerializeField] int imageColumn;
    Sprite image;
    string displayText;
    public bool completed;

    [Header("Answers")]
    [SerializeField] public List<NestedList> AnswersStorage; //don't modify this
    public List<List<AnswerObject>> Answers;

    //Answers.count
    #region private class
    [Serializable]
    public class NestedList
    {
        public List<AnswerObject> answerColumn;
    }
    #endregion
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    public Vector2Int ID
    {
        get { return id; }
    }
    public PromptType Type
    {
        get { return promptType; }
    }
    public string Text
    {
        get { return displayText; }
    }

    public Sprite TopImage { get { return image; } }

    #endregion

    #region initialization

    public void Initialize()
    {
        Answers = new List<List<AnswerObject>>();
        for (int i = 0; i < AnswersStorage.Count; i++)
        {
            Answers.Add(new List<AnswerObject>());
            Answers[i].Add(null); //0 is null to indicate ranking
            for (int j = 0; j < AnswersStorage[i].answerColumn.Count; j++)
            {
                Answers[i].Add(AnswersStorage[i].answerColumn[j]);
            }
        }
        completed = false;
        displayText = text;
    }

    #endregion

    #region public functions
    public void SwapRankings(int column, int here, int other)
    {
        AnswerObject temp = Answers[column][here];
        Answers[column][here] = Answers[column][other];
        Answers[column][other] = temp;
    }

    public void FillInPromptPlaceholders()
    {
        displayText = "";
        bool overwrite = false;
        int answerIndex = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (!overwrite)
            {
                if (text[i] == '[')
                {
                    //Debug.LogError("BEginning " + answerIndex + " " + text + " " + Answers[answerIndex].Count);
                    if (Answers[answerIndex][0] != null)
                    {
                        overwrite = true;
                        displayText += Answers[answerIndex][0].text;
                        //Debug.LogError("OVERWRITTEN");
                    }
                    else
                    {
                        displayText += '[';
                    }
                    answerIndex++;
                }
                else
                {
                    displayText += text[i];
                }
            }
            else if (text[i] == ']')
            {
                overwrite = false;
            }
        }
        //Debug.LogError("Reached End");

    }
    #endregion

    #region Get/Set Functions

    public void SetTopImage(AnswerBox box)
    {
        image = box.GetAnswer().image;
    }

    #endregion
}
