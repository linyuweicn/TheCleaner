using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConvoGlobalManager : MonoBehaviour
{
    [HideInInspector] 
    int nextScene;
    public static int agreeableScore;
    public static int overallTotalScore; //across all levels
    public static int dailyTotalScore; //specific to one level

    [HideInInspector]
    public bool BuyLicense;
    public static bool Betrayl;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1; //default
    }

    public void setNextScene(int scene)
    {
        nextScene = scene;
    }

    public int getNextScene()
    {
        return nextScene;
    }

    public void IncreaseAgreeableScore(int inc)
    {
        agreeableScore += inc;
        Debug.Log("Agreeable score is " + agreeableScore);
    }

    public void DecreaseAgreeableScore(int dec)
    {
        agreeableScore -= dec;
        Debug.Log("Agreeable score is " + agreeableScore);
    }
    
    public void SetNewAgreeableScore(int newScore)
    {
        agreeableScore = newScore;
        Debug.Log("Agreeable score is " + agreeableScore);
    }

    public void UpdateScoreWithWhiteboardScore(int totalScore)
    {
        dailyTotalScore = 0; //reset daily total score
        dailyTotalScore = totalScore;
        overallTotalScore += dailyTotalScore;
        Debug.Log("dailyTotalScore"+ dailyTotalScore + " " + "overallTotalScore"+overallTotalScore);
    }

    public void BuyLicenseResult()
    {
        BuyLicense = true;
        Debug.Log(BuyLicense + "BuyLicense");
    }

    public void LiaBsetrays()
    {
        Betrayl = true;
        Debug.Log(Betrayl + "Betrayl");
    }
}
