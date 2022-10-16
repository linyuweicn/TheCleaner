using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class YiranConversationScene2 : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    AudioManager audioManager;
    public NPCConversation[] B4CompleteConversations;
    public NPCConversation[] AfterCompleteConversations;
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

        audioManager = FindObjectOfType<AudioManager>();
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
        if (!BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed)
        {
            //Debug.Log("not completed");
            if (Input.GetMouseButtonDown(0))
            {

                convSequence++;
                audioManager.PlaySFX(audioClip);

                if (B4CompleteConversations.Length > 1) // when there are more than 1 conversation
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

                            case 4:
                                ConversationManager.Instance.StartConversation(B4CompleteConversations[3]);
                                Debug.Log("C4");
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
        else
        {
            //Debug.Log(" completed");
            if (Input.GetMouseButtonDown(0))
            {

                convSequence++;

                if (AfterCompleteConversations.Length > 1) // when there are more than 1 conversation
                {

                    if (convSequence == 1)
                    {
                        ConversationManager.Instance.StartConversation(AfterCompleteConversations[0]);
                        Debug.Log("AC1");
                    }
                    else
                    {
                        convSequence = Random.Range(2, AfterCompleteConversations.Length);


                        switch (convSequence)
                        {
                            case 2:
                                ConversationManager.Instance.StartConversation(AfterCompleteConversations[1]);
                                Debug.Log("AC2");
                                break;

                            case 3:
                                ConversationManager.Instance.StartConversation(AfterCompleteConversations[2]);
                                Debug.Log("AC3");
                                break;

                            case 4:
                                ConversationManager.Instance.StartConversation(AfterCompleteConversations[3]);
                                Debug.Log("AC4");
                                break;

                        }
                    }


                } else
                {
                    ConversationManager.Instance.StartConversation(AfterCompleteConversations[0]);
                    Debug.Log("AC1");

                }

            }




        }


    }
}
