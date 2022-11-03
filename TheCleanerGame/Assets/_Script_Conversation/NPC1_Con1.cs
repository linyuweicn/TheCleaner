using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class NPC1_Con1 : MonoBehaviour
{
    // this script should be intergrated with NPCConWithWhiteBoard.


    private int convSequence=0;
    public NPCConversation[] B4CompleteConversations;
    [SerializeField] GameObject[] GameObjets;
    Collider2D CharaCollider;
    public static bool canTurnOffCollider; //GeneralObjects uses this 
    private Button mybutton;
     


    void Start()
    {
        CharaCollider = gameObject.GetComponent<Collider2D>();
        mybutton = gameObject.GetComponent<Button>();
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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ConversationManager.Instance.SelectPreviousOption();
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ConversationManager.Instance.SelectNextOption();
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            ConversationManager.Instance.PressSelectedOption();

    }

    public void OnMouseOver()
    {
        if (mybutton = null)
        {
            ClikOnNonButtonConv();
        }
        else
        {
            ClickButtonPeople();

        }
       


    }


    public void ClikOnNonButtonConv()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            convSequence++;
            //Debug.Log(convSequence);
            if (convSequence <= B4CompleteConversations.Length)
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequence - 1]);

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

        }

    }
    public void ClickButtonPeople()
    {
        if (Input.GetMouseButtonDown(0))
        {
            convSequence++;
            if (convSequence <= B4CompleteConversations.Length)
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequence - 1]);

            }
            else
            {
                Debug.Log("should disable button");
              /*  ColorBlock colorVar = mybutton.colors;
                colorVar.highlightedColor = new Color(255, 255, 255);
                mybutton.colors = colorVar;*/
           
            }
        }
    }
}
