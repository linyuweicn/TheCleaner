using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralObjects : MonoBehaviour
{
    
    [SerializeField] GameObject AnimatorOrObject;
    Animator m_Animator;


    [SerializeField] BoxCollider2D[] Colliders;

    //a temp fix for rn
    //[SerializeField] GameObject[] Objects;
    private bool hasClicked;
    public bool OpenAtBegining;

    

    void Start()
    {
       
     /*   
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].GetComponent<Collider2D>();
        }*/



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
       

        if (hasClicked == false)
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

        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", false);

        }
        else
        {
            AnimatorOrObject.SetActive(false);
        }
        
    
        /*for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(true);
        }*/

        
    }

    public void OpenObjects()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = false;

        }

        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", true); // turn on stuff such as calendar or email

           
        }
        else
        {
            AnimatorOrObject.SetActive(true);
        }
        //Disable other clickable objects

      
        /*for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(false);
        }*/

        hasClicked = true;
    }


}
