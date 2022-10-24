using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObjects : MonoBehaviour
{
    [SerializeField] GameObject enableObjects;
    [SerializeField] BoxCollider2D [] boxCollider2D;

    private bool isCalaendarClicked;

    [SerializeField] GameObject Animator;
    Animator m_Animator;

    void Start()
    {
        
        
        if (Animator != null)
        {
            m_Animator = Animator.GetComponent<Animator>();
            //for aniamted objects, enable them at the begining
        }
        else if (enableObjects != null)
        {
            enableObjects.SetActive(false);
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

        }
        else
        {
            enableObjects.SetActive(false); // open objects that does not require aniamtions
        }
        

        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            boxCollider2D[i].enabled = true;
        }

        isCalaendarClicked = false;

    }

    public void OnMouseDown()
    {
        if (m_Animator != null)
        {
            m_Animator.SetBool("isOn", true);
            
        }
        else
        {
            enableObjects.SetActive(true);
        }


        //Disable other clickable objects
        for (int i = 0; i< boxCollider2D.Length; i++)
        {
            boxCollider2D[i].enabled = false;
        }

    }
}
