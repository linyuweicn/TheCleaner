using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrainstormGeneralManager : MonoBehaviour
{
    #region variables

    // List of scores, AveScore[0] is for Character, AveScore[1], AveScore[2] are for Narration, 
    // AveScore[3] AveScore[4] are for Theme
    public static List<float> AveScore = new List<float>(){0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    
    public static BrainstormGeneralManager Instance { get; private set; }
    
    public Dictionary<int, CardContainer> ContainerDictionary;
    public Dictionary<BrainstormState, List<BrainstormPanelUI>> PanelUIDictionary = new Dictionary<BrainstormState, List<BrainstormPanelUI>>();

    [SerializeField] PromptObject prompt; 
    [SerializeField] BrainstormState brainstormState;
    [SerializeField] FeedbackType feedbackState;

    [SerializeField] BrainstormEventManager eventManager;

    public PromptObject Prompt
    {
        get { return prompt; }
    }

    public BrainstormState MyBrainstormState
    {
       get { return brainstormState; }
    }

    public FeedbackType MyFeedbackState
    {
        get { return feedbackState; }
    }

    public BrainstormEventManager EventManager
    {
        get { return eventManager; }
    }
    #endregion

    #region initialization
    private void Awake()
    {
        Instance = this;
        Physics.queriesHitTriggers = false;

        ContainerDictionary = new Dictionary<int, CardContainer>();

    }

    private void Start()
    {
        brainstormState = BrainstormState.Menu; 
    }

    public void AddContainerToGeneralManager(CardContainer container)
    {
        if (ContainerDictionary.ContainsKey(container.PromptID))
        {
            throw new SystemException("There exists 2 prompts with the same id in Brainstorm containers. Make sure the Y ID is all unique, also make sure X is all the same");
        }
        ContainerDictionary[container.PromptID] = container;
    }

    public void AddPanelUIToGeneralManager(BrainstormPanelUI panelUI, List<BrainstormState> activeStates)
    {
        foreach (BrainstormState state in activeStates)
        {
            if (!PanelUIDictionary.ContainsKey(state))
            {
                PanelUIDictionary.Add(state, new List<BrainstormPanelUI>());
            }
            PanelUIDictionary[state].Add(panelUI);
        }
    }

    #endregion

    #region public functions
    public void SwitchState(BrainstormState targetState)
    {
        TriggerUIStateChanges(brainstormState, targetState);
        TriggerEventStateChanges(brainstormState, targetState);
        brainstormState = targetState;
    }

    public void SwitchFeedbackState(FeedbackType feedbackType)
    {
        feedbackState = feedbackType;
    }

    public void AssignPrompt(PromptObject prompt)
    {
        this.prompt = prompt;
    }

    public bool CheckIfAllDailyPromptsHaveBeenCompleted()
    {
        foreach (CardContainer c in ContainerDictionary.Values)
        {
            if (!c.Prompt.completed)
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region helper functions

    void TriggerUIStateChanges(BrainstormState oldState, BrainstormState targetState)
    {
        List<BrainstormPanelUI> triggeredUI = new List<BrainstormPanelUI>();

        foreach (BrainstormPanelUI ui in PanelUIDictionary[oldState])
        {
            if (!triggeredUI.Contains(ui)) { triggeredUI.Add(ui); }
        }

        foreach (BrainstormPanelUI ui in PanelUIDictionary[targetState])
        {
            if (!triggeredUI.Contains(ui)) { triggeredUI.Add(ui); }
        }

        foreach (BrainstormPanelUI ui in triggeredUI)
        {
            ui.TransitionFromStates(oldState, targetState);
        }
    }

    void TriggerEventStateChanges(BrainstormState oldState, BrainstormState targetState)
    {
        switch (oldState)
        {
            case BrainstormState.Menu:
                EventManager.OnExitMenuStateEvent();
                break;
            case BrainstormState.Rank:
                EventManager.OnExitRankingStateEvent();
                break;
            case BrainstormState.Decision:
                EventManager.OnExitDecisionStateEvent();
                break;
        }

        switch (targetState)
        {
            case BrainstormState.Menu:
                EventManager.OnEnterMenuStateEvent();
                break;
            case BrainstormState.Rank:
                EventManager.OnEnterRankingStateEvent();
                break;
            case BrainstormState.Decision:
                EventManager.OnEnterDecisionStateEvent();
                break;
        }
    }
    #endregion
}
