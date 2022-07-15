using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

public class PromptManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextAsset promptText;
    [SerializeField] TextMeshProUGUI promptBox;
    [SerializeField] TextMeshProUGUI promptHeader;
    Prompt activePrompt;
    Prompt nextPrompt;
    int nextSlot;
    public int slotNo;
    public Dictionary<PitchTypes, List<Prompt>> prompts;

    private void Awake()
    {
        ReadPrompts(promptText.text);
    }
    void Start()
    {
        CalculateNextPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    [Serializable] private class PromptHolder
    {
        public List<Prompt> prompts;
        Dictionary<PitchTypes, int> countNo;

        public PromptHolder()
        {
            
        }
        public void Construct()
        {
            CountValues();
            foreach (Prompt p in prompts)
            {
                p.Construct();
            }
        }
        void CountValues()
        {
            countNo = new Dictionary<PitchTypes, int>();

            foreach (PitchTypes p in Enum.GetValues(typeof(PitchTypes)))
            {
                countNo.Add(p, 0);
            }

            foreach (Prompt p in prompts)
            {
                switch (p.type)
                {
                    case PitchTypes.Theme:
                        countNo[PitchTypes.Theme]++;
                        break;
                    case PitchTypes.Character:
                        countNo[PitchTypes.Character]++;
                        break;
                    case PitchTypes.Detail:
                        countNo[PitchTypes.Detail]++;
                        break;
                    default:
                        break;
                }
            }
        }
        public int GetNumberOf(PitchTypes type)
        {
            return countNo[type];
        }

    }

    void ReadPrompts(string text)
    {
        PromptHolder holder = JsonUtility.FromJson<PromptHolder>(text);
        holder.Construct();

        prompts = new Dictionary<PitchTypes, List<Prompt>>();
        
        foreach (PitchTypes type in Enum.GetValues(typeof(PitchTypes)))
        {
            prompts.Add(type, new List<Prompt>());

            for (int i = 0; i < holder.GetNumberOf(type); i++)
            {
                prompts[type].Add(null);
            }
        }
        
        foreach (Prompt p in holder.prompts)
        {
            prompts[p.type][p.promptNo] = p;
        }

    }

    public Prompt GetPrompt(PitchTypes type, int promptNo)
    {
        if (prompts.ContainsKey(type))
        {
            if (prompts[type].Count > promptNo && promptNo >= 0)
            {
                return prompts[type][promptNo];
            } else
            {
                return prompts[type][0];
            }
        }
        return null;
    }

    public Prompt ActivePrompt
    {
        get { return activePrompt; }
    }

    public void SetNewActivePrompt(PitchTypes type, int promptNo, int slotNo)
    {
        activePrompt = GetPrompt(type, promptNo);
        this.slotNo = slotNo % activePrompt.Slots;
        UpdatePromptText();
    }

    public void ClearActivePrompt()
    {
        activePrompt = null;
    }

    public void UpdatePromptText()
    {
        promptHeader.text = "Prompt " + (activePrompt.promptNo + 1) + ":";
        promptBox.text = activePrompt.text;
    }

    public void ChangeSlotNumber(int slotNo)
    {
        this.slotNo = slotNo;
    }
    public void CalculateNextPrompt()
    {
        foreach (PitchTypes t in prompts.Keys)
        {
            for (int pNo = 0; pNo < prompts[t].Count; pNo++)
            {
                if (!prompts[t][pNo].MaxedOut())
                {
                    nextPrompt = prompts[t][pNo];
                    nextSlot = prompts[t][pNo].answers.Count;

                    return;
                }
            }
        }
        nextPrompt = null;
        nextSlot = -1;
    }

    public bool AddAnswerTo(Prompt prompt, Answer ans)
    {
        bool result = prompt.AddAnswer(ans);
        CalculateNextPrompt();
        return result;
    }

    public bool RemoveAnswerFrom(Prompt prompt, Answer ans)
    {
        bool result = prompt.RemoveAnswer(ans);
        CalculateNextPrompt();
        return result;
    }

    public void GetPriorPrompt(Prompt prompt, ref PitchTypes type, ref int pNo, ref int sNo)
    {
        if (prompt == null)
        {
            type = PitchTypes.Detail;
            pNo = prompts[type].Count - 1;
            sNo = prompts[type][pNo].Slots - 1;
        } else
        {
            if (sNo == 0)
            {
                if (pNo == 0)
                {
                    if (type == 0)
                    {
                        //nothing should happen
                    } else
                    {
                        type--;
                        pNo = prompts[type].Count - 1;
                        sNo = prompts[type][pNo].Slots - 1;
                    }
                } else
                {
                    pNo--;
                    sNo = prompts[type][pNo].Slots - 1;
                }

            } else
            {
                sNo--;
            }
        }
    }

    public Prompt NextPrompt
    {
        get { return nextPrompt; }
    }
    public int NextSlot
    {
        get { return nextSlot; }
    }
}
