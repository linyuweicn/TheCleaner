using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsContent : MonoBehaviour
{
  
    private TextMeshProUGUI Content;
    [TextArea(3, 10)]
    [SerializeField] string LowSscoreText;
    [TextArea(3, 10)]
    [SerializeField] string MedSscoreText;
    [TextArea(3, 10)]
    [SerializeField] string HighSscoreText;
    private string finalMessage;

    private float score;
    [SerializeField] int lowMedBound = 0;
    [SerializeField] int medHighBound = 5;

    void Start()
    {
        score = ConvoGlobalManager.agreeableScore + ConvoGlobalManager.overallTotalScore;
        Debug.Log("news score is " + score);
        Content = gameObject.GetComponent<TextMeshProUGUI>();

        if (score  < lowMedBound)
        {
            finalMessage = LowSscoreText;
        }
        else if (lowMedBound < score && score < medHighBound)
        {
            finalMessage = MedSscoreText;
        }
        else if (score > medHighBound)
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
