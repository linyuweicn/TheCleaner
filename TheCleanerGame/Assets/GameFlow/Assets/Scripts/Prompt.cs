using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable] public class Prompt
{
    // Start is called before the first frame update
    public string text;
    public static Prompt active;
    public List<Answer> answers;

    public PitchTypes type;
    public int promptNo;

    int slots;
    List<string> origText;

    public Prompt()
    {
        answers = new List<Answer>();
    }
    public Prompt(string text)
    {
        answers = new List<Answer>();

        this.text = text;
    }

    public void Construct()
    {
        CalcSlots();
        SetUpOrigText();
    }

    void CalcSlots()
    {
        if (text != null)
        {
            slots = text.Count(f => (f == '['));
        }
    }

    void SetUpOrigText()
    {
        origText = new List<string>();

        for (int i = 0; i < slots; i++)
        {
            origText.Add(GetOrigText(text, i));
        }
    }

    public int Slots
    {
        get { return slots; }
    }

    public bool AddAnswer(Answer ans)
    {
        if (answers.Count < slots && !answers.Contains(ans))
        {
            answers.Add(ans);
            ReplaceSlotWithAnswer(ans);

            if (MaxedOut())
            {
                PitchContainer.instances[type].AddCompletedPrompts(1);
            }

            return true;
        } else
        {
            return false;
        }
    }

    public bool RemoveAnswer(Answer ans)
    {
        if (answers.Remove(ans))
        {
            RevertAnswerInSlot(ans.slotNo);

            if (answers.Count + 1 == Slots)
            {
                PitchContainer.instances[type].AddCompletedPrompts(-1);
            }

            return true;
        } else
        {
            return false;
        }
    }

    string GetOrigText(string phrase, int slotNo)
    {
        int leftBracket = -1;
        int rightBracket = -1;
        FindBracketedSection(phrase, slotNo, ref leftBracket, ref rightBracket);

        Debug.Log(leftBracket + " " + rightBracket + " " + slotNo);

        if (leftBracket < 0 || rightBracket < 0)
        {
            return null;
        } else
        {
            return phrase.Substring(leftBracket + 1, rightBracket - leftBracket - 1);
        }
    }

    public void FindBracketedSection(string phrase, int slotNo, ref int leftBracket, ref int rightBracket)
    { //exclusive of brackets
        int bracketFound = 0;
        bool searching = true;
        for (int i = 0; i < phrase.Length; i++)
        {
            if (searching)
            {
                if (phrase[i] == '[')
                {
                    bracketFound++;
                }
                if (bracketFound - 1 == slotNo)
                {
                    searching = false;
                    leftBracket = i;
                }
            } else
            {
                if (phrase[i] == ']')
                {
                    rightBracket = i;
                    return;
                }
            }
            
        }
        leftBracket = -1;
        rightBracket = -1;
    }

    string ReplacePhrase(string phrase, int slotNo, string substitute)
    {
        int leftBracket = -1;
        int rightBracket = -1;
        FindBracketedSection(phrase, slotNo, ref leftBracket, ref rightBracket);

        if (leftBracket < 0 || rightBracket < 0)
        {
            return null;
        }
        else
        {
            return CorrectAandAn(phrase.Substring(0, leftBracket + 1) + substitute + phrase.Substring(rightBracket));
        }
    }

    string CorrectAandAn(string phrase)
    {
        List<string> words = new List<string>(phrase.Split(' '));
        string output = words[0];
        string prevWord = words[0];
        for (int i = 1; i < words.Count; i++)
        {
            if (prevWord == "a" && IsVowel(words[i][0]))
            {
                output += "n";
            }
            if (prevWord == "an" && !IsVowel(words[i][0]))
            {
                output.Remove(output.Length - 1);
            }
            output += " " + words[i];
        }
        return output;
    }

    public void ReplaceSlotWithAnswer(Answer answer)
    {
        text = ReplacePhrase(text, answer.slotNo, answer.text);
    }

    public void RevertAnswerInSlot(int slotNo)
    {
        text = ReplacePhrase(text, slotNo, origText[slotNo]);
    }

    public bool MaxedOut()
    {
        return answers.Count == slots;
    }

    bool IsVowel(char c)
    {
        return "aeiouAEIOU".IndexOf(c) >= 0;
    }
}
