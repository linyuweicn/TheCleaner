using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerUIContainer : BrainstormPanelUI
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnswerBox answerBox;
    [SerializeField] private ShadowBox shadowBox;

    [SerializeField] public GameObject answerParent;
    [SerializeField] public GameObject shadowParent;
    [SerializeField] public GameObject superParent;

    [SerializeField] Vector3 initialAnswerPos;
    [SerializeField] float heightAnswerDiff;
    [SerializeField] float widthAnswerDiff;

    public List<List<AnswerBox>> AnswerBoxes;
    public List<ShadowBox> ShadowBoxes;

    public void GenerateAnswers()
    {
        AnswerBoxes = new List<List<AnswerBox>>();
        ShadowBoxes = new List<ShadowBox>();

        PromptObject prompt = BrainstormGeneralManager.Instance.Prompt;
        for (int i = 0; i < prompt.Answers.Count; i++)
        {
            AnswerBoxes.Add(new List<AnswerBox>());
            for (int j = 0; j < prompt.Answers[i].Count; j++)
            {
                Vector3 spawnPoint = initialAnswerPos + (i * widthAnswerDiff * Vector3.right) + (j * heightAnswerDiff * Vector3.down);
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

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (newState == BrainstormState.Rank)
        {
            StartCoroutine("Enter");
        }
        else if (oldState == BrainstormState.Rank && newState == BrainstormState.Decision)
        {
            CullAnswers();
        }
        else if (newState == BrainstormState.Decision)
        {
            GenerateAnswers();
            CullAnswers();
        }
        else
        {
            Hide();
        }
    }

    IEnumerator Enter()
    {
        animator.SetTrigger("Enter");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
        Show();
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Enter"));
    }

    public override void Show()
    {
        GenerateAnswers();
    }

    public override void Hide()
    {
        DestroyAllAnswers();
    }

    AnswerBox SpawnAnswer(int column, int ranking, Vector3 position)
    {
        AnswerBox output = Instantiate(answerBox, answerParent.transform);
        output.Construct(column, ranking, position, this);
        return output;
    }

    ShadowBox SpawnShadow(int column, int ranking, Vector3 position)
    {
        ShadowBox output = Instantiate(shadowBox, shadowParent.transform);
        output.transform.localPosition = position;
        output.Construct(column, ranking, position, this);
        return output;
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
        AnswerBoxes.Clear();
        for (int i = 0; i < ShadowBoxes.Count; i++)
        {
            Destroy(ShadowBoxes[i]);
        }
        ShadowBoxes.Clear();
    }

    void CullAnswers()
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
        PromptObject prompt = BrainstormGeneralManager.Instance.Prompt;
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
}
