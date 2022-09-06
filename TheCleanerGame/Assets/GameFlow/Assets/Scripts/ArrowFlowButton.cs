using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ArrowFlowButton : MonoBehaviour //Handles Behavior of ArrowButtons
{
    #region variables
    [SerializeField] int change; //positive if going forward, negative if going backwards
    [SerializeField] Color highlightedColor;
    [SerializeField] Color clickColor;
    [SerializeField] PitchContainer themeContainer;
    [SerializeField] PitchContainer characterContainer;
    [SerializeField] PitchContainer detailContainer;
    PromptManager promptManager;
    Color origColor;
    Image img;
    bool clickable;
    #endregion

    #region initialization
    void Start()
    {
        promptManager = GeneralFlowStateManager.instance.promptManager;
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
        if (GeneralFlowStateManager.instance.focusedContainer == null)
        {
            Prompt p = promptManager.GetNextPrompt();
            PitchContainer container = p.pitchType == PitchTypes.Theme ? themeContainer :
                p.pitchType == PitchTypes.Character ? characterContainer : detailContainer;
            GeneralFlowStateManager.instance.TransitionToRank(p.pitchType, p.promptNo, container);
        } else
        {
            GeneralFlowStateManager.instance.TransitionToDefault(GeneralFlowStateManager.instance.focusedContainer);
        }
        
    }

    void SwitchToPrior() //when clicked and it goes to the last visited prompt
    {
        if (GeneralFlowStateManager.instance.focusedContainer == null)
        {
            Prompt p = promptManager.GetLastPrompt();
            PitchContainer container = p.pitchType == PitchTypes.Theme ? themeContainer :
                 p.pitchType == PitchTypes.Character ? characterContainer : detailContainer;
            GeneralFlowStateManager.instance.TransitionToRank(p.pitchType, p.promptNo, container);
        }
        else
        {
            GeneralFlowStateManager.instance.TransitionToDefault(GeneralFlowStateManager.instance.focusedContainer);
        }
    }

    #endregion
}
