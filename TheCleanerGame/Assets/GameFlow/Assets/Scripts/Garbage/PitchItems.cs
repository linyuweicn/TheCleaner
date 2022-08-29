using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PitchItems : MonoBehaviour
{
    // Start is called before the first frame update
    PromptManager pMang;
    FlowTransitionManager fMang;
    FlowTransition parent;
    TextMeshProUGUI text;
    
    [SerializeField] int promptNo;

    
    void Start()
    {
        pMang = FindObjectOfType<PromptManager>();
        fMang = FindObjectOfType<FlowTransitionManager>();
        parent = transform.parent.parent.GetComponent<FlowTransition>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (fMang.FlowState == FlowTransitionManager.State.Default)
        {
            parent.StartTransition(promptNo);
        }
    }

}
