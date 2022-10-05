using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OldArrowFlowButton : MonoBehaviour //Handles Behavior of ArrowButtons
{
    #region variables
    [SerializeField] int change; //positive if going forward, negative if going backwards
    [SerializeField] Color highlightedColor;
    [SerializeField] Color clickColor;
    [SerializeField] OldPitchContainer themeContainer;
    [SerializeField] OldPitchContainer characterContainer;
    [SerializeField] OldPitchContainer detailContainer;
    OldPromptManager promptManager;
    Color origColor;
    Image img;
    bool clickable;
    #endregion

    #region initialization
    void Start()
    {
        promptManager = OldGeneralFlowStateManager.instance.promptManager;
        img = transform.parent.GetComponent<Image>();

        origColor = img.color;
        Physics.queriesHitTriggers = true; //so you can click on trigger hitboxes.
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }
    #region handle click events
    public void OnClick()
    {
        if (change > 0)
        {
            SwitchToNext();
        } else if (change < 0)
        {
            SwitchToPrior();
        }

        if (OldGeneralFlowStateManager.instance.answerManager.censorshipUI != null)
        {
            OldGeneralFlowStateManager.instance.answerManager.censorshipUI.Hide();
        }
    }

    private void OnMouseEnter()
    {
        CheckIfClickable();
        if (clickable)
        {
            img.color = highlightedColor;
        } else
        {
            img.color = origColor;
        }
    }

    private void OnMouseOver()
    {
        if (clickable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                img.color = clickColor;

                OnClick();

            }
            else if (Input.GetMouseButtonUp(0))
            {
                img.color = origColor;
                CheckIfClickable();
            }
        }
    }

    private void OnMouseExit()
    {
        img.color = origColor;
        clickable = false;
    }

    void CheckIfClickable() //determines if the button should be clickable
    {
        if (change > 0)
        {
            if (promptManager.GetNextPrompt() != null)
            {
                clickable = true;
            } else
            {
                clickable = false;
            }
        } else if (change < 0)
        {
            if (promptManager.GetLastPrompt() != null)
            {
                clickable = true;
            } else
            {
                clickable = false;
            }
        }
        
    }
    void SwitchToNext() //when clicked and it goes to next prompt
    {
        if (OldGeneralFlowStateManager.instance.focusedContainer == null)
        {
            OldPrompt p = promptManager.GetNextPrompt();
            OldPitchContainer container = p.pitchType == OldPitchTypes.Theme ? themeContainer :
                p.pitchType == OldPitchTypes.Character ? characterContainer : detailContainer;
            OldGeneralFlowStateManager.instance.TransitionToRank(p.pitchType, p.promptNo, container);
        } else
        {
            OldGeneralFlowStateManager.instance.TransitionToDefault(OldGeneralFlowStateManager.instance.focusedContainer);
        }
        
    }

    void SwitchToPrior() //when clicked and it goes to the last visited prompt
    {
        if (OldGeneralFlowStateManager.instance.focusedContainer == null)
        {
            OldPrompt p = promptManager.GetLastPrompt();
            OldPitchContainer container = p.pitchType == OldPitchTypes.Theme ? themeContainer :
                 p.pitchType == OldPitchTypes.Character ? characterContainer : detailContainer;
            OldGeneralFlowStateManager.instance.TransitionToRank(p.pitchType, p.promptNo, container);
        }
        else
        {
            OldGeneralFlowStateManager.instance.TransitionToDefault(OldGeneralFlowStateManager.instance.focusedContainer);
        }
    }

    #endregion
}
