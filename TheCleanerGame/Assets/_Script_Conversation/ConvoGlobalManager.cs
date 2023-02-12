using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConvoGlobalManager : MonoBehaviour
{
    [HideInInspector] int nextScene;
    [SerializeField] public static int agreeableScore;

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
    }

    public void DecreaseAgreeableScore(int dec)
    {
        agreeableScore -= dec;
    }
    
    public void SetNewAgreeableScore(int newScore)
    {
        agreeableScore = newScore;
    }
}
