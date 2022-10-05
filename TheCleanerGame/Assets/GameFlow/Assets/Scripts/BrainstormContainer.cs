using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BrainstormContainer : MonoBehaviour
{
    #region variables
    [SerializeField] PromptObject associatedPrompt;
    [SerializeField] Vector3 hiddenPosition;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI fractionText;

    Vector3 origPosition;
    ContainerState state;

    bool mouseOver;

    public ContainerState State { get { return state; } }
    public PromptObject Prompt { get { return associatedPrompt; } }
    public int PromptID { get { return associatedPrompt.ID.y; } }
    #endregion

    #region initialization
    private void Awake()
    {
        origPosition = transform.position;
        state = ContainerState.Revealed;
    }
    void Start()
    {
        BrainstormGeneralManager.Instance.AddContainterToList(this);
        PromptManager.Instance.AddPrompt(Prompt);
        UpdateText();
    }
    #endregion

    #region Update

    private void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickedOn();
            }
        }
    }

    #endregion

    public void MoveToRankState()
    {
        if (state == ContainerState.Revealed)
        {
            Action func = FinishSwitchingToRank;
            MoveToPosition(hiddenPosition, func);
        }
        else
        {
            Debug.LogError("Tries To Move To Rank in an unhandled situation at " + name);
        }
    }

    public void MoveToMenuState()
    {
        if (state == ContainerState.Revealed)
        {
            UpdateText();
            Action func = FinishSwitchingToMenu;
            MoveToPosition(origPosition, func);
        }
        else
        {
            Debug.LogError("Tries To Move To Menu in an unhandled situation at " + name);
        }
    }

    #region helper functions

    void MoveToPosition(Vector3 pos, Action func)
    {
        state = ContainerState.Moving;
        StartCoroutine(MovingTo(pos, speed, func));
    }

    IEnumerator MovingTo(Vector3 pos, float speed, Action func)
    {
        while (transform.position != pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pos) <= 0.1f)
            {
                transform.position = pos;
                break;
            }
            yield return null;
        }
        Debug.Log("Finished moving to position " + name);
        func();
    }

    void FinishSwitchingToMenu()
    {
        state = ContainerState.Revealed;
    }

    void FinishSwitchingToRank()
    {
        state = ContainerState.Hidden;
    }

    void UpdateText()
    {
        fractionText.text = PromptManager.Instance.GetCompletedPromptCount(Prompt.Type) + " / " + PromptManager.Instance.GetTotalPromptCount(Prompt.Type);
    }

    #endregion

    #region MouseClick Interface

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    public void ClickedOn()
    {
        Debug.Log("clicked on " + name);
        if (state == ContainerState.Revealed)
        {
            BrainstormGeneralManager.Instance.SwitchToRankState(PromptID);
        }
    }

    #endregion
}
