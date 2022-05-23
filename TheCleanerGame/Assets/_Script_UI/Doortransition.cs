    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doortransition : MonoBehaviour
{

    public GameObject Labcamera;
    public GameObject ComputerCamera;
    public bool isDoor1clicked;
    private bool isDoor2clicked;

    public static bool CanClick = true;

    public bool inOffice = false; // sued for CanvasManager and controls the drag and drop canvas
    public bool inLab = false; // sued for CanvasManager and controls the drag and drop canvas
    public bool inMyOffice = false; // sued for CanvasManager and controls the drag and drop canvas

    
    
    
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

        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        inLab = true;
    }

    private void OnMouseDown()
    {
        //when the quiz psanel is up, player cannot move - see quiz manager and canvas management

        if ((gameObject.name == "Lab-office door") && CanClick)// click the Lab-office door
        {
           
            if (!isDoor1clicked ) // move to office
            {
                Labcamera.SetActive(false);
                isDoor1clicked = true;
                
                inLab = false;
                StartCoroutine(DelayQuestionCanvas());
                //inoffice = true
                
            }
            else // move to lab
            {
                Labcamera.SetActive(true);

                isDoor1clicked = false;

                inOffice = false;
                StartCoroutine(DeLayDDCanvas());
                

                Debug.Log(inLab + "inLab");
            }
        }else if ((gameObject.name == "DoorToMyOffice") && CanClick) // DoorToMyOfficer
        {
            if (!isDoor2clicked)
            {
                ComputerCamera.SetActive(true); // move to personal offcice
                isDoor2clicked = true;

                inOffice = false;
                CanvasManagement.canOpenQuestionCanvas = false;
                StartCoroutine(DelayMyOfficeCanvas());
                //Debug.Log(inOffice + " inOffice");
            }
            else // move to offcice
            {
                ComputerCamera.SetActive(false);
                isDoor2clicked = false;

                inMyOffice = false;
                StartCoroutine(DelayQuestionCanvas());

            }
            
            
        }        
       
       
    }

    public void SetCanClick() // set at the end of intro converstaion, see the dialogue panels
    {
        CanClick = true;
    }


    private IEnumerator DeLayDDCanvas() // delay drag and drop canvas
    {
        yield return new WaitForSeconds(1.75f);
        Debug.Log("wait");
        inLab = true;
    }

    private IEnumerator DelayQuestionCanvas() // see canvas management script
    {
        yield return new WaitForSeconds(1.75f);
        //CanvasManagement.canOpenQuestionCanvas = true;
        inOffice = true;
    }
    private IEnumerator DelayMyOfficeCanvas() // see canvas management script
    {
        yield return new WaitForSeconds(1.75f);
        inMyOffice = true;
        Debug.Log(inMyOffice + "inMyOffice");
    }


}
