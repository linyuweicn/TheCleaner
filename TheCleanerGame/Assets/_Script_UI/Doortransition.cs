using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Doortransition : MonoBehaviour
{

    public GameObject EntranceCamera;
    public GameObject BrainstormCamera;
    public GameObject YiranOfficeCamera;
    public GameObject PersonalRoomCamera;
    public GameObject ExitDoor;
    public static bool isDoor1clicked;
    public static bool isDoor2clicked;

    [SerializeField] GameObject CalendarPage;

    public static bool CanClick = true;

   
  
    
    
    public void Update()
    {
        if (BrainstormGeneralManager.Instance.ContainerDictionary[0].Prompt.completed
            && BrainstormGeneralManager.Instance.ContainerDictionary[1].Prompt.completed
            && BrainstormGeneralManager.Instance.ContainerDictionary[2].Prompt.completed
            )
        {
            ExitDoor.SetActive(true);
        }

    }

    public void Start()
    {
        /*       if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                   CanClick = true;
                    //Debug.Log(CanClick);
                }*/

        CanClick = true;
        CalendarPage.SetActive(false);
        ExitDoor.SetActive(false);
    }

    private void OnMouseDown()
    {
        //bug note: if the objects does not react to clicking, reassign the caermas in the scene to the scripts.
        if (SceneTransitionButton.gameIsPaused)
        {
            Debug.Log("game is paused and prevent door transition");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // when the game is paused, prevent changing spite outline.
            }
        }
        if ((gameObject.name == "LabDoor") && CanClick)// click the Lab door
        {

            if (!isDoor1clicked) // move to move to entrance
            {
                EntranceCamera.SetActive(true);
                isDoor1clicked = true;

            }
            else // move to offuce
            {
                EntranceCamera.SetActive(false);

                isDoor1clicked = false;

            }
        }
        else if ((gameObject.name == "OfficeDoor") && CanClick) // DoorToMyOfficer
        {
            if (!isDoor2clicked)
            {
                BrainstormCamera.SetActive(true); // move to brainstorm room
                isDoor2clicked = true;
                Debug.Log("Clicked");
            }
            else // move to Yiran room
            {
                BrainstormCamera.SetActive(false);
                isDoor2clicked = false;

            }
        }
        else if ((gameObject.name == "ExitDoor") && CanClick) // transit to personal room = end the day 
        {
            CalendarPage.SetActive(true);

        }


    }

    public void SetCanClick() // set at the end of intro converstaion, see the dialogue panels
    {
        CanClick = true;
    }

    public void CannotClick() // set at the end of intro converstaion, see the dialogue panels
    {
        CanClick = false;
    }

    public void SetCameraActive()
    {
        PersonalRoomCamera.SetActive(true);
    }

}
