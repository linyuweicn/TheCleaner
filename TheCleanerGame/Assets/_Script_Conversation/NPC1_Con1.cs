using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC1_Con1 : MonoBehaviour
{
    private int convSequence=0;
    public NPCConversation[] B4CompleteConversations;
    [SerializeField] GameObject[] GameObjets;
    Collider2D CharaCollider;
    public static bool canTurnOffCollider; //GeneralObjects uses this 
    

    void Start()
    {
        CharaCollider = gameObject.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
       if (ConversationManager.Instance != null ) 
        {
            if (ConversationManager.Instance.IsConversationActive)
            {

                for (int i = 0; i < GameObjets.Length; i++)
                {
                    GameObjets[i].SetActive(false); // do not set collider to unenabled for those gameObjects. It will conflict with GenrealObjects Scripst.
                    
                }


            }
            else // if conv is not active
            {
                for (int i = 0; i < GameObjets.Length; i++)
                {
                    GameObjets[i].SetActive(true); // do not set collider to unenabled for those gameObjects. It will conflict with GenrealObjects Scripst.

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
            //Debug.Log(convSequence);
            if (convSequence <= B4CompleteConversations.Length)
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequence-1]);

                //diable the first item, do not let it had more conversation.
                
                if (convSequence == B4CompleteConversations.Length)
                {
                    canTurnOffCollider = true; 
                    if (canTurnOffCollider)
                    {
                        CharaCollider.enabled = false;
                    }

                    
                }

            }
          

            /*if (B4CompleteConversations.Length > 1)
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
                
            }*/


        }

        
    }

  /*  public void ClickButtonPeople()
    {
        if (Input.GetMouseButtonDown(0))
        {

            convSequence++;
            ConversationManager.Instance.StartConversation(B4CompleteConversations[0]);
            Debug.Log("C1 button");
        }
    }*/
}
