using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerEndingCutScene : MonoBehaviour
{
    public GameObject EndingCamera;
    public GameObject EndingCredits;
    public NPCConvWithWhiteBoard ConvScipt;

    public void Start()
    {
        
    }

    public void TriggerCredits()
    {
        StartCoroutine(WaitToTrigger());
    }

    IEnumerator WaitToTrigger( )
    {
        if (ConvScipt.hasFinishedConv)
        {
            Debug.Log("finished conv");
            yield return new WaitForSeconds(2f);
            EndingCamera.SetActive(true);
            EndingCredits.SetActive(true);
        }

     }
}
