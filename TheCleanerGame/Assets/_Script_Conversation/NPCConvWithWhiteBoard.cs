using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems;


public class NPCConvWithWhiteBoard : MonoBehaviour
{
    [System.Serializable]
    public class MultiDimensionalConvo
    {
        [HideInInspector]
        public string ConvType;
        public NPCConversation[] convoArray;
    }

    public NPCConversation[] B4CompleteConversations;
    public NPCConversation[] AfterCompleteConversations;

    
    public MultiDimensionalConvo[] B4Conversations;
    public MultiDimensionalConvo[] AfterConversations;

    [SerializeField] int lowMedBound = 0;
    [SerializeField] int medHighBound = 5;
    [SerializeField] bool useAgreeableScore;

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
    //public AudioClip audioClip;


    void Start()
    {

        //public MultiDimensionalConvo[] B4Conversations = new MultiDimensionalConvo[3];
        //public MultiDimensionalConvo[] AfterConversations = new MultiDimensionalConvo[3];

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
        //audioManager.PlaySFX(audioClip);
        audioManager.PlayUiSound("ui_highlight");
        //Debug.Log(audioManager);
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

        //Debug.Log(convSequenceB4);
        if(useAgreeableScore)
        {
                Debug.Log(convSequenceB4 + "convSequenceB4");
            Debug.Log(B4Conversations[0].convoArray.Length + "B4Conversations[0].convoArray.Length");
            if (convSequenceB4 <= B4Conversations[0].convoArray.Length)
            {
                if(ConvoGlobalManager.agreeableScore <= lowMedBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[0].convoArray[convSequenceB4 - 1]);
                else if(ConvoGlobalManager.agreeableScore > lowMedBound && ConvoGlobalManager.agreeableScore <= medHighBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[1].convoArray[convSequenceB4 - 2]);
                else if(ConvoGlobalManager.agreeableScore > medHighBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[2].convoArray[convSequenceB4 - 2]);
                //DisableObjects();
                //ConversationManager.Instance.StartConversation(B4CompleteConversations[convSequenceB4 - 1]);

                if (convSequenceB4 == B4Conversations[0].convoArray.Length)
                {
                    canTurnOffCollider = true;
                    hasFinishedConv = true;
                    //Debug.Log(hasFinishedConv + "hasFinishedConv");
                
                }

            }
        }
        else
        {
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
            //Debug.Log("Clicked");
            convSequenceAfter++;
            Debug.Log(convSequenceAfter + " convSequenceAfter");
            
        if (useAgreeableScore)
            {
            Debug.Log(AfterConversations[0].convoArray.Length + " AfterConversations[0].convoArray.Length");
            //Debug.Log(convSequence);
            if (convSequenceAfter <= AfterConversations[0].convoArray.Length)
                {
                    if(ConvoGlobalManager.agreeableScore <= lowMedBound)
                        ConversationManager.Instance.StartConversation(AfterConversations[0].convoArray[convSequenceAfter - 1]);
                    else if(ConvoGlobalManager.agreeableScore > lowMedBound && ConvoGlobalManager.agreeableScore <= medHighBound)
                        ConversationManager.Instance.StartConversation(AfterConversations[1].convoArray[convSequenceAfter - 2]);
                    else if(ConvoGlobalManager.agreeableScore > medHighBound)
                        ConversationManager.Instance.StartConversation(AfterConversations[2].convoArray[convSequenceAfter - 2]);
                    //DisableObjects();
                    //ConversationManager.Instance.StartConversation(AfterCompleteConversations[convSequenceAfter - 1]);
                    if (convSequenceAfter == AfterConversations[0].convoArray.Length)
                    {
                        canTurnOffCollider = true;
                        Debug.Log(canTurnOffCollider + "canTurnOffColliderafter");
                        
                    }//when conversation is not ctive enable object? 
                }
            }
            else
            {
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
            if (CharaCollider != null)
            {
                CharaCollider[j].enabled = false;
            }
            
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
            if (CharaCollider != null)
                CharaCollider[j].enabled = true;
        }

    }
}
        


    
