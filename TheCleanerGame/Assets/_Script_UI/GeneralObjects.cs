using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralObjects : MonoBehaviour
{
    
    [SerializeField] GameObject Animator;
    Animator m_Animator;


    [SerializeField] BoxCollider2D[] Colliders;

    //a temp fix for rn
    [SerializeField] GameObject[] Objects;
    private bool hasClicked;
    public bool OpenAtBegining;

    

    void Start()
    {
       
        
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].GetComponent<Collider2D>();
        }



        if (Animator != null)
        {
            m_Animator = Animator.GetComponent<Animator>();
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


        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", false);


            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i].enabled = true;
            }
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }

            hasClicked = false;

        }

    }

    public void OpenObjects()
    {
        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", true); // turn on stuff such as calendar or email

            //Disable other clickable objects

            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i].enabled = false;

            }
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(false);
            }

            hasClicked = true;
        }
    }

}
