using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhtieBoard : MonoBehaviour
{
    bool isBrainStormOn = false;
    [SerializeField] GameObject BrainStormCanvas;
    [SerializeField] GameObject WhiteBoard;

    public GameObject[] EnabledObjects;
    public GameObject[] DisabledObjects;

    public GameObject Animator;
    Animator m_Animator;
    

    void Start()
    {
        m_Animator = Animator.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isBrainStormOn = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (isBrainStormOn) //brainstorm canvas is showing
        {

            WhiteBoard.SetActive(true);
            m_Animator.SetBool("isOn", false);
            // make this off
            isBrainStormOn = false;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(true);
            }

        }
        else //if barinsotrm canvas is off
        {
            WhiteBoard.SetActive(false);
            m_Animator.SetBool("isOn", true); // show brainstorm canvas
            // make this on
            isBrainStormOn = true;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(false); // disable all the obajects
            }


        }

    }

  
}
