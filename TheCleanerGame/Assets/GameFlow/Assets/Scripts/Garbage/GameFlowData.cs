using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameFlowData : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameFlowData instance;

    [Header("Handling Textbox Spawns")]
    [SerializeField] public List<Vector3> ansPositions;
    [SerializeField] GenericTextbox prefab;
    [SerializeField] GameObject parent;
    PromptManager pMang;
    AnswerManager aMang;
     public List<GenericTextbox> texts;

    private void Awake()
    {
       instance = this;    
    }
    void Start()
    {
        pMang = FindObjectOfType<PromptManager>();
        aMang = FindObjectOfType<AnswerManager>();
        texts = new List<GenericTextbox>();

        GenerateTextBoxes();
        HideTextBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Textboxes
    public void GenerateTextBoxes()
    {
        for (int i = 0; i < ansPositions.Count; i++)
        {
            GenericTextbox t = Instantiate(prefab, parent.transform, false);
            t.Relocate(ansPositions[i]);
            texts.Add(t);
        }
    }
    public void ShowTextBoxes()
    {
        ClearTextBoxes();
        aMang.GenerateAnswers(pMang.ActivePrompt.type, pMang.ActivePrompt.promptNo, pMang.slotNo);
        parent.SetActive(true);
    }
    public void HideTextBoxes()
    {
        parent.SetActive(false);
    }
    public void ClearTextBoxes()
    {
        foreach (GenericTextbox t in texts)
        {
            t.Clear();
        }
    }
    #endregion
}
