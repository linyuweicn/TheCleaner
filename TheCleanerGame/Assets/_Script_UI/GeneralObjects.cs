using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralObjects : MonoBehaviour
{

    [SerializeField] GameObject AnimatorOrObject;
    Animator m_Animator;
    AudioManager audioManager;

    [SerializeField] BoxCollider2D[] Colliders;

    //a temp fix for rn, becuase NPC conversations set some object's collider to be true in teh Update function
    [SerializeField] GameObject[] Objects;
    private bool hasClicked;
    public bool OpenAtBegining;



    void Start()
    {

        /*   
           for (int i = 0; i < Colliders.Length; i++)
           {
               Colliders[i].GetComponent<Collider2D>();
           }*/
        audioManager = FindObjectOfType<AudioManager>();


        if (AnimatorOrObject != null)
        {
            m_Animator = AnimatorOrObject.GetComponent<Animator>();
            //for aniamted objects, enable them at the begining
            if (OpenAtBegining)
            {
                
                OnMouseDown();
                Debug.Log("ShouldClick");
            }
            
        }

        

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnMouseDown() // enable animation
    {

        if (SceneTransitionButton.gameIsPaused)
        {
            Debug.Log("game is paused and prevent clicking");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // when the game is paused, prevent clicking objects
            }
        }

        if (OpenAtBegining)
        {
            
            OpenAtBegining = false;
            audioManager.PlayUiSound("");//sounds only
        }
        else
        {
            audioManager.PlayUiSound("ui_confirm");
        }
       


        if (!hasClicked)
        {

            OpenObjects();
            
        }
        else
        {
            CloseObject();
        }




    }


    public void CloseObject()
    {
        hasClicked = false;
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = true;
           
        }

      
        if (Objects != null)
        {
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }
        }

        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", false);
            

        }
        else
        {
            AnimatorOrObject.SetActive(false);

        }


    }

    public void OpenObjects()
    {
        
        hasClicked = true;
        if (m_Animator != null)
        {
            
            m_Animator.SetBool("isOn", true); // turn on stuff such as calendar or email

        }
        else
        {
            AnimatorOrObject.SetActive(true);
        }


        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = false;
            
        } 
            

            //Disable other clickable objects

        if (Objects != null)
        {
             for (int j = 0; j < Objects.Length; j++)
                {
                    Objects[j].SetActive(false);
                }
        }
   
    }

    IEnumerator CloseAnimationObjeccts()
    {
        yield return new WaitForSeconds(1.3f);

        AnimatorOrObject.SetActive(false);
        //ConversationManager.Instance.StartConversation(EndingComments);
    }
}
