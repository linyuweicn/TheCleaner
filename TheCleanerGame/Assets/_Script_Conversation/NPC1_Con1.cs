using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC1_Con1 : MonoBehaviour
{
    private int convSequence;
    public NPCConversation[] B4CompleteConversations;
    [SerializeField] Collider2D[] boxCollider2D;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (ConversationManager.Instance != null )
        {
            if (ConversationManager.Instance.IsConversationActive)
            {

                for (int i = 0; i < boxCollider2D.Length; i++)
                {
                    boxCollider2D[i].enabled = false;
                }


            }
            else
            {
                for (int i = 0; i < boxCollider2D.Length; i++)
                {
                    boxCollider2D[i].enabled = true;
                }
            }
        }
           
        
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            convSequence++;
            if (B4CompleteConversations.Length > 1)
            {
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
            else
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[0]);
                Debug.Log("C1");
            }

            
        }

        
    }

    public void ClickButtonPeople()
    {
        if (Input.GetMouseButtonDown(0))
        {

            convSequence++;
            ConversationManager.Instance.StartConversation(B4CompleteConversations[0]);
            Debug.Log("C1 button");
        }
    }
}
