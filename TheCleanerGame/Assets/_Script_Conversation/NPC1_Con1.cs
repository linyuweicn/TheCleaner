using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC1_Con1 : MonoBehaviour
{
    private int convSequence;
    public NPCConversation[] B4CompleteConversations;
    public GameObject[] DisabledObjects;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            convSequence++;

            if (convSequence == 1)
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[0]);
                Debug.Log("C1");
            }
            else
            {
                convSequence = Random.Range(2, B4CompleteConversations.Length);


                switch (convSequence)
                {
                    case 2:
                        ConversationManager.Instance.StartConversation(B4CompleteConversations[1]);
                        Debug.Log("C2");
                        break;

                    case 3:
                        ConversationManager.Instance.StartConversation(B4CompleteConversations[2]);
                        Debug.Log("C3");
                        break;
                }
            }

            
        }
    }
}
