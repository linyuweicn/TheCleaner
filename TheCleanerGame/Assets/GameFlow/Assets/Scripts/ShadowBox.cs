using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBox : MonoBehaviour
{
    // Start is called before the first frame update
    private int column;
    private int ranking;
    private Vector3 assignedPos;
    private AnswerUIContainer answerUIContainer;
    [SerializeField] private Animator animator;

    void Start()
    {
        animator.SetTrigger("Enter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Construct(int column, int ranking, Vector3 assignedPos, AnswerUIContainer container)
    {
        this.column = column;
        this.ranking = ranking;
        this.assignedPos = assignedPos;

        answerUIContainer = container;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.layer == collision.gameObject.layer)
        {
            AnswerBox other = collision.gameObject.GetComponent<AnswerBox>();
            if (column == other.GetColumn() && (ranking == 0 || BrainstormGeneralManager.Instance.Prompt.Answers[column][ranking] != null))
            {
                other.TryToSwapAnswers(column, answerUIContainer.AnswerBoxes[column][ranking], transform.position);
            }
        }
    }
}
