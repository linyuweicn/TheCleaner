using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int column;
    public int ranking;
    public Vector3 assignedPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Construct(int column, int ranking, Vector3 assignedPos)
    {
        this.column = column;
        this.ranking = ranking;
        this.assignedPos = assignedPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.layer == collision.gameObject.layer)
        {
            AnswerBox other = collision.gameObject.GetComponent<AnswerBox>();
            if (ranking == 0 || BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking] != null)
            {
                other.TryToSwapAnswers(column, BrainstormGeneralManager.Instance.rankPanelManager.AnswerBoxes[column][ranking], transform.position);
            }
        }
    }
}
