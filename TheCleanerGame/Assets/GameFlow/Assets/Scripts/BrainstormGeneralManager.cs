using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrainstormGeneralManager : MonoBehaviour
{
    #region variables

    // List of scores, AveScore[0] is for Character, AveScore[1], AveScore[2] are for Narration, 
    // AveScore[3] AveScore[4] are for Theme
    public static List<float> AveScore = new List<float>(){0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    
    public static BrainstormGeneralManager Instance { get; private set; }
    public Dictionary<int, BrainstormContainer> ContainerDictionary;
    [HideInInspector] int focusedContainer;
    [HideInInspector] public BrainstormState state;

    [SerializeField] GameObject rankPanel;
    [SerializeField] GameObject menuPanel;
    public RankPanelManager rankPanelManager;
    public BrainstormContainer FocusedContainer
    {
        get { return ContainerDictionary[focusedContainer]; }
    }
    #endregion

    #region initialization
    private void Awake()
    {
        Instance = this;
        ContainerDictionary = new Dictionary<int, BrainstormContainer>();
        Physics.queriesHitTriggers = false;
    }

    private void Start()
    {
        state = BrainstormState.Menu; 
        StartPreparingMenu();
    }

    public void AddContainterToList(BrainstormContainer container)
    {
        if (ContainerDictionary.ContainsKey(container.PromptID))
        {
            throw new SystemException("There exists 2 prompts with the same id in Brainstorm containers. Make sure the Y ID is all unique, also make sure X is all the same");
        }
        ContainerDictionary[container.PromptID] = container;
    }
    #endregion

    #region public functions
    public void SwitchToRankState(int focused)
    {
        if (state == BrainstormState.Menu)
        {
            focusedContainer = focused;

            state = BrainstormState.TransToRank;
            foreach (BrainstormContainer c in ContainerDictionary.Values)
            {
                c.MoveToRankState();
            }
            Action func = FinishPreparingRank;
            StartCoroutine(WaitUntilContainersBecome(ContainerState.Hidden, func));
        }
        else
        {
            Debug.LogError("Should not switch to rank state while not during menu state");
        }
    }

    public void SwitchToMenuState()
    {
        if (state == BrainstormState.Rank)
        {
            focusedContainer = -1;
            rankPanelManager.State = RankPanelState.Ranking;

            StartPreparingMenu();
            state = BrainstormState.TransToMenu;
            foreach (BrainstormContainer c in ContainerDictionary.Values)
            {
                c.MoveToMenuState();
            }
            Action func = FinishPreparingMenu;
            StartCoroutine(WaitUntilContainersBecome(ContainerState.Revealed, func));
        }
        else
        {
            Debug.LogError("Should not switch to menu state while not during rank state");
        }
    }
    #endregion

    #region helper functions


    void StartPreparingMenu()
    {
        menuPanel.SetActive(true);
        rankPanel.SetActive(false);
    }

    void FinishPreparingMenu()
    {
        state = BrainstormState.Menu;
        rankPanelManager.DestroyAllAnswers();
    }

    void FinishPreparingRank()
    {
        state = BrainstormState.Rank;
        rankPanel.SetActive(true);
        menuPanel.SetActive(false);

        if (FocusedContainer.Prompt.completed)
        {
            rankPanelManager.StartCulledStage();
        }
        else
        {
            rankPanelManager.UpdatePromptText();
            rankPanelManager.GenerateAnswers();
        }
    }

    IEnumerator WaitUntilContainersBecome(ContainerState cState, Action func)
    {
        bool ready = false;
        while (!ready)
        {
            ready = true;
            foreach (BrainstormContainer c in ContainerDictionary.Values)
            {
                if (c.State != cState)
                {
                    ready = false;
                    break;
                }
            }
            yield return null;
        }
        if (func != null)
        {
            func();
        }
    }

    #endregion
}
