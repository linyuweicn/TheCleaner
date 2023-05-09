using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBGM : MonoBehaviour
{
    // thsi csript swpaed the main BGM based on the curretn score
    [SerializeField] int lowMedBound;
    [SerializeField] int medHighBound;

    private float score;

    public AudioClip LowBMG;
    public AudioClip MedBMG;
    public AudioClip HighBMG;

    public AudioSource audioSource;
    void Start()
    {
        score = ConvoGlobalManager.agreeableScore + ConvoGlobalManager.overallTotalScore;
        Debug.Log(score + "in audio");

        if (score <= lowMedBound)
        {
            audioSource.clip = LowBMG;
            
        }
        else if (lowMedBound < score && score <= medHighBound)
        {
            audioSource.clip = MedBMG;
        }
        else if (score > medHighBound)
        {
            audioSource.clip = HighBMG;
        }
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
