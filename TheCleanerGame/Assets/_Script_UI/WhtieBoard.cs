using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhtieBoard : MonoBehaviour
{
    bool isBrainStormOn = false;
    public GameObject Animator;
    //[SerializeField] GameObject WhiteBoard;
    [SerializeField] BoxCollider2D WhiteBoardCollider;

    public GameObject[] EnabledObjects;
    public GameObject[] DisabledObjects;

    
    Animator m_Animator;
    AudioManager audioManager;

    void Start()
    {
        m_Animator = Animator.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
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
        if (SceneTransitionButton.gameIsPaused)
        {
            //Debug.Log("game is paused and prevent changing color");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // when the game is paused, prevent changing spite outline.
            }
        }
        if (!isBrainStormOn) //brainstorm canvas is showing
        {

   
            //WhiteBoard.SetActive(false);
            WhiteBoardCollider.enabled = false;
            m_Animator.SetBool("isOn", true); // show brainstorm canvas
            // make this on
            isBrainStormOn = true;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(false); // disable all the obajects
            }

            audioManager.PlayUiSound("ui_confirm");

        }
       
    }

    public void CloseBrainstorm()
    {
        if (isBrainStormOn)
        {
            //WhiteBoard.SetActive(true);
            WhiteBoardCollider.enabled = true;
            m_Animator.SetBool("isOn", false);
            // make this off
            isBrainStormOn = false;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(true);
            }
        }

    }

}
