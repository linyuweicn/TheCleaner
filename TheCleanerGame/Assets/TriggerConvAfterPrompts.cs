using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerConvAfterPrompts : MonoBehaviour
{
    [SerializeField] NPCConversation EndingComments;
    private bool hasTriggered;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerConversation()
    {
        if (BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed
            &&BrainstormGeneralManager.Instance.ContainerDictionary[1].Prompt.completed
            &&BrainstormGeneralManager.Instance.ContainerDictionary[2].Prompt.completed
            && !hasTriggered)
        {
            Debug.Log("trigger end");
            StartCoroutine(StartConv());
            hasTriggered = true;
        }
        else
        {
            Debug.Log("not end yet");
        }
    }

    IEnumerator StartConv()
    {
        yield return new WaitForSeconds(1.1f);

        //Trigger conv after the animation + close the canvas
        // has been triggred is true
        ConversationManager.Instance.StartConversation(EndingComments);
    }
}
