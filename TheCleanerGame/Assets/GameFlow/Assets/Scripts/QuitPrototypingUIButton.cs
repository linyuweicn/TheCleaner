using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPrototypingUIButton : BrainstormPanelUI
{
    [SerializeField] GameObject button;
    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (newState == BrainstormState.Menu)
        {
            if (BrainstormGeneralManager.Instance.CheckIfAllDailyPromptsHaveBeenCompleted())
            {
                Show();
            }
            else
            {
                Hide();
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
