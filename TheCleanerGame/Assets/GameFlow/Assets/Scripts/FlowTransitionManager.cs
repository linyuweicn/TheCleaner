using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlowTransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float transTime;
    [SerializeField] Vector3 dest;
    public List<FlowTransition> transitions;
    
    FlowTransition focused;
    State state;
    PitchTypes activeType;
    
    GameFlowData gfData;
    PromptManager pMang;

    [SerializeField] TextMeshProUGUI promptHeaderTxt;
    [SerializeField] TextMeshProUGUI promptItemTxt;

    void Start()
    {
        FlowState = State.Default;
        transitions = new List<FlowTransition>(FindObjectsOfType<FlowTransition>());

        gfData = FindObjectOfType<GameFlowData>();
        pMang = FindObjectOfType<PromptManager>();

        TurnOffPrompt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public State FlowState
    {
        get { return state; }
        set { state = value; }
    }
    public float TransTime
    {
        get { return transTime; }
    }
    public Vector3 Dest
    {
        get { return dest; }
    }
    public PitchTypes Type
    {
        get { return activeType; }
    }
    public enum State
    {
        Default,
        Focused,
    }

    public void Focus(FlowTransition flow, int promptNo, int slotNo)
    {
        focused = flow;
        FlowState = State.Focused;
        activeType = flow.type;

        pMang.SetNewActivePrompt(activeType, promptNo, slotNo);
    }

    public void UnFocus()
    {
        focused = null;
        FlowState = State.Default;
        TurnOffPrompt();
        gfData.HideTextBoxes();

        foreach (FlowTransition f in transitions)
        {
            f.ResetPos();
        }
    }

    public void FinishFocus()
    {
        TurnOnPrompt();
        gfData.ShowTextBoxes();
    }

    public void TurnOffPrompt()
    {
        promptHeaderTxt.gameObject.SetActive(false);
        promptItemTxt.gameObject.SetActive(false);
    }

    public void TurnOnPrompt()
    {
        promptHeaderTxt.gameObject.SetActive(true);
        promptItemTxt.gameObject.SetActive(true);
    }

    public void ToPriorSlot()
    {
        if (pMang.slotNo == 0)
        {
            pMang.slotNo = 0;
            if (pMang.ActivePrompt.promptNo == 0)
            {
                UnFocus();
            } else
            {
                ChangePrompt(activeType, pMang.ActivePrompt.promptNo - 1, 0);
            }
        } else
        {
            ChangePrompt(pMang.ActivePrompt.type, pMang.ActivePrompt.promptNo, pMang.slotNo - 1);
        }
    }

    public void ToNextSlot()
    {
        if (pMang.slotNo + 1 < pMang.ActivePrompt.Slots)
        {
            ChangePrompt(pMang.ActivePrompt.type, pMang.ActivePrompt.promptNo, pMang.slotNo + 1);
        } else
        {
            pMang.slotNo = pMang.ActivePrompt.Slots;
            if (pMang.prompts[activeType].Count > pMang.ActivePrompt.promptNo + 1)
            {
                ChangePrompt(activeType, pMang.ActivePrompt.promptNo + 1, 0);
            } else
            {
                UnFocus();
            }
        }
    }
    
    public void UpdateAnswerData()
    {

        gfData.HideTextBoxes();
        gfData.ShowTextBoxes();
    }

    public void ChangePrompt(PitchTypes type, int promptNo, int slotNo)
    {

        pMang.SetNewActivePrompt(type, promptNo, slotNo);

        if (FlowState != State.Focused)
        {
            PitchContainer.instances[type].fTrans.StartTransition(promptNo);
        } else
        {
            UpdateAnswerData();
        }
    }

}