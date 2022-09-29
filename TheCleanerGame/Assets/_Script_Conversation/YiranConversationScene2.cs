using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class YiranConversationScene2 : MonoBehaviour
{
    public  NPCConversation [] Conversations;
    private int convSequence;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            convSequence++;

            if (convSequence == 1)
            {
                ConversationManager.Instance.StartConversation(Conversations[0]);
                Debug.Log("C1");
            }
            else
            {
                convSequence = Random.Range(2, 5);


                switch (convSequence)
                {
                    case 2:
                        ConversationManager.Instance.StartConversation(Conversations[1]);
                        Debug.Log("C2");
                        break;

                    case 3:
                        ConversationManager.Instance.StartConversation(Conversations[2]);
                        Debug.Log("C3");
                        break;

                    case 4:
                        ConversationManager.Instance.StartConversation(Conversations[3]);
                        Debug.Log("C4");
                        break;

                }
            }

        }
    }
}
