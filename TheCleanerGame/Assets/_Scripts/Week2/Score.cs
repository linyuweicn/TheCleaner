using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public static float PlayerSatisValue = 50;
    public static float SafetyValue = 50;
    public static float MoneyValue = 50;
    public string HeadingText;

    public GameObject PlayerSatisfication;
    Text PlayerSatTex;    
    
    public GameObject Safety;
    Text SafetyTex;

    public GameObject Money;
    Text MoneyTex;


    // Use this for initialization
    void Start()
    {
        PlayerSatTex = PlayerSatisfication.GetComponent<Text>();
        SafetyTex = Safety.GetComponent<Text>();
        MoneyTex = Money.GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        PlayerSatTex.text = HeadingText + PlayerSatisValue;
        SafetyTex.text = HeadingText + SafetyValue;
        MoneyTex.text = HeadingText + MoneyValue;

     
    }
}
