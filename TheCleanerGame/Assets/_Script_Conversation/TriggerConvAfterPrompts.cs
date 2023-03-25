using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerConvAfterPrompts : MonoBehaviour
{
    //[SerializeField] NPCConversation EndingComments;
    [SerializeField] GameObject npc;
    [SerializeField] NPCConvWithWhiteBoard[] CharaColliders;
    private bool hasTriggered;
    public NPCConvWithWhiteBoard npcScript;
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
            npc.SetActive(true);
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
        npcScript.OnMouseDown();
        //ConversationManager.Instance.StartConversation(EndingComments);
    }

    public void enableCharacterColliders()
    {
        if (BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed
           && BrainstormGeneralManager.Instance.ContainerDictionary[1].Prompt.completed
           && BrainstormGeneralManager.Instance.ContainerDictionary[2].Prompt.completed
           )
        {
            for (int i = 0; i < CharaColliders.Length; i++)
            {
                CharaColliders[i].canTurnOffCollider = false;
                //Debug.Log(CharaColliders[i].name);
            }
        }
    }
}
