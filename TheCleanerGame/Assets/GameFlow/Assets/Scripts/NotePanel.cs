using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject note;
    [SerializeField] TextMeshProUGUI text;
    bool mouseOver = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !mouseOver)
        {
            CloseNote();
        }
    }

    public void OpenNote()
    {
        UpdateTextForNote(GetTotalText());
        note.SetActive(true);
    }

    public void CloseNote()
    {
        note.SetActive(false);
    }

    void UpdateTextForNote(string str)
    {
        text.text = str;
    }

    string GetTotalText()
    {
        string output = "";
        foreach (BrainstormContainer c in BrainstormGeneralManager.Instance.ContainerDictionary.Values)
        {
            output += c.Prompt.Text;
            output += "\n\n";
        }
        return output;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

}
