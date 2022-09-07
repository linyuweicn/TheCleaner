using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhtieBoard : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject BrainStormCanvas;
    [SerializeField] GameObject WhiteBoard;
    void Start()
    {
      
    }

    private void OnEnable()
    {
        isOn = false;
        WhiteBoard.SetActive(true);
        BrainStormCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (isOn) //if barinsotrm canvas is on
        {
            // make this off
            isOn = false;
            WhiteBoard.SetActive(true);
            BrainStormCanvas.SetActive(false);
        }
        else //if barinsotrm canvas is off
        {
            // make this on
            isOn = true;
            WhiteBoard.SetActive(false);
            BrainStormCanvas.SetActive(true);
        }
    }
}
