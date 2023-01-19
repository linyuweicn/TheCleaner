using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RankPanelManager : MonoBehaviour
{
/*    #region variables
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] ArrowButtonObject nextButton;
    [SerializeField] float decisionTimeDelay;
    [SerializeField] DecisionImage decisionImage;

    [SerializeField] AnswerBox answerBox;
    [SerializeField] ShadowBox shadowBox;
    public GameObject answerParent;
    public GameObject shadowParent;
    public GameObject superParent;

    [SerializeField] Vector3 initialAnswerPos;
    [SerializeField] float heightDiff;
    [SerializeField] float widthDiff;

    public List<List<AnswerBox>> AnswerBoxes;
    public List<ShadowBox> ShadowBoxes;
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
    public void PrepareRankedState()
    {
        decisionImage.Hide();
        if (BrainstormGeneralManager.Instance.FocusedContainer.Prompt.completed)
        {
            SkipToDecisionState();
        }
        else
        {
            UpdatePromptText();
            GenerateAnswers();
        }
    }

    public void UpdatePromptText()
    {
        promptText.text = BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Text;
    }

    public void GenerateAnswers()
    {
        AnswerBoxes = new List<List<AnswerBox>>();
        ShadowBoxes = new List<ShadowBox>();

        PromptObject prompt = BrainstormGeneralManager.Instance.FocusedContainer.Prompt;
        for (int i = 0; i < prompt.Answers.Count; i++)
        {
            AnswerBoxes.Add(new List<AnswerBox>());
            for (int j = 0; j < prompt.Answers[i].Count; j++)
            {
                Vector3 spawnPoint = initialAnswerPos + (i * widthDiff * Vector3.right) + (j * heightDiff * Vector3.down);
                if (prompt.Answers[i][j] != null)
                {
                    AnswerBoxes[i].Add(SpawnAnswer(i, j, spawnPoint));
                }
                else if (j == 0)
                {
                    AnswerBoxes[i].Add(null);
                }
                ShadowBoxes.Add(SpawnShadow(i, j, spawnPoint));
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
                if (AnswerBoxes[i][j] != null)
                {
                    AnswerBoxes[i][j].SelfDestruct();
                }
            }
            AnswerBoxes[i].Clear();
            AnswerBoxes[i].Add(top);
        }
        PromptObject prompt = BrainstormGeneralManager.Instance.FocusedContainer.Prompt;
        for (int i = 0; i < prompt.Answers.Count; i++)
        {
            AnswerObject answer = prompt.Answers[i][0];
            prompt.Answers[i].Clear();
            prompt.Answers[i].Add(answer);
        }
        for (int i = 0; i < ShadowBoxes.Count; i++)
        {
            Destroy(ShadowBoxes[i].gameObject);
        }
        ShadowBoxes.Clear();

    }

    public void SkipToDecisionState()
    {
        feedbackManager.ResetFeedback();
        GenerateAnswers();
        UpdatePromptText();
        State = RankPanelState.Culled;
        nextButton.MakeGreen();
        RewritePrompt();

        decisionImage.ShowNoAnimation(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.TopImage);
    }
    public void ToDecisionState()
    {
        feedbackManager.ResetFeedback();
        State = RankPanelState.Culled;
        CullAnswers();
        RewritePrompt();

        StartDelay();
    }

    public void StartDelay()
    {
        feedbackManager.TriggerSummaryFeedback();
        StartCoroutine(DelayForSketch());
    }

    IEnumerator DelayForSketch()
    {
        State = RankPanelState.TransToCulled;
       
        decisionImage.ShowAnimation(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.TopImage);
        float elapsed = 0.0f;
        while (elapsed < decisionTimeDelay)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        State = RankPanelState.Culled;
        
    }

    #endregion

    #region helper functions
    AnswerBox SpawnAnswer(int column, int ranking, Vector3 position)
    {
        AnswerBox output = Instantiate(answerBox, answerParent.transform);
        output.Construct(column, ranking, position);
        return output;
    }

    ShadowBox SpawnShadow(int column, int ranking, Vector3 position)
    {
        ShadowBox output = Instantiate(shadowBox, shadowParent.transform);
        output.transform.localPosition = position;
        output.Construct(column, ranking, position);
        return output;
    }

    void RewritePrompt()
    {
        BrainstormGeneralManager.Instance.FocusedContainer.Prompt.FillInPromptPlaceholders();
        UpdatePromptText();
    }
    #endregion

    #region

    #endregion*/
}
