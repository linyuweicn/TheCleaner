using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class AnswerBox : MonoBehaviour
{
    #region variables
    [Header("Debug Attributes")]
    public int ranking;
    int column;

    public Vector3 assignedPos;
    Vector3 mouseOffset;
    Vector3 startLerpPos;

    bool mouseOver;
    bool clickedOn;
    bool isMoving;

    float elapsed;
    [Header("Actual Variables")]
    [SerializeField] float transitionTime;

    RankPanelManager rankPanelManager;
    FeedbackManager feedbackManager;
    ScoreProgress scoreProgress;
    public PromptObject promptObject;

    Camera cam;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] LayerMask answerBoxLayer;

    
    AudioManager audiomanager;
    #endregion

    #region initialization

    private void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        scoreProgress = FindObjectOfType<ScoreProgress>();
        audiomanager = FindObjectOfType< AudioManager>();
    }

    public void Construct(int column, int ranking, Vector3 position)
    {
        transform.localPosition = position;
        assignedPos = transform.position;

        this.column = column;
        this.ranking = ranking;

        rankPanelManager = FindObjectOfType<RankPanelManager>();
        feedbackManager = FindObjectOfType<FeedbackManager>();
        text.text = BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking].text;
    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickedDown();
                audiomanager.PlayUiSound("ui_click");
            }
            else if (!Input.GetMouseButton(0))
            {
                ClickedUp();
            }
            else if (rankPanelManager.State == RankPanelState.Feedback)
            {
                ClickedUp();
            }
        }

        if (clickedOn)
        {
            transform.position = mouseOffset + GetMousePosition();
        }
        else
        {
            MoveTo(assignedPos);
        }
    }

    #region swapping with other answers

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (answerBoxLayer == (answerBoxLayer | (1 << collision.gameObject.layer)))
        {
            //Collision with Another Box
            if (clickedOn)
            {
                AnswerBox other = collision.gameObject.GetComponent<AnswerBox>();
                if (other != null && !other.isMoving && other.column == column)
                {
                    SwapPositionsAmongAnswers(column, other);
                }
            }
        }
    }

    void SwapPositionsAmongAnswers(int column, AnswerBox other)
    {
        if (ranking != other.ranking)
        {
            int j;
            int targetRanking = other.ranking;
            for (int i = ranking; i != targetRanking; i = j)
            {
                j = i + (i < other.ranking ? 1 : -1);
                
                BrainstormGeneralManager.Instance.FocusedContainer.Prompt.SwapRankings(column, i, j);

                AnswerBox otherAnswer = rankPanelManager.AnswerBoxes[column][j];

                int tempRanking = ranking;
                ranking = otherAnswer.ranking;
                otherAnswer.ranking = tempRanking;

                Vector3 tempAssignedPos = assignedPos;
                SetAssignedPos(otherAnswer.assignedPos, true);
                otherAnswer.SetAssignedPos(tempAssignedPos, true);

                rankPanelManager.SwapAnswers(column, i, j);
            }
            PromptManager.Instance.MarkPromptAsCompleted(BrainstormGeneralManager.Instance.FocusedContainer.Prompt);
        }
    }

    public void SetAssignedPos(Vector3 assigned, bool startMoving = false) //sets destination without interfering with MoveTo()
    {
        startLerpPos = transform.position;
        elapsed = 0.0f;

        assignedPos = assigned;
        isMoving = startMoving;
    }

    private void MoveTo(Vector3 destination) //if it needs to go to a destination, it moves
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(startLerpPos, destination, elapsed / transitionTime); //always moves in transitionTime

            if (elapsed >= transitionTime) //reached destination
            {
                isMoving = false;
                transform.position = destination;

                elapsed = 0.0f;

                if (ranking == 0) //handles Feedback and calculate scores
                {
                    feedbackManager.TriggerFeedback(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking]);
                    
                    //get the score for the top choice
                    float totalScores = feedbackManager.GetScores(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking]);
                   /* float totalNoOfColum = promptObject.AnswersStorage.Count;
                    Debug.Log("totalNoOfColum is" + totalNoOfColum);*/
                    //increment the likeness bar
                    scoreProgress.IncrementProgress(totalScores);
                }
            }

            elapsed += Time.deltaTime;
        }
    }

    #endregion

    #region mouse interface
    private void OnMouseEnter()
    {
        if (rankPanelManager.State == RankPanelState.Ranking)
        {
            mouseOver = true;
        }
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
    private void ClickedDown()
    {
        if (!clickedOn)
        {
            clickedOn = true;
            mouseOffset = transform.position - GetMousePosition();
            spriteRenderer.sortingOrder++;
        }
    }
    private void ClickedUp()
    {
        if (clickedOn)
        {
            spriteRenderer.sortingOrder--;
            SetAssignedPos(assignedPos, true);
        }
        clickedOn = false;
    }
    
    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - cam.transform.position.z;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }

   /* public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.clickCount == 2)
        {
            Debug.Log("double click");
        }

    }*/

    #endregion

    #region other functions

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    #endregion
}
