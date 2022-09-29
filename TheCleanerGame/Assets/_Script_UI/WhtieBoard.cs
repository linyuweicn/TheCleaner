using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhtieBoard : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject BrainStormCanvas;
    [SerializeField] GameObject WhiteBoard;

    public GameObject[] EnabledObjects;
    public GameObject[] DisabledObjects;


    void Start()
    {
      
    }

    private void OnEnable()
    {
        isOn = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (isOn) //brainstorm canvas is showing
        {

            WhiteBoard.SetActive(true);
            BrainStormCanvas.SetActive(false);
            // make this off
            isOn = false;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(true);
            }

        }
        else //if barinsotrm canvas is off
        {
            WhiteBoard.SetActive(false);
            BrainStormCanvas.SetActive(true);
            // make this on
            isOn = true;

            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].SetActive(false);
            }
            

        }
                
    }

  
}
