using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptManager : MonoBehaviour
{
    #region variables
    public static PromptManager Instance { get; private set; }

    public Dictionary<int, Dictionary<int, PromptObject>> PromptDictionary; //[Day][n-th Prompt]

    [SerializeField] int totalTheme, totalCharacter, totalSetting, totalNarration, TotalProtagonist, TotalArt;
    int completedTheme, completedCharacter, completedSetting, completedNarration, completedProtagonist, completedArt;
    //[SerializeField] GameObject ExitDoor;// enable the door when prompts are completed

    #endregion

    #region initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
        PromptDictionary = new Dictionary<int, Dictionary<int, PromptObject>>();
    }
    void Start()
    {
        //ExitDoor.SetActive(false);
    }

    void InitializeKey(int key)
    {
        if (!PromptDictionary.ContainsKey(key))
        {
            PromptDictionary.Add(key, new Dictionary<int, PromptObject>());
        }
    }

    #endregion

    public void AddPrompt(PromptObject prompt)
    {
        InitializeKey(prompt.ID.x);
        PromptDictionary[prompt.ID.x].Add(prompt.ID.y, prompt);
    }

    #region Get/Set Functions
    public int GetTotalPromptCount(PromptType type)
    {
        switch (type)
        {
            case PromptType.Theme:
                return totalTheme;
            case PromptType.Character:
                return totalCharacter;
            case PromptType.Setting:
                return totalSetting;
            case PromptType.Narration:
                return totalNarration;
            case PromptType.Protagonist:
                return TotalProtagonist;
            case PromptType.Art:
                return TotalArt;
            default:
                return 0;
        }
    }

    public int GetCompletedPromptCount(PromptType type)
    {
        switch (type)
        {
            case PromptType.Theme:
                return completedTheme;
            case PromptType.Character:
                return completedCharacter;
            case PromptType.Setting:
                return completedSetting;
            case PromptType.Narration:
                return completedNarration;
            case PromptType.Protagonist:
                return completedProtagonist;
            case PromptType.Art:
                return completedArt;
            default:
                return 0;
        }
    }

    public void IncrementCompletedPromptCount(PromptType type)
    {
        switch (type)
        {
            case PromptType.Theme:
                completedTheme++;
                break;
            case PromptType.Character:
                completedCharacter++;
                break;
            case PromptType.Setting:
                completedSetting++;
                break;
            case PromptType.Narration:
                completedNarration++;
                break;
            case PromptType.Protagonist:
                completedProtagonist++;
                break;
            case PromptType.Art:
                completedArt++;
                break;
        }
    }

    public void MarkPromptAsCompleted(PromptObject prompt)
    {
        if (!prompt.completed)
        {
            prompt.completed = true;
            IncrementCompletedPromptCount(prompt.Type);
            //ExitDoor.SetActive(true);
        }
    }
    #endregion
}
