using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObjects : MonoBehaviour
{
    
    [SerializeField] GameObject Animator;
    Animator m_Animator;

    [SerializeField] Collider2D[] Colliders;
    [SerializeField] Collider2D[] CharaColliders;

    private bool hasClicked;
    //to coontrol character's collider 
    

    void Start()
    {
        
        
        if (Animator != null)
        {
            m_Animator = Animator.GetComponent<Animator>();
            //for aniamted objects, enable them at the begining
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
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

           /* if (!NPC1_Con1.canTurnOffCollider )
            {
                for (int i = 0; i < CharaColliders.Length; i++)
                {
                    CharaColliders[i].enabled = true;
                }

            }
            else
            {
                for (int i = 0; i < CharaColliders.Length; i++)
                {
                    CharaColliders[i].enabled = false;
                }
            }*/
            hasClicked = false;
            
        }
       

    

    }

    public void OnMouseDown() // enable animation
    {

        if (hasClicked == false)
        {
            if (m_Animator != null)
            {
                m_Animator.SetBool("isOn", true); // turn on stuff such as calendar or email

                //Disable other clickable objects

                for (int i = 0; i < Colliders.Length; i++)
                {
                    Colliders[i].enabled = false;
                    Debug.Log("clicked on iem");
                    Debug.Log(Colliders[0].enabled);
                }

                hasClicked = true;
            }
          
        }
        else
        {
            CloseObject();
        }
        
    
    }
}
