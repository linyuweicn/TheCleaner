using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreProgress : MonoBehaviour
{
    private Slider slider;
    private float targetProgress =0f;
    [SerializeField] float fillSpeed;


    // inside of your update or animation method
    

    FeedbackManager feedbackManager;
    

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        feedbackManager = FindObjectOfType<FeedbackManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (slider.value < targetProgress)
        {
            
            slider.value += fillSpeed * Time.deltaTime;
        }
        else if (slider.value > targetProgress + fillSpeed * Time.deltaTime)
        {
            slider.value -= fillSpeed * Time.deltaTime;
        }
        else
        {
            slider.value = targetProgress;
        }

    }

    public void IncrementProgress(float newProgress)
    {
        //newProgress = answer.totalscore;
        /*if (newProgress >= targetProgress)
        {
            targetProgress = slider.value + newProgress;
        }
        else
        {
            targetProgress = newProgress;
        }*/

        targetProgress = newProgress;

        Debug.Log("new progress is "+ newProgress);
    }
}
