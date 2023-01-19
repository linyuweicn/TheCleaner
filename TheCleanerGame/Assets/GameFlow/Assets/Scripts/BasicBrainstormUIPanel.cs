using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBrainstormUIPanel : BrainstormPanelUI
{
    [SerializeField] List<MonoBehaviour> componentList;
    [SerializeField] List<GameObject> gameObjectList;


    public override void TransitionFromStates(BrainstormState oldState, BrainstormState newState)
    {
        bool foundInActiveStates = false;
        foreach (BrainstormState s in activeStates)
        {
            if (s == newState)
            {
                foundInActiveStates = true;
            }
        }

        if (foundInActiveStates)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        foreach (MonoBehaviour c in componentList)
        {
            c.enabled = true;
        }

        foreach (GameObject g in gameObjectList)
        {
            g.SetActive(true);
        }
    }

    public override void Hide()
    {
        foreach (MonoBehaviour c in componentList)
        {
            c.enabled = false;
        }

        foreach (GameObject g in gameObjectList)
        {
            g.SetActive(false);
        }
    }
}
