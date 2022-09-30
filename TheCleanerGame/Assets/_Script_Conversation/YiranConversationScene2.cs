using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class YiranConversationScene2 : MonoBehaviour
{
    public  NPCConversation [] Conversations;
    public GameObject[] DisabledObjects;
    private int convSequence;
    bool isConOver;

    void Start()
    {
        for (int i = 0; i < DisabledObjects.Length; i++)
        {
            DisabledObjects[i].SetActive(true);
        }

        isConOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive)
        {

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(false);
            }

        }
        else
        {
            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(true);
            }
        }
    }

    private void OnMouseOver()
    {
      

        if (Input.GetMouseButtonDown(0))
        {
            
            convSequence++;

            if (Conversations.Length >1) // when there are more than 1 conversation
            {

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
            else
            {
                ConversationManager.Instance.StartConversation(Conversations[0]);
                Debug.Log("C1");

            }


        }

        
    }


}
