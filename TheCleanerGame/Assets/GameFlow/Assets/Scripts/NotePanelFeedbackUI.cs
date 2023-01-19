using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePanelFeedbackUI : FeedbackUI
{
    // Start is called before the first frame update
    [SerializeField] GameObject note;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        feedbackUIContainer.AddToFeedbackDictionary(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Show()
    {
        OpenNote();
    }

    public override void Hide()
    {
        CloseNote();
    }

    public override void OnClick(FeedbackButton button)
    {
        if (note.activeSelf)
        {
            brainstormManager.SwitchFeedbackState(FeedbackType.Null);
            Hide();
        }
        else
        {
            brainstormManager.SwitchFeedbackState(FeedbackType.Note);
            Show();
        }
    }

    public override void Trigger(AnswerObject answer)
    {
        
    }

    public void OpenNote()
    {
        UpdateTextForNote(GetTotalText());
        note.SetActive(true);
    }

    public void CloseNote()
    {
        note.SetActive(false);
        brainstormManager.SwitchFeedbackState(FeedbackType.Null);
    }

    void UpdateTextForNote(string str)
    {
        text.text = str;
    }

    string GetTotalText()
    {
        string output = "";
        foreach (CardContainer c in BrainstormGeneralManager.Instance.ContainerDictionary.Values)
        {
            output += c.Prompt.Text;
            output += "\n\n";
        }
        return output;
    }

}
