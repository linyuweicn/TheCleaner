using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC2_Conversation : MonoBehaviour
{
    private int convSequence;
    public NPCConversation Conversation1;
    public NPCConversation Conversation2;
    public NPCConversation Conversation3;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            convSequence++;

            if (convSequence == 1)
            {
                ConversationManager.Instance.StartConversation(Conversation1);
                Debug.Log("C1");
            }
            else
            {
                convSequence = Random.Range(2, 4);


                switch (convSequence)
                {
                    case 2:
                        ConversationManager.Instance.StartConversation(Conversation2);
                        Debug.Log("C2");
                        break;

                    case 3:
                        ConversationManager.Instance.StartConversation(Conversation3);
                        Debug.Log("C3");
                        break;
                }
            }


        }
    }
}
