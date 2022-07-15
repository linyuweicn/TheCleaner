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
    Color origColor;
    FlowTransitionManager fMang;
    PromptManager pMang;
    Image img;
    bool clickable;
    void Start()
    {
        fMang = FindObjectOfType<FlowTransitionManager>();
        pMang = FindObjectOfType<PromptManager>();
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
            clickable = pMang.NextPrompt != null;
        }
        else if (change < 0)
        {
            clickable = pMang.NextPrompt == null || pMang.NextPrompt.type != 0 || pMang.NextPrompt.promptNo != 0 || pMang.NextSlot != 0;
        }
    }

    void SwitchToNext()
    {
        switch (fMang.FlowState)
        {
            case FlowTransitionManager.State.Default:
                if (pMang.NextPrompt != null)
                {
                    fMang.ChangePrompt(pMang.NextPrompt.type, pMang.NextPrompt.promptNo, pMang.NextSlot);
                }
                break;
            case FlowTransitionManager.State.Focused:
                fMang.ToNextSlot();
                break;
            default:
                break;
        }

    }

    void SwitchToPrior()
    {
        switch (fMang.FlowState)
        {
            case FlowTransitionManager.State.Default:
                if (pMang.NextPrompt == null || pMang.NextPrompt.type != 0 || pMang.NextPrompt.promptNo != 0 || pMang.NextSlot != 0)
                {
                    PitchTypes type = 0;
                    int pNo = -1;
                    int sNo = -1;
                    pMang.GetPriorPrompt(pMang.NextPrompt, ref type, ref pNo, ref sNo);
                    fMang.ChangePrompt(type, pNo, sNo);
                }
                break;
            case FlowTransitionManager.State.Focused:
                fMang.ToPriorSlot();
                break;
            default:
                break;
        }
    }
}
