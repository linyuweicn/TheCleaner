using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMeButton : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    private bool isPanelOpen;
    private bool isClicked;

    void Start()
    {
        isPanelOpen = false;
        isClicked = false;
    }

    // Update is called once per frame
    public void ClickButton()
    {
        if (!isPanelOpen)
        {
            Panel.SetActive(true);
            isPanelOpen = true;

        }
        else 
        {
            Panel.SetActive(false);
            isPanelOpen = false;

        }
    }
}
