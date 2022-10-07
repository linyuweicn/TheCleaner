using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagement : MonoBehaviour
{
    [Header("Canvas in Lab")]
    public GameObject[] DragDropCanvas;

    [Header("Canvas in Office")]
    public GameObject[] QuestionCanvas;

    [Header("Canvas in MyOffice")]
    public GameObject[] LaptopCanvas;

    public Doortransition doorScript;
    private bool canOpenCanvas;
    public static bool canOpenQuestionCanvas;
    

    void Start()
    {
        canOpenQuestionCanvas = false;
        
        for (int i = 0; i < DragDropCanvas.Length; i++)
        {
            DragDropCanvas[i].SetActive(false);
            canOpenCanvas = false;
        }
        for (int i = 0; i < QuestionCanvas.Length; i++)
        {
            QuestionCanvas[i].SetActive(false);
           
        } 
        
        for (int i = 0; i < LaptopCanvas.Length; i++)
        {
            LaptopCanvas[i].SetActive(false);
           
        }

       // Debug.Log(canOpenQuestionCanvas);

    }

    // Update is called once per frame
    void Update()
    {

        

      /*  if ( (doorScript.inLab && canOpenCanvas))  // when player is not in the office, for the firt level
        {
            for (int i = 0; i < DragDropCanvas.Length; i++)
            {
                DragDropCanvas[i].SetActive(true);
            }

            for (int i = 0; i < QuestionCanvas.Length; i++)
            {
                QuestionCanvas[i].SetActive(false);
            }
        }
        else if (!doorScript.inLab)
        {
            for (int i = 0; i < DragDropCanvas.Length; i++)
            {
                DragDropCanvas[i].SetActive(false);
                Debug.Log("else in inalb part");
            }

        }
        
        if (doorScript.inOffice && canOpenQuestionCanvas) // bool in QuizManager
        {

            for (int i = 0; i < QuestionCanvas.Length; i++)
            {
                QuestionCanvas[i].SetActive(true);
                Doortransition.CanClick = false; // cannot move to other places
               
            }
            
        }
        else
        {
            for (int i = 0; i < QuestionCanvas.Length; i++)
            {
                QuestionCanvas[i].SetActive(false);
            }
        }*/
        
        /*if (!doorScript.inOffice)
        {
            for (int i = 0; i < QuestionCanvas.Length; i++)
            {
                QuestionCanvas[i].SetActive(false);
            }
            Debug.Log("not in office " + canOpenQuestionCanvas);

        }*/




        /*if (doorScript.inMyOffice)
        {

            for (int i = 0; i < LaptopCanvas.Length; i++)
            {
                LaptopCanvas[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < LaptopCanvas.Length; i++)
            {
                LaptopCanvas[i].SetActive(false);
            }
        }*/



    }

   private IEnumerator oepnCanvas()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < DragDropCanvas.Length; i++)
        {
            DragDropCanvas[i].SetActive(true);
        }
        canOpenCanvas = true;
    }
    public void SetCanvasOpen() // in the dialogue script
    {
        StartCoroutine(oepnCanvas());
        
    }

    public void CanOpenQuestionPanel() // set in the dialogue editor

    {
        canOpenQuestionCanvas = true;
        Debug.Log(canOpenQuestionCanvas + "canOpenQuestionCanvas");
    }

    private IEnumerator DeLayDDCanvas() // delay drag and drop canvas
    {
        yield return new WaitForSeconds(1.75f);

        for (int i = 0; i < DragDropCanvas.Length; i++)
        {
            DragDropCanvas[i].SetActive(true);
        }

       
    }
    /*private IEnumerator DelayQuestionCanvas() // see canvas management script
    {
        yield return new WaitForSeconds(1.75f);
        for (int i = 0; i < QuestionCanvas.Length; i++)
        {
            QuestionCanvas[i].SetActive(true);
        }
    }*/
}
