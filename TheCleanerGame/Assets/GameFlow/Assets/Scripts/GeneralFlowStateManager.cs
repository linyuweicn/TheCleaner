using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFlowStateManager : MonoBehaviour
{
    #region variables
    public static GeneralFlowStateManager instance;
    Dictionary<PitchTypes, Prompt> promptStates;
    [SerializeField] string promptTxt;
    #endregion

    #region private classes
    [Serializable]
    private class PromptHolder
    {
        public List<Prompt> prompts;

        public PromptHolder()
        {

        }
    }
    #endregion
    private void Awake()
    {
        foreach (PitchTypes type in Enum.GetValues(typeof(PitchTypes)))
        {

        }
    }
    void Start()
    {
        ReadPrompts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadPrompts(string text)
    {
        PromptHolder holder = JsonUtility.FromJson<PromptHolder>(text);

        foreach (PitchTypes)
    }
}
