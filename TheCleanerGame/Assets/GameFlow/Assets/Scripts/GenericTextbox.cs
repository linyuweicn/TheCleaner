using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenericTextbox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI textBox;
    [SerializeField] Color highlighted;
    Color origColor;

    ScoreManager sMang;
    PromptManager pMang;
    AnswerManager aMang;
    FlowTransitionManager fMang;
    Answer answer;
    private void Awake()
    {
        sMang = FindObjectOfType<ScoreManager>();
        pMang = FindObjectOfType<PromptManager>();
        aMang = FindObjectOfType<AnswerManager>();
        fMang = FindObjectOfType<FlowTransitionManager>();
        origColor = textBox.color;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Relocate(Vector3 vec)
    {
        transform.localPosition = vec;
    }

    public void Selected()
    {
        Debug.Log("selected");  
        if (answer != null)
        {
            if (answer.selected)
            {
                RemoveFromSelected();
            } else
            {
                AddToSelected();
            }   
        }
    }

    public void AddToSelected()
    {
        if (pMang.AddAnswerTo(pMang.ActivePrompt, answer))
        {
            answer.selected = true;
            textBox.color = highlighted;
            
            pMang.UpdatePromptText();
            PitchContainer.instances[answer.type].UpdateFractions();
            sMang.AddPoints(answer);

            fMang.ToNextSlot();
        }
    }

    public void RemoveFromSelected()
    {
        if (pMang.RemoveAnswerFrom(pMang.ActivePrompt, answer))
        {
            answer.selected = false;
            textBox.color = origColor;
            pMang.UpdatePromptText();

            PitchContainer.instances[answer.type].UpdateFractions();
            sMang.TakePoints(answer);
        }
    }
    public void Clear()
    {
        answer = null;
        textBox.text = "";
    }
    public void AssignAnswer(Answer ans)
    {
        answer = ans;
        textBox.text = ans.text;

        if (pMang.ActivePrompt.answers.Contains(answer))
        {
            textBox.color = highlighted;
        } else
        {
            textBox.color = origColor;
        }
    }
}
