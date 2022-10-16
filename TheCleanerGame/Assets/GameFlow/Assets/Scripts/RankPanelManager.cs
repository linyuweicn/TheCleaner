using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RankPanelManager : MonoBehaviour
{
    #region variables
    [SerializeField] TextMeshProUGUI promptText;

    [SerializeField] AnswerBox answerBox;
    [SerializeField] GameObject shadowBox;
    [SerializeField] GameObject answerParent;

    [SerializeField] Vector3 initialAnswerPos;
    [SerializeField] float heightDiff;
    [SerializeField] float widthDiff;

    public List<List<AnswerBox>> AnswerBoxes;
    public List<GameObject> ShadowBoxes;
    public RankPanelState State { get; set; }
    public FeedbackManager feedbackManager;
    #endregion

    #region initialization
    private void Awake()
    {
        State = RankPanelState.Ranking;
    }
    void Start()
    {
        
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    #region public functions
    public void UpdatePromptText()
    {
        promptText.text = BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Text;
    }

    public void GenerateAnswers()
    {
        AnswerBoxes = new List<List<AnswerBox>>();
        ShadowBoxes = new List<GameObject>();

        PromptObject prompt = BrainstormGeneralManager.Instance.FocusedContainer.Prompt;
        for (int i = 0; i < prompt.Answers.Count; i++)
        {
            AnswerBoxes.Add(new List<AnswerBox>());
            for (int j = 0; j < prompt.Answers[i].Count; j++)
            {
                Vector3 spawnPoint = initialAnswerPos + (i * widthDiff * Vector3.right) + (j * heightDiff * Vector3.down);
                AnswerBoxes[i].Add(SpawnAnswer(i, j, spawnPoint));
                ShadowBoxes.Add(SpawnShadow(spawnPoint));
            }
        }
    }

    public void SwapAnswers(int column, int here, int other)
    {
        AnswerBox temp = AnswerBoxes[column][here];
        AnswerBoxes[column][here] = AnswerBoxes[column][other];
        AnswerBoxes[column][other] = temp;
    }

    public void DestroyAllAnswers()
    {
        for (int i = 0; i < AnswerBoxes.Count; i++)
        {
            for (int j = 0; j < AnswerBoxes[i].Count; j++)
            {
                AnswerBoxes[i][j].SelfDestruct();
            }
        }
        for (int i = 0; i < ShadowBoxes.Count; i++)
        {
            Destroy(ShadowBoxes[i]);
        }
        ShadowBoxes.Clear();
    }

    public void CullAnswers()
    {
        for (int i = 0; i < AnswerBoxes.Count; i++)
        {
            AnswerBox top = AnswerBoxes[i][0];
            for (int j = 1; j < AnswerBoxes[i].Count; j++)
            {
                AnswerBoxes[i][j].SelfDestruct();
            }
            AnswerBoxes[i].Clear();
            AnswerBoxes[i].Add(top);
        }
        for (int i = 0; i < ShadowBoxes.Count; i++)
        {
            Destroy(ShadowBoxes[i]);
        }
        ShadowBoxes.Clear();
    }

    public void NextStage()
    {
        feedbackManager.ResetFeedback();
        State = RankPanelState.Culled;
        CullAnswers();
    }

    #endregion

    #region helper functions
    AnswerBox SpawnAnswer(int column, int ranking, Vector3 position)
    {
        AnswerBox output = Instantiate(answerBox, answerParent.transform);
        output.Construct(column, ranking, position);
        return output;
    }

    GameObject SpawnShadow(Vector3 position)
    {
        GameObject output = Instantiate(shadowBox, answerParent.transform);
        output.transform.localPosition = position;
        return output;
    }
    #endregion

    #region

    #endregion
}
