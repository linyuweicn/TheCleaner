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
    
    public bool hasFinishedConv; // Used in triggerending cut scene

    AudioManager audioManager;


    void Start()
    {

        origPosition = transform.position;
        if (startConvAtBegining)
        {
            StartCoroutine(TriggerConv());
            Debug.Log("start 1 conv");
        }
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (canTurnOffCollider)
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
         }*/



        //below is the method that does not need to trigger using Converstain Manger
        if (canTurnOffCollider)
        {
            for (int j = 0; j < CharaCollider.Length; j++)
            {
                CharaCollider[j].enabled = false;
            }

        }
        else
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                DisableObjects();
            }
            else
            {
                EnableObjects(); //-- the GneralObjects need to find abother way to turn it off
            }
        }


    }

    public void OnMouseDown()
    {
        audioManager.PlayUiSound("ui_highlight");
        if (BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed
            && BrainstormGeneralManager.Instance.ContainerDictionary[1].Prompt.completed
            && BrainstormGeneralManager.Instance.ContainerDictionary[2].Prompt.completed)
        {
            StartAfterWhieBoard();



        }
        else
        {
            StartConvBeforeWhieBoard();
            //Debug.Log("not completed");
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

        convSequenceB4++;
        //Debug.Log(convSequence);
        if (convSequenceB4 <= B4CompleteConversations.Length)
        {
            //DisableObjects();
            ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequenceB4 - 1]);

            

            if (convSequenceB4 == B4CompleteConversations.Length)
            {
                canTurnOffCollider = true;
                hasFinishedConv = true;
                //Debug.Log(hasFinishedConv + "hasFinishedConv");
              
            }

        }

    }

    public void StartAfterWhieBoard()
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
                //DisableObjects();
                ConversationManager.Instance.StartConversation(AfterCompleteConversations[convSequenceAfter - 1]);
                if (convSequenceAfter == AfterCompleteConversations.Length)
                {
                    canTurnOffCollider = true;
                    Debug.Log(canTurnOffCollider + "canTurnOffColliderafter");
                    
                 }//when conversation is not ctive enable object? 
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

    public void DisableObjects()
    {
        
       for (int i = 0; i < boxCollider2D.Length; i++)
       {
            boxCollider2D[i].enabled = false;
            //Debug.Log(boxCollider2D[i].name + "disabled");
        }

        for (int j = 0; j < CharaCollider.Length; j++)
        {
            CharaCollider[j].enabled = false;
        }


    }

    public void EnableObjects() // enable objects
    {
      
     for (int i = 0; i < boxCollider2D.Length; i++)
      {
        boxCollider2D[i].enabled = true;
         //Debug.Log(boxCollider2D[i].name + "enabled");
       }
        
        for (int j = 0; j < CharaCollider.Length; j++)
        {
            CharaCollider[j].enabled = true;
        }

    }
}
        


    
