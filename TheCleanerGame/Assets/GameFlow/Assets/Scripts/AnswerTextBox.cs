using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerTextBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image img;
    [SerializeField] public Collider2D collider;
    Prompt prompt;
    public Answer answer;
    AnswerManager answerManager;

    [SerializeField] Color orange;
    [SerializeField] Color blue;
    [SerializeField] Color purple;
    [SerializeField] Color darkOrange;
    [SerializeField] Color darkBlue;
    [SerializeField] Color darkPurple;
    [SerializeField] float transitionTime;

    bool mouseOver;
    bool mouseClickedOn;
    bool darkenedColor;
    bool isMoving;
    bool canBeMoved;

    Vector3 origPosition;
    Vector3 origLocalPosition;
    Vector3 mousePositionOffset;
    Camera cam;
    ShadowTextBox spawnedShadow;
    Vector3 destination;
    Vector3 startingPlace;
    AnswerTextBox lastSwapped;
    float elapsed = 0.0f;
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

    // Update is called once per frame
    void Update()
    {
        CheckToChangeColor();
        
        if (mouseClickedOn)
        {
            transform.position = mousePositionOffset + GetMousePosition();
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);
            
            if (!Input.GetMouseButton(0))
            {
                ClickedRelease();
            }

            CheckIfCollideWithAnswerBoxes();
        } else
        {
            MoveTo();
        }
    }

    public void Construct(Prompt prompt, Answer answer, int ranking)
    {
        this.prompt = prompt;
        this.answer = answer;

        if (answer.ranking < 0)
        {
            ChangeRanking(ranking);
        }
        
        text.text = answer.text;

        BecomeOriginalColor();
        GenerateShadow();
    }

    void CheckToChangeColor()
    {
        if (!Input.GetMouseButton(0) && canBeMoved)
        {
            if (mouseOver && !darkenedColor)
            {
                darkenedColor = true;
                BecomeDarkenedColor();
            }
            else if (!mouseOver && darkenedColor)
            {
                darkenedColor = false;
                BecomeOriginalColor();
            }
        }
    }

    void BecomeOriginalColor()
    {
        switch (answer.answerType)
        {
            case AnswerTypes.Orange:
                img.color = orange;
                break;
            case AnswerTypes.Blue:
                img.color = blue;
                break;
            case AnswerTypes.Purple:
                img.color = purple;
                break;
        }
    }

    void BecomeDarkenedColor()
    {
        switch (answer.answerType)
        {
            case AnswerTypes.Orange:
                img.color = darkOrange;
                break;
            case AnswerTypes.Blue:
                img.color = darkBlue;
                break;
            case AnswerTypes.Purple:
                img.color = darkPurple;
                break;
        }
    }

    public void SelfDestruct()
    {
        DeSpawnShadow();
        Destroy(gameObject);
    }

    public void ClickedOn()
    {
        origPosition = transform.position;

        mousePositionOffset = origPosition - GetMousePosition();
        mouseClickedOn = true;
        transform.SetParent(answerManager.superParent);
    }

    public void ClickedRelease()
    {
        mouseClickedOn = false;

        transform.SetParent(answerManager.answerTextParent);
        SetDestination(origLocalPosition, true);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canBeMoved)
        {
            ClickedOn();
        }
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 100;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 1;
        return mousePosition;
    }

    private void CheckIfCollideWithAnswerBoxes()
    {
        foreach (AnswerTextBox a in answerManager.generatedAnswers[answer.answerType].Values)
        {
            if (a != this && a.collider.bounds.Contains(transform.position) && lastSwapped != a)
            {
                //Debug.Log("Collide with " + a.text.text + " " + answer.ranking + " " + answer.text);
                SwapPositionsForAnswers(answer.answerType, answer.ranking, a.answer.ranking);
                break;
            }
        }
    }

    private void GenerateShadow()
    {
        spawnedShadow = answerManager.SpawnShadow(transform.position);
    }

    private void DeSpawnShadow()
    {
        if (spawnedShadow != null)
        {
            spawnedShadow.SelfDestruct();
            spawnedShadow = null;
        }
    }

    private void MoveTo()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(startingPlace, destination, elapsed / transitionTime);
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

    public void SetDestination(Vector3 newDest, bool startMoving = false)
    {
        startingPlace = transform.localPosition;
        elapsed = 0.0f;

        newDest.z = 0;
        destination = newDest;
        origLocalPosition = newDest;
        isMoving = startMoving;

        Debug.Log("OrigDestination is :" + newDest);
    }

    public void SwapRankingsAmongAnswers(AnswerTypes type, int first, int second)
    {
        AnswerTextBox tempAnswer = answerManager.generatedAnswers[type][first];
        answerManager.generatedAnswers[type][first] = answerManager.generatedAnswers[type][second];
        answerManager.generatedAnswers[type][second] = tempAnswer;

        int tempRanking = answerManager.generatedAnswers[type][first].answer.ranking;
        answerManager.generatedAnswers[type][first].ChangeRanking(answerManager.generatedAnswers[type][second].answer.ranking);
        answerManager.generatedAnswers[type][second].ChangeRanking(tempRanking);
    }

    public void SwapPositionsForAnswers(AnswerTypes type, int first, int second)
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

    public void SetToStone()
    {
        canBeMoved = false;
        DeSpawnShadow();
    }
}
