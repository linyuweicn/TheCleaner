using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeginFeedbackUI : FeedbackUI
{
    #region BeginUI
    [System.Serializable]
    class BeginUI
    {
        public string feedbacktext;
        [System.Serializable]
        public struct PromptInfo
        {
            public PromptObject prompt;
            public int column;
        };
        public List<PromptInfo> referencedPrompts;

        public string GetText()
        {
            return FillInPromptPlaceholders();
        }
        public string FillInPromptPlaceholders()
        {
            string output = "";
            bool overwrite = false;
            int promptIndex = 0;

            for (int i = 0; i < feedbacktext.Length; i++)
            {
                if (!overwrite)
                {
                    if (feedbacktext[i] == '[')
                    {
                        if (referencedPrompts.Count > promptIndex && referencedPrompts[promptIndex].prompt.Answers != null &&
                            referencedPrompts[promptIndex].prompt.Answers[referencedPrompts[promptIndex].column][0] != null)
                        {
                            overwrite = true;
                            output += referencedPrompts[promptIndex].prompt.Answers[referencedPrompts[promptIndex].column][0].text;
                        }
                        else
                        {
                            output += '[';
                        }
                        promptIndex++;
                    }
                    else
                    {
                        output += feedbacktext[i];
                    }
                }
                else if (feedbacktext[i] == ']')
                {
                    overwrite = false;
                }
            }

            return output;
        }
    };
    #endregion

    [SerializeField] GameObject displayBubble;
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] List<BeginUI> beginUIList;

    void Start()
    {
        feedbackUIContainer.AddToFeedbackDictionary(this);
        Show();
    }

    public override void Show()
    {
        displayBubble.SetActive(true);
        BeginUI random = beginUIList[Random.Range(0, beginUIList.Count)];
        feedbackText.text = random.GetText();
    }
    public override void Hide()
    {
        displayBubble.SetActive(false);
    }

    public override void OnClick(FeedbackButton button)
    {
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
        Hide();
    }

    public override void Trigger(AnswerObject answer)
    {
        
    }
}