using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class AnswerBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region variables
    [Header("Debug Attributes")]
    public int ranking;
    int column;

    public Vector3 assignedPos;
    Vector3 mouseOffset;
    Vector3 startLerpPos;

    // List of scores, AveScore[0] is for Character, AveScore[1], AveScore[2] are for Narration, 
    // AveScore[3] AveScore[4] are for Theme

    //List<float> AveScore = new List<float>(){0.0f, 0.0f, 0.0f, 0.0f, 0.0f};


    bool mouseOver;
    public bool clickedOn;
    bool isMoving;

    float elapsed;
    [Header("Actual Variables")]
    [SerializeField] float transitionTime;

    RankPanelManager rankPanelManager;
    FeedbackManager feedbackManager;
    ScoreProgress scoreProgress;
    public PromptObject promptObject;

    Camera cam;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] LayerMask answerBoxLayer;

    
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
                audiomanager.PlayUiSound("ui_drag_03");
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
        BrainstormGeneralManager.Instance.FocusedContainer.Prompt.SwapRankings(column, ranking, 0);
        rankPanelManager.SwapAnswers(column, ranking, 0);

        int oldRanking = ranking;
        ranking = 0;

        Vector3 oldAssignedPosition = assignedPos;
        SetAssignedPos(otherPosition);

        for (int i = oldRanking + 1; i < rankPanelManager.AnswerBoxes[column].Count; i++)
        {
            BrainstormGeneralManager.Instance.FocusedContainer.Prompt.SwapRankings(column, i, i - 1);

            AnswerBox otherAnswer = rankPanelManager.AnswerBoxes[column][i];

            otherAnswer.ranking--;

            Vector3 tempAssignedPos = oldAssignedPosition;
            oldAssignedPosition = otherAnswer.assignedPos;
            otherAnswer.SetAssignedPos(tempAssignedPos, true);

            rankPanelManager.SwapAnswers(column, i, i - 1);
        }

        bool allAtTop = true;
        for (int i = 0; i < rankPanelManager.AnswerBoxes.Count; i++)
        {
            if (rankPanelManager.AnswerBoxes[i][0] == null)
            {
                allAtTop = false;
                break;
            }
        }

        if (allAtTop)
        {
            PromptManager.Instance.MarkPromptAsCompleted(BrainstormGeneralManager.Instance.FocusedContainer.Prompt);
        }
    }

    void SwapPositionsAmongAnswers(int column, AnswerBox other)
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
        feedbackManager.TriggerFeedback(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking]);

        //get the score for the top choice
        float tempScores = feedbackManager.GetScores(BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking]);

        PromptType type = BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Type;

        int j = 0;

        switch (type)
        {
            case PromptType.Theme:
                BrainstormGeneralManager.AveScore[0] = tempScores;
                j = 0;
                break;
            case PromptType.Character:
                if (column == 1) { BrainstormGeneralManager.AveScore[1] = tempScores; j = 1; } else { BrainstormGeneralManager.AveScore[2] = tempScores; j = 2; };
                break;
            case PromptType.Setting:
                if (column == 1) { BrainstormGeneralManager.AveScore[3] = tempScores; j = 3; } else { BrainstormGeneralManager.AveScore[4] = tempScores; j = 4; };
                break;
        };

        float count = 1.0f;

        for (int i = 0; i < 5; i++)
        {
            if (BrainstormGeneralManager.AveScore[i] != 0.0f && i != j)
            {
                count = count + 1.0f;
                tempScores = tempScores + BrainstormGeneralManager.AveScore[i];
            }
        };

        float totalScores = tempScores / count;

        //Debug.Log(tempScores.ToString());

        //increment the likeness bar
        scoreProgress.IncrementProgress(totalScores);
    }

    #endregion

    #region mouse interface
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (rankPanelManager.State == RankPanelState.Ranking)
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
            transform.parent = rankPanelManager.superParent.transform;
        }
    }
    private void ClickedUp()
    {
        if (clickedOn)
        {
            transform.parent = rankPanelManager.answerParent.transform;
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
        return BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Answers[column][ranking];
    }
    public string GetAnswerText()
    {
        return GetAnswer().text;
    }

    #endregion
}
