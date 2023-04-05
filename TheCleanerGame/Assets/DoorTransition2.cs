using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransition2 : MonoBehaviour
{
    [SerializeField] GameObject ActiveCamera;
    [SerializeField] GameObject DeActiveCamera;

    [SerializeField] GameObject ExitDoor;
    [SerializeField] GameObject CalendarPage;

    private AudioManager AudioManager;
    void Start()
    {
        AudioManager = FindObjectOfType<AudioManager>();
        
        CalendarPage.SetActive(false);
        ExitDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed
           && BrainstormGeneralManager.Instance.ContainerDictionary[1].Prompt.completed
           && BrainstormGeneralManager.Instance.ContainerDictionary[2].Prompt.completed
           )
        {
            ExitDoor.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        AudioManager.PlayUiSound("ui_DoorClose02");
        if (ActiveCamera != null && DeActiveCamera!= null)
        {
            ActiveCamera.SetActive(true);
            DeActiveCamera.SetActive(false);
        }
        

        if (gameObject.name == "ExitDoor") // transit to personal room = end the day 
        {
            CalendarPage.SetActive(true);
            AudioManager.PlayUiSound("ui_DoorClose02");
        }
    }
}
