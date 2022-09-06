using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerTextBox : MonoBehaviour //handles behavior of AnswerTextBox
{
    // Start is called before the first frame update
    #region variables

    //personal data
    Prompt prompt;
    public Answer answer;

    //references
    [SerializeField] public Collider2D collider;
    [SerializeField] AnswerTextBoxVisuals ATVisuals;
    [SerializeField] LayerMask answerTextLayer;
    [HideInInspector] public AnswerManager answerManager;
    Camera cam;
    AnswerTextBox lastSwapped;

    //time floats
    float elapsed = 0.0f;
    [SerializeField] float transitionTime;

    //answerTextBox states
    bool mouseOver;
    bool mouseClickedOn;
    bool isMoving;
    bool canBeMoved;

    //positions
    Vector3 origLocalPosition; //the position where it is rooted to
    Vector3 mousePositionOffset; //the offset from the mouse when you click on it
    Vector3 destination; //its potential destination
    Vector3 startingPlace; //source of its lerp


    #endregion

    #region initialization
    private void Awake()
    {
        cam = Camera.main;
        answerManager = GeneralFlowStateManager.instance.answerManager;
        
        lastSwapped = this;
        canBeMoved = true;
    }
    void Start()
    {
        origLocalPosition = transform.localPosition;
    }

    public void Construct(Prompt prompt, Answer answer, int ranking)
    {
        this.prompt = prompt;
        this.answer = answer;

        if (answer.ranking < 0)
        {
            ChangeRanking(ranking);
        }

        ATVisuals.Construct();

    }
    #endregion

    #region Update
    void Update()
    {
        if (mouseOver && Input.GetMouseButtonDown(0) && canBeMoved) //handles whether it can be clicked
        {
            ClickedOn();
        }

        if (mouseClickedOn)
        {
            transform.position = mousePositionOffset + GetMousePosition();
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1); //ones out z
            
            if (!Input.GetMouseButton(0))
            {
                ClickedRelease();
            }

        } else
        {
            MoveTo(); //only moves while it is not being clicked on
        }
    }
    #endregion

    #region self-destruct

    public void SelfDestruct()
    {
        ATVisuals.DeSpawnShadow();
        Destroy(gameObject);
    }

    #endregion

    #region Handle Mouse Interaction
    public void ClickedOn()
    {
        mousePositionOffset = transform.position - GetMousePosition();
        mouseClickedOn = true;
        transform.SetParent(answerManager.superParent); //make this over other objects
    }

    public void ClickedRelease()
    {
        mouseClickedOn = false;
        transform.SetParent(answerManager.answerTextParent); //return back to original layer
        SetDestination(origLocalPosition, true);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        ATVisuals.BecomeDarkenedColor();
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        ATVisuals.BecomeOriginalColor();
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - cam.transform.position.z;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }

    #endregion

    #region Collision Handling

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (answerTextLayer == (answerTextLayer | (1 << collision.gameObject.layer)))
        {
            //Collision with Another Box
            if (mouseClickedOn)
            {
                AnswerTextBox other = collision.gameObject.GetComponent<AnswerTextBox>();
                if (other != null && !other.isMoving && other.answer.answerType == answer.answerType)
                {
                    SwapPositionsAmongAnswers(answer.answerType, answer.ranking, other.answer.ranking);
                }
            }
        }
    }

    #endregion

    #region Moving and Swapping
    private void MoveTo() //if it needs to go to a destination, it moves
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(startingPlace, destination, elapsed / transitionTime); //always moves in transitionTime
            elapsed += Time.deltaTime;

            if (elapsed >= transitionTime)
            {
                isMoving = false;
                transform.localPosition = destination;
                lastSwapped.lastSwapped = lastSwapped;
                lastSwapped = this;
                elapsed = 0.0f;
            }
        }
    }

    public void SetDestination(Vector3 newDest, bool startMoving = false) //sets destination without interfering with MoveTo()
    {
        startingPlace = transform.localPosition;
        elapsed = 0.0f;

        newDest.z = 0;
        destination = newDest;
        origLocalPosition = newDest;
        isMoving = startMoving;
    }

    public void SwapRankingsAmongAnswers(AnswerTypes type, int first, int second) //swaps interpersonal rankings (in answer)
    {
        AnswerTextBox tempAnswer = answerManager.generatedAnswers[type][first];
        answerManager.generatedAnswers[type][first] = answerManager.generatedAnswers[type][second];
        answerManager.generatedAnswers[type][second] = tempAnswer;

        int tempRanking = answerManager.generatedAnswers[type][first].answer.ranking;
        answerManager.generatedAnswers[type][first].ChangeRanking(answerManager.generatedAnswers[type][second].answer.ranking);
        answerManager.generatedAnswers[type][second].ChangeRanking(tempRanking);
    }

    public void SwapPositionsAmongAnswers(AnswerTypes type, int first, int second)
    {
        if (first != second)
        {
            for (int i = first; i != second; i += first < second ? 1 : - 1)
            {
                int j = i + (first < second ? 1 : -1);
               
                lastSwapped = answerManager.generatedAnswers[type][j];
                lastSwapped.lastSwapped = this;

                Vector3 tempOrigLocalPosition = lastSwapped.origLocalPosition;
                lastSwapped.SetDestination(origLocalPosition, true);
                origLocalPosition = tempOrigLocalPosition;
                
                SwapRankingsAmongAnswers(type, i, j);
            }

            GeneralFlowStateManager.instance.VisitPrompt();
        }

    }

    void ChangeRanking(int newRank)
    {
        answer.ranking = newRank;
        prompt.answerDictionary[answer.answerType][newRank] = answer;
    }
    #endregion

    #region misc
    public void SetToStone()
    {
        canBeMoved = false;
        ATVisuals.DeSpawnShadow();
    }
    #endregion

    #region Fields

    public bool MouseOver
    {
       get { return mouseOver; }
    }

    #endregion
}
