using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ArrowButtonObject : MonoBehaviour
{
    #region variables
    [SerializeField] Image img;
    [SerializeField] ButtonType buttonType;
    [SerializeField] Color deactivatedColor;
    [SerializeField] Button button;
    bool activated;
    Color origColor;
    RankPanelManager rankPanelManager;
    FeedbackManager feedbackManager;
    AudioManager audioManager;

    #endregion
    private void Awake()
    {
        activated = true;
        origColor = img.color;
        rankPanelManager = FindObjectOfType<RankPanelManager>();
        feedbackManager = FindObjectOfType<FeedbackManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        if (rankPanelManager.State == RankPanelState.Feedback)
        {
            if (buttonType == ButtonType.Back)
            {
                DeactivateButton();
            }
            else if (buttonType == ButtonType.Forward)
            {
                ActivateButton();
            }
        }
        else if (BrainstormGeneralManager.Instance.state == BrainstormState.Menu)
        {
            if (buttonType == ButtonType.Back)
            {
                DeactivateButton();
            }
            else if (buttonType == ButtonType.Forward)
            {
                ActivateButton();
            }
        }
        else if (BrainstormGeneralManager.Instance.state == BrainstormState.Rank)
        {
            ActivateButton();
        }
        else if (BrainstormGeneralManager.Instance.state == BrainstormState.TransToMenu || BrainstormGeneralManager.Instance.state != BrainstormState.TransToRank)
        {
            DeactivateButton();
        }
    }
    public void ClickedOn()
    {
        switch (buttonType)
        {
            case ButtonType.Back:
                GoBack();                
                break;
            case ButtonType.Forward:
                GoForward();               
                break;
        }
    }

    public void GoBack()
    {
        if (BrainstormGeneralManager.Instance.state == BrainstormState.Rank)
        {
            BrainstormGeneralManager.Instance.SwitchToMenuState();
        }
  
    }

    public void GoForward()
    {
        if (rankPanelManager.State == RankPanelState.Feedback)
        {
            feedbackManager.ResetFeedback();
        }
        else if (BrainstormGeneralManager.Instance.state == BrainstormState.Rank)
        {
            RankNext();
        }
        else if (BrainstormGeneralManager.Instance.state == BrainstormState.Menu)
        {
            foreach (int i in BrainstormGeneralManager.Instance.ContainerDictionary.Keys)
            {
                if (!BrainstormGeneralManager.Instance.ContainerDictionary[i].Prompt.completed)
                {
                    BrainstormGeneralManager.Instance.SwitchToRankState(i);
                    return;
                }
            }
            BrainstormGeneralManager.Instance.SwitchToRankState(0);
        }

    }

    public void RankNext()
    {
        if (BrainstormGeneralManager.Instance.state == BrainstormState.Rank)
        {
            if (rankPanelManager.State != RankPanelState.Culled)
            {
                PromptManager.Instance.MarkPromptAsCompleted(BrainstormGeneralManager.Instance.FocusedContainer.Prompt);
                rankPanelManager.NextStage();
           
            }
            else
            {
                GoBack();
            }
        }
    }
    void DeactivateButton()
    {
        if (activated)
        {
            button.enabled = false;
            img.color = deactivatedColor;
            activated = false;
        }
    }

    void ActivateButton()
    {
        if (!activated)
        {
            button.enabled = true;
            img.color = origColor;
            activated = true;
        }
    }
}
