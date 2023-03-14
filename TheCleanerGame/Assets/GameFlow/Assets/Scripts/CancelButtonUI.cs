using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButtonUI : BrainstormPanelUI
{
    [SerializeField] GameObject button;

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (newState == BrainstormState.Menu)
        {
            if (BrainstormGeneralManager.Instance.CheckIfAllDailyPromptsHaveBeenCompleted())
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        button.SetActive(true);
    }

    public override void Hide()
    {
        button.SetActive(false);
    }
}
