using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems;
using System;


public class NPCConvWithWhiteBoard : MonoBehaviour
{
    
    [System.Serializable]
    public class MultiDimensionalConvo
    {
        [HideInInspector]
        public string ConvType;
        public NPCConversation[] convoArray;
    }
    [Header("Conversations!")]
    public NPCConversation[] B4CompleteConversations;
    public NPCConversation[] AfterCompleteConversations;


    public MultiDimensionalConvo[] B4Conversations;
    public MultiDimensionalConvo[] AfterConversations;

    [SerializeField] int lowMedBound = 0;
    [SerializeField] int medHighBound = 5;
    [SerializeField] bool useAgreeableScore;
    [SerializeField] bool startConvAtBegining;
    private int convSequenceB4;
    private int convSequenceAfter;

    [Header("Colliders!")]
    [SerializeField] BoxCollider2D[] boxCollider2D; // for items, do not put characters in here because the Conversation plugin disables clicking other characters already
    private BoxCollider2D CharaCollider;

    [Header("Positions!")]
    Vector3 origPosition;
    [SerializeField] Vector3 NewPosition;
    [SerializeField] Vector3 NewPosition2;
    [SerializeField] Vector3 NewPosition3;
    public bool FlipWhenMoving;
    private SpriteRenderer CharaSpriteRenderer;
  

    [HideInInspector]
    public bool canTurnOffCollider; //GeneralObjects and TriggerConvAfterPrompts uses this 
    [HideInInspector]
    public bool hasFinishedConv; // Used in triggerending cut scene

    private AudioManager audioManager;
    //public AudioClip audioClip;


    void Start()
    {

        //public MultiDimensionalConvo[] B4Conversations = new MultiDimensionalConvo[3];
        //public MultiDimensionalConvo[] AfterConversations = new MultiDimensionalConvo[3];
        CharaSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        CharaCollider = gameObject.GetComponent<BoxCollider2D>();
        //Debug.Log(CharaCollider);
        origPosition = transform.position;
        if (startConvAtBegining)
        {
            StartCoroutine(TriggerConv());
           // Debug.Log("start 1 conv");
        }
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        //Logic  for turning off colliders:
        /*The Converstaion Manager  will turn off other character's colliders when the target is speaking.
         * After finishing all conv b4 brainstorm, turn off that character's collider.
         * Do 3 brainstorms --> when closing the panel, turn the character colliders on.
         * (You can find the function in TiggerConAfterPrompts)
         * Then do the after brainstorm colliders.
         * */
        if (canTurnOffCollider)
        {

            CharaCollider.enabled = false;
            //Debug.Log(canTurnOffCollider + " in update as canTurnOffCollider");

        }
        else
        {
            
            if (ConversationManager.Instance.IsConversationActive)
            {
                DisableObjects();
            }
            else
            {
                EnableObjects(); 
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
        if (useAgreeableScore)
        {
            Debug.Log(convSequenceB4 + "convSequenceB4");
            Debug.Log(B4Conversations[0].convoArray.Length + "B4Conversations[0].convoArray.Length");
            if (convSequenceB4 <= B4Conversations[0].convoArray.Length)
            {
                if (ConvoGlobalManager.agreeableScore <= lowMedBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[0].convoArray[convSequenceB4 - 1]);
                else if (ConvoGlobalManager.agreeableScore > lowMedBound && ConvoGlobalManager.agreeableScore <= medHighBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[1].convoArray[convSequenceB4 - 1]);
                else if (ConvoGlobalManager.agreeableScore > medHighBound)
                    ConversationManager.Instance.StartConversation(B4Conversations[2].convoArray[convSequenceB4 - 1]);
                

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
                    Debug.Log(canTurnOffCollider + "canTurnOffCollider" + gameObject.ToString());

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

        //Debug.Log("Clicked");
        convSequenceAfter++;
        Debug.Log(convSequenceAfter + " convSequenceAfter");
        CharaCollider.enabled = true;
        if (useAgreeableScore)
        {
            Debug.Log(AfterConversations[0].convoArray.Length + " AfterConversations[0].convoArray.Length");
            //Debug.Log(convSequence);
            if (convSequenceAfter <= AfterConversations[0].convoArray.Length)
            {

                if (ConvoGlobalManager.agreeableScore <= lowMedBound)
                    ConversationManager.Instance.StartConversation(AfterConversations[0].convoArray[convSequenceAfter - 1]);
                else if (ConvoGlobalManager.agreeableScore > lowMedBound && ConvoGlobalManager.agreeableScore <= medHighBound)
                    ConversationManager.Instance.StartConversation(AfterConversations[1].convoArray[convSequenceAfter - 1]);
                else if (ConvoGlobalManager.agreeableScore > medHighBound)
                    ConversationManager.Instance.StartConversation(AfterConversations[2].convoArray[convSequenceAfter - 1]);
                
                if (convSequenceAfter == AfterConversations[0].convoArray.Length)
                {
                    canTurnOffCollider = true;
                    Debug.Log(canTurnOffCollider + "canTurnOffColliderafter");

                }
            }
        }
        else
        {
            //Debug.Log(convSequence);
            if (convSequenceAfter <= AfterCompleteConversations.Length)
            {

               
                ConversationManager.Instance.StartConversation(AfterCompleteConversations[convSequenceAfter - 1]);
                if (convSequenceAfter == AfterCompleteConversations.Length)
                {
                    canTurnOffCollider = true;
                    Debug.Log(canTurnOffCollider + "canTurnOffColliderafter in " + gameObject.ToString());

                }//when conversation is not ctive enable object? 
            }
        }


    }

    IEnumerator MovingTo(Vector3 pos, float speed)
    {

        yield return new WaitForSeconds(1.5f);
        if (FlipWhenMoving)
            CharaSpriteRenderer.flipX = !CharaSpriteRenderer.flipX;
      

        while (transform.position != pos)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime * 60);

            if (Vector3.Distance(transform.position, pos) <= 0.01f)
            {
                transform.position = pos;
                if (FlipWhenMoving)
                    CharaSpriteRenderer.flipX = !CharaSpriteRenderer.flipX;
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

        CharaCollider.enabled = false;


    }

    public void EnableObjects() // enable objects
    {

        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            boxCollider2D[i].enabled = true;
            //Debug.Log(boxCollider2D[i].name + "enabled  in " + gameObject.ToString());
        }


        if (canTurnOffCollider)
        {
            CharaCollider.enabled = false;
            Debug.Log("in enable() " + canTurnOffCollider + "canTurnOffCollider");
        }
        else
        {
            CharaCollider.enabled = true;


        }
    }

    public void SwapExpression(Sprite s)
    {
        CharaSpriteRenderer.sprite = s;
    }

    public void FlipWHenMoving()
    {
        CharaSpriteRenderer.flipX = !CharaSpriteRenderer.flipX;
    }

    public void SpawnParticle(GameObject ParticlePrefab)
    {
        Vector2 spawnPosition = transform.Find("ParticleLocation").transform.position;
        Debug.Log(spawnPosition);
        Instantiate(ParticlePrefab, spawnPosition, Quaternion.identity);
    }
}
        


    
