using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems;

public class NPCConvWithWhiteBoard : MonoBehaviour
{

    
    public NPCConversation[] B4CompleteConversations;
    public NPCConversation[] AfterCompleteConversations;

    private int convSequenceB4;
    private int convSequenceAfter;

    [SerializeField] BoxCollider2D[] boxCollider2D;
    [SerializeField] Collider2D []CharaCollider;

    Vector3 origPosition;
    [SerializeField] Vector3 NewPosition;
    [SerializeField] Vector3 NewPosition2;
    [SerializeField] Vector3 NewPosition3;

    public bool startConvAtBegining;

    private bool canTurnOffCollider; //GeneralObjects uses this 


    void Start()
    {

        origPosition = transform.position;
        if (startConvAtBegining)
        {
            StartCoroutine(TriggerConv());
            Debug.Log("start 1 conv");
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (ConversationManager.Instance.IsConversationActive) 
        {


            for (int i = 0; i < boxCollider2D.Length; i++)
            {
                boxCollider2D[i].enabled = false;
            }

            for (int j = 0; j < CharaCollider.Length; j++)
            {
                CharaCollider[j].enabled = false;
            }
           
        }
        else //if conversation is not active
        {
          

            for (int i = 0; i < boxCollider2D.Length; i++)
            {
                boxCollider2D[i].enabled = true; 
            }
            
            if (canTurnOffCollider)
            {
                for (int j = 0; j < CharaCollider.Length; j++)
                {
                    CharaCollider[j].enabled = false;
                }
            }
            else
            {
                for (int j = 0; j < CharaCollider.Length; j++)
                {
                    CharaCollider[j].enabled = true;
                    
                }
            }
           
        }

 /*       if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ConversationManager.Instance.SelectPreviousOption();
        else if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
            ConversationManager.Instance.SelectNextOption();
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                ConversationManager.Instance.PressSelectedOption();*/
        
       
    }

    public void OnMouseDown()
    {
        if (!BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed)
        {
            
            StartConvBeforeWhieBoard();
            //Debug.Log("not completed");
            //MoveToNewPos();
        }
        else
        {
            StartAfterBeforeWhieBoard();
        
        }
     

    }

    public void StartConvBeforeWhieBoard()
    {
        

       

            if (SceneTransitionButton.gameIsPaused)
            {
                Debug.Log("game is paused and prevent conversation");
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return; // when the game is paused, prevent changing spite outline.
                }
            }
            //Debug.Log("Clicked");
            convSequenceB4++;
            //Debug.Log(convSequence);
            if (convSequenceB4 <= B4CompleteConversations.Length)
            {
                ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequenceB4 - 1]);

                //diable the first item, do not let it had more conversation.

                if (convSequenceB4 == B4CompleteConversations.Length)
                {
                    canTurnOffCollider = true;
                    Debug.Log(canTurnOffCollider + "canTurnOffColliderB4");
                   
                }

            }

        
    }

    public void StartAfterBeforeWhieBoard()
    {
        
            if (SceneTransitionButton.gameIsPaused)
            {
                Debug.Log("game is paused and prevent conversation");
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return; // when the game is paused, prevent changing spite outline.
                }
            }
            canTurnOffCollider = false;
            Debug.Log("Clicked");
            convSequenceAfter++;


            //Debug.Log(convSequence);
            if (convSequenceAfter <= AfterCompleteConversations.Length)
            {
                ConversationManager.Instance.StartConversation(AfterCompleteConversations[convSequenceAfter - 1]);
                if (convSequenceAfter == AfterCompleteConversations.Length)
                {
                    canTurnOffCollider = true;
                    Debug.Log(canTurnOffCollider + "canTurnOffColliderafter");

                }
            }

        
    }

    IEnumerator MovingTo(Vector3 pos, float speed)
    {


        yield return new WaitForSeconds(1.3f);
        while (transform.position != pos)
        {

            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime*60);
            
            if (Vector3.Distance(transform.position, pos) <= 0.01f)
            {
                transform.position = pos;
                break;
            }
            yield return null;
        
        }

    }

    public void MoveToNewPos()
    { 
        StartCoroutine(MovingTo(NewPosition, 0.2f));
    }

    public void MoveToNewPos2()
    {
        StartCoroutine(MovingTo(NewPosition2, 0.2f));
    }

    public void MoveToNewPos3()
    {
        StartCoroutine(MovingTo(NewPosition3, 0.2f));
    }

    IEnumerator TriggerConv()
    {
        yield return new WaitForSeconds(2.1f);
        OnMouseDown();

        //Input.GetMouseButtonDown(0);
    }


    }
