using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class BrainstormTutorial : MonoBehaviour
{
    [SerializeField] NPCConversation BeginingTutorial;
    [SerializeField] NPCConversation PanelTutorial;
    public bool skipTutorial; // for debug purpose 
    private bool hasStartedTutorial = false;    
    private bool hasClickedOnPanel = false;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTutorialConv()
    {
        yield return new WaitForSeconds(0.8f);

        //Trigger conv after the animation + close the canvas
        // has been triggred is true
        ConversationManager.Instance.StartConversation(BeginingTutorial);
    }

    IEnumerator StartPanelConv()
    {
        yield return new WaitForSeconds(0.5f);

        //Trigger conv after the animation + close the canvas
        // has been triggred is true
        ConversationManager.Instance.StartConversation(PanelTutorial);
    }

    public void  OnMouseDown()
    {
        if (!skipTutorial && !hasStartedTutorial)
        {
            StartCoroutine(StartTutorialConv());
            hasStartedTutorial = true;
        }
        
    }

    public void StartPanelTutorial()
    {
        if (!skipTutorial && !hasClickedOnPanel)
        {
            //start new conv and animations
            StartCoroutine(StartPanelConv());
            hasClickedOnPanel = true;
        }
    }


}
