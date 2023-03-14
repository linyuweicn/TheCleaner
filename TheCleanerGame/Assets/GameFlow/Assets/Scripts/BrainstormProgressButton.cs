using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainstormProgressButton : BrainstormPanelUI
{
    [SerializeField] private Sprite rankSprite;
    [SerializeField] private Sprite decisionSprite;
    [SerializeField] private Color darkColor;
    [SerializeField] private Image image;
    [SerializeField] FeedbackUIContainer feedbackUI;

    private bool mouseOver;

    private void Update()
    {

    }

    public void OnClick()
    {
        feedbackUI.Hide();

        switch (brainstormManager.MyBrainstormState)
        {
            case BrainstormState.Rank:
                if (brainstormManager.Prompt.completed)
                {
                    image.sprite = decisionSprite;
                    brainstormManager.SwitchState(BrainstormState.Decision);
                }
                break;
            case BrainstormState.Decision:
                brainstormManager.SwitchState(BrainstormState.Menu);
                break;
        }
    }

    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        if (oldState == BrainstormState.Menu)
        {
            Show();
            if (newState == BrainstormState.Decision)
            {
                image.sprite = decisionSprite;
            }
        }
        else if (newState == BrainstormState.Menu)
        {
            Hide();
        }
    }

    public override void Show()
    {
        image.sprite = rankSprite;
        image.enabled = true;
    }

    public override void Hide()
    {
        image.enabled = false;
    }
}
