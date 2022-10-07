using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doortransition : MonoBehaviour
{

    public GameObject EntranceCamera;
    public GameObject BrainstormCamera;
    public GameObject YiranOfficeCamera;
    public GameObject PersonalRoomCamera;
    public static bool isDoor1clicked;
    public static bool isDoor2clicked;

    [SerializeField] GameObject CalendarPage;

    public static bool CanClick = true;

  
    
    
    public void Update()
    {
       
    }

    public void Start()
    {
       if (SceneManager.GetActiveScene().buildIndex == 1)
        {
           CanClick = true;
            //Debug.Log(CanClick);
        }

        CalendarPage.SetActive(false);

    }

    private void OnMouseDown()
    {

        if ((gameObject.name == "LabDoor") && CanClick)// click the Lab door
        {

            if (!isDoor1clicked) // move to office
            {
                BrainstormCamera.SetActive(true);
                isDoor1clicked = true;

            }
            else // move to entrance
            {
                BrainstormCamera.SetActive(false);

                isDoor1clicked = false;

            }
        }
        else if ((gameObject.name == "OffficeDoor") && CanClick) // DoorToMyOfficer
        {
            if (!isDoor2clicked)
            {
                YiranOfficeCamera.SetActive(true); // move to Yiran offcice
                isDoor2clicked = true;
            }
            else // move to brainstorm room
            {
                YiranOfficeCamera.SetActive(false);
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
