using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsContent : MonoBehaviour
{
  
    private TextMeshProUGUI Content;
    [SerializeField] string LowSscoreText;
    [SerializeField] string MedSscoreText;
    [SerializeField] string HighSscoreText;
    private string finalMessage;

    [SerializeField] float AgreeableScore;

    void Start()
    {
        
        Content = gameObject.GetComponent<TextMeshProUGUI>();

        if (0 < AgreeableScore && AgreeableScore < 10)
        {
            finalMessage = LowSscoreText;
        }
        else if (10 <= AgreeableScore && AgreeableScore < 15)
        {
            finalMessage = MedSscoreText;
        }
        else
        {
            finalMessage = HighSscoreText;
        }


        UpdateContent( );
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void UpdateContent( )
    {
        Content.text = finalMessage;
    }

}
