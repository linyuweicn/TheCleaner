using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

public class OldPromptManager : MonoBehaviour //in charge of reading and storing prompts
{
    // Start is called before the first frame update
    [SerializeField] TextAsset promptTextFile;
    [SerializeField] TextAsset promptStatementTextFile; //prompt statements are when we convert prompts to statements
    [SerializeField] TextMeshProUGUI promptBox;
    [SerializeField] TextMeshProUGUI promptHeader;
    public Dictionary<OldPitchTypes, SortedDictionary<int, OldPrompt>> promptLists; //stores prompts
    //prompts are stored using their PitchType, and then their Prompt Number

    #region private classes
    [Serializable]
    private class PromptHolder
    {
        public List<OldPrompt> prompts;

        public PromptHolder()
        {
            prompts = new List<OldPrompt>();
        }
    }

    [Serializable]
    private class PromptStatementHolder
    {
        public List<OldPromptStatement> promptStatements;

        public PromptStatementHolder()
        {
            promptStatements = new List<OldPromptStatement>();
        }
    }

    #endregion
    private void Awake()
    {
        //initializes promptlists
        promptLists = new Dictionary<OldPitchTypes, SortedDictionary<int, OldPrompt>>();
        foreach (OldPitchTypes t in Enum.GetValues(typeof(OldPitchTypes)))
        {
            promptLists.Add(t, new SortedDictionary<int, OldPrompt>()); 
        }

        ReadPrompts(promptTextFile);
        ReadPromptStatements(promptStatementTextFile);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReadPrompts(TextAsset text)
    {
        PromptHolder holder = JsonUtility.FromJson<PromptHolder>(text.text);

        foreach (OldPrompt p in holder.prompts)
        {
            promptLists[p.pitchType].Add(p.promptNo, p);
        }
    }

    void ReadPromptStatements(TextAsset text)
    {
        PromptStatementHolder holder = JsonUtility.FromJson<PromptStatementHolder>(text.text);

        foreach (OldPromptStatement p in holder.promptStatements)
        {
            promptLists[p.pitchType][p.promptNo].statement = p;
        }
    }

    public OldPrompt GetPrompt(OldPitchTypes type, int promptNo) //accesses prompts
    {
        if (promptLists.ContainsKey(type))
        {
            if (promptLists[type].ContainsKey(promptNo))
            {
                return promptLists[type][promptNo];
            }
        }
        return null;
    }

    public int GetMaximum(OldPitchTypes type) //number of prompts of a certain pitch type
    {
        return promptLists[type].Count;
    }

    public int GetNumVisited(OldPitchTypes type) //number of prompts that were visited of a certain ptich type
    {
        int sum = 0;
        foreach (int i in promptLists[type].Keys)
        {
            if (promptLists[type][i].visited)
            {
                sum++;
            }
        }
        return sum;
    }

    public OldPrompt GetNextPrompt() //returns next unvisited prompt (order from theme -> character -> detail) (ordered from prompt number)
    {
        for (int i = 0; i < Enum.GetValues(typeof(OldPitchTypes)).Length; i++)
        {
            foreach (var pair in promptLists[(OldPitchTypes) i].OrderBy(p => p.Key))
            {
                if (!pair.Value.calculated)
                {
                    return pair.Value;
                }
            }
        }

        if (promptLists[(OldPitchTypes) 0].Count > 0)
        {
            var it = promptLists[(OldPitchTypes)0].GetEnumerator();
            return it.Current.Value;
        }

        return null;
    }

    public OldPrompt GetNextPrompt(OldPitchTypes type) //returns next unvisited prompt (ordered by prompt number) of a certain prompt type
    {
        foreach (var pair in promptLists[type].OrderBy(p => p.Key))
        {
            if (!pair.Value.calculated)
            {
                return pair.Value;
            }
        }

        if (promptLists[type].Count > 0)
        {
            var it = promptLists[(OldPitchTypes)0].GetEnumerator();
            it.MoveNext();
            return it.Current.Value;
        }

        return null;
    }

    public OldPrompt GetLastPrompt() //get the prompt before the next prompt
    {
        OldPrompt p = GetNextPrompt();

        if (p.promptNo > 0)
        {
            return GetPrompt(p.pitchType, p.promptNo - 1);
        } else
        {
            if (p.pitchType != (OldPitchTypes) (0))
            {
                OldPitchTypes lastType = p.pitchType - 1;
                IEnumerator it = null;
                return promptLists[lastType].Last<KeyValuePair<int, OldPrompt>>().Value;
            } else
            {
                return null;
            }
        }
    }
}
