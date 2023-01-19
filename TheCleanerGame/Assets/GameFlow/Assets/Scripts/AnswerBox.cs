using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class AnswerBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region variables
    [Header("Debug Attributes")]
    private int ranking;
    private int column;

    private Vector3 assignedPos;
    private Vector3 mouseOffset;
    private Vector3 startLerpPos;

    // List of scores, AveScore[0] is for Character, AveScore[1], AveScore[2] are for Narration, 
    // AveScore[3] AveScore[4] are for Theme

    //List<float> AveScore = new List<float>(){0.0f, 0.0f, 0.0f, 0.0f, 0.0f};


    private bool mouseOver;
    private bool clickedOn;
    private bool isMoving;

    private float elapsed;
    [Header("Actual Variables")]
    [SerializeField] private float transitionTime;

    private AnswerUIContainer answerUIContainer;
    private ScoreProgress scoreProgress;
    private PromptObject promptObject;

    Camera cam;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] LayerMask answerBoxLayer;
    [SerializeField] Animator animator;

    
    AudioManager audiomanager;

   /* //change cursor sprites
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;*/
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

        animator.SetTrigger("Enter");
    }

    public void Construct(int column, int ranking, Vector3 position, AnswerUIContainer container)
    {
        transform.localPosition = position;
        assignedPos = transform.position;

        this.column = column;
        this.ranking = ranking;
        answerUIContainer = container;

        text.text = BrainstormGeneralManager.Instance.Prompt.Answers[column][ranking].text;
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
                audiomanager.PlayUiSound("ui_drag_03");
            }
            else if (!Input.GetMouseButton(0))
            {
                ClickedUp();
            }
            else if (BrainstormGeneralManager.Instance.MyFeedbackState != FeedbackType.Null)
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
/*            if (clickedOn)
            {
                AnswerBox other = collision.gameObject.GetComponent<AnswerBox>();
                if (other != null && !other.isMoving && other.column == column)
                {
                    SwapPositionsAmongAnswers(column, other);
                }
            }*/
        }
    }

    public void TryToSwapAnswers(int column, AnswerBox other, Vector3 otherPosition)
    {
        if (clickedOn)
        {
            if (other == null)
            {
                MoveAnswerToEmptyTopChoice(column, other, otherPosition);
            }
            else if (ranking != other.ranking)
            {
                SwapPositionsAmongAnswers(column, other);
            }
        }
    }

    void MoveAnswerToEmptyTopChoice(int column, AnswerBox other, Vector3 otherPosition)
    {
        BrainstormGeneralManager.Instance.Prompt.SwapRankings(column, ranking, 0);
        answerUIContainer.SwapAnswers(column, ranking, 0);

        int oldRanking = ranking;
        ranking = 0;

        Vector3 oldAssignedPosition = assignedPos;
        SetAssignedPos(otherPosition);

        for (int i = oldRanking + 1; i < answerUIContainer.AnswerBoxes[column].Count; i++)
        {
            BrainstormGeneralManager.Instance.Prompt.SwapRankings(column, i, i - 1);

            AnswerBox otherAnswer = answerUIContainer.AnswerBoxes[column][i];

            otherAnswer.ranking--;

            Vector3 tempAssignedPos = oldAssignedPosition;
            oldAssignedPosition = otherAnswer.assignedPos;
            otherAnswer.SetAssignedPos(tempAssignedPos, true);

            answerUIContainer.SwapAnswers(column, i, i - 1);
        }

        bool allAtTop = true;
        for (int i = 0; i < answerUIContainer.AnswerBoxes.Count; i++)
        {
            if (answerUIContainer.AnswerBoxes[i][0] == null)
            {
                allAtTop = false;
                break;
            }
        }

        if (allAtTop)
        {
            PromptManager.Instance.MarkPromptAsCompleted(BrainstormGeneralManager.Instance.Prompt);
        }
    }

    void SwapPositionsAmongAnswers(int column, AnswerBox other)
    {
        int j;
        int targetRanking = other.ranking;
        for (int i = ranking; i != targetRanking; i = j)
        {
            j = i + (i < other.ranking ? 1 : -1);

            BrainstormGeneralManager.Instance.Prompt.SwapRankings(column, i, j);

            AnswerBox otherAnswer = answerUIContainer.AnswerBoxes[column][j];

            int tempRanking = ranking;
            ranking = otherAnswer.ranking;
            otherAnswer.ranking = tempRanking;

            Vector3 tempAssignedPos = assignedPos;
            SetAssignedPos(otherAnswer.assignedPos, true);
            otherAnswer.SetAssignedPos(tempAssignedPos, true);

            answerUIContainer.SwapAnswers(column, i, j);
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
                    BecomeTopRank();
                }
            }

            elapsed += Time.deltaTime;
        }
    }

    private void BecomeTopRank()
    {
        BrainstormGeneralManager.Instance.EventManager.OnAnswerRankedTopEvent(this);
    }

    #endregion

    #region mouse interface
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (BrainstormGeneralManager.Instance.MyBrainstormState == BrainstormState.Rank)
        {
            mouseOver = true;
        }
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        mouseOver = false;
        //Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    private void ClickedDown()
    {
        if (!clickedOn)
        {
            clickedOn = true;
            mouseOffset = transform.position - GetMousePosition();
            transform.parent = answerUIContainer.superParent.transform;
        }
    }
    private void ClickedUp()
    {
        if (clickedOn)
        {
            transform.parent = answerUIContainer.answerParent.transform;
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

 

    #endregion

    #region other functions

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }


    #endregion

    #region get/set

    public AnswerObject GetAnswer()
    {
        return BrainstormGeneralManager.Instance.Prompt.Answers[column][ranking];
    }
    public string GetAnswerText()
    {
        return GetAnswer().text;
    }

    public int GetColumn() { return column; }
    #endregion
}
