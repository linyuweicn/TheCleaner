using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerEndingCutScene : MonoBehaviour
{
    public GameObject EndingCamera;
    public GameObject EndingCredits;
    public NPCConvWithWhiteBoard ConvScipt;
    public endingdetections detection;

    public void Start()
    {
        
    }

    public void TriggerCredits()
    {
       if(detection!= null && detection.YiranComplete && detection.CharlieComplete && detection.LucaCopmlete)
        {
            StartCoroutine(WaitToTrigger());
        }
        else
        {
            Debug.Log("wrong ending");

        }

    }

    IEnumerator WaitToTrigger( )
    {
        if (ConvScipt.hasFinishedConv)
        {
            Debug.Log("finished conv");
            yield return new WaitForSeconds(2.5f);
            EndingCamera.SetActive(true);
            EndingCredits.SetActive(true);
        }

     }

    public void TriggerImeediately()
    {
        EndingCamera.SetActive(true);
        EndingCredits.SetActive(true);
    }
}
