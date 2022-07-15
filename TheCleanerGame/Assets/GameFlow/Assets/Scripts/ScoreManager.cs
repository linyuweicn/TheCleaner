using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI satBox;
    [SerializeField] TextMeshProUGUI agreeBox;
    [SerializeField] TextMeshProUGUI innoBox;
    [SerializeField] TextMeshProUGUI prodBox;

    float satisfaction;
    float innovation;
    float production;
    float censorFulfillment;
    void Start()
    {
        satisfaction = 0;
        innovation = 0;
        production = 0;
        censorFulfillment = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(Answer ans)
    {
        satisfaction += ans.satisfaction;
        innovation += ans.innovation;
        production += ans.production;
        censorFulfillment += ans.censorFulfillment;

        CalculateAgreement();
        Overwrite();
    }

    public void TakePoints(Answer ans)
    {
        satisfaction -= ans.satisfaction;
        innovation -= ans.innovation;
        production -= ans.production;
        censorFulfillment -= ans.censorFulfillment;

        CalculateAgreement();
        Overwrite();
    }

    void Overwrite()
    {
        satBox.text = satisfaction.ToString("F2");
        innoBox.text = innovation.ToString("F2");
        prodBox.text = production.ToString("F2");
    }

    void CalculateAgreement()
    {
        float agreement = (0.5f * censorFulfillment) + (0.3f * innovation) + (0.1f * satisfaction) - (0.1f * production);
        agreeBox.text = agreement.ToString("F2");
    }
}
