using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueCalculation : MonoBehaviour
{
    public string HeadingText;

    public GameObject AIObejct;
    Text AISuggestionTex;

    public GameObject tryAgainPanel;
    Animation TryAginClip;
    public GameObject PassPanel;
    Animation PassPanelClip;


    ObjectDetection threeValues;

    // Use this for initialization
    void Start()
    {
        AISuggestionTex = AIObejct.GetComponent<Text>();
        tryAgainPanel.SetActive(false);
        PassPanel.SetActive(false);

        TryAginClip = tryAgainPanel.GetComponent<Animation>();
        PassPanelClip = PassPanel.GetComponent<Animation>();
        /*PlayerSatisValue = threeValues.SatisfactionValue;
        SafetyValue = threeValues.SafetyValue;
        MoneyValue = threeValues.MoneyValue */
        /* PlayerSatisValue = ObjectDetection.SatisfactionValue;
         SafetyValue = ObjectDetection.SafetyValue;
         MoneyValue = ObjectDetection.MoneyValue;*/
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Calculate()
    {
        float PlatformSum = ObjectDetection.PlatformV + ObjectDetection.ContentV + ObjectDetection.GenreV + ObjectDetection.ProductionV;
        AISuggestionTex.text = HeadingText + PlatformSum;

        if (PlatformSum >= 50)
        {
            //StartCoroutine(DelayPanel());
            PassPanel.SetActive(true);
            TryAginClip.Play();
            Debug.Log("pass");

        }
        else{
            //StartCoroutine(DelayPanel());
            tryAgainPanel.SetActive(true);
            PassPanelClip.Play();
            Debug.Log("fail");
            
        }
    }

    IEnumerator DelayPanel()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("wait");
    }
}
