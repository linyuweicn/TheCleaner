using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ArrowFlowButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int change;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color clickColor;
    [SerializeField] PitchContainer themeContainer;
    [SerializeField] PitchContainer characterContainer;
    [SerializeField] PitchContainer detailContainer;
    PromptManager promptManager;
    Color origColor;
    Image img;
    bool clickable;
    void Start()
    {
        promptManager = GeneralFlowStateManager.instance.promptManager;
        img = transform.parent.GetComponent<Image>();

        origColor = img.color;
        Physics.queriesHitTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    void CheckIfClickable()
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

    void SwitchToNext()
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

    void SwitchToPrior()
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
}
