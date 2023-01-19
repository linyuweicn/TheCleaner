using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BrainstormPanelUI : MonoBehaviour
{
    [SerializeField] protected BrainstormGeneralManager brainstormManager;
    [SerializeField] protected List<BrainstormState> activeStates;

    private void Awake()
    {
        AddToBrainstormGeneralManager();
    }

    public void AddToBrainstormGeneralManager()
    {
        brainstormManager.AddPanelUIToGeneralManager(this, activeStates);
    }
    public abstract void TransitionFromStates(BrainstormState oldState, BrainstormState newState);

    public abstract void Hide();
    public abstract void Show();
}
