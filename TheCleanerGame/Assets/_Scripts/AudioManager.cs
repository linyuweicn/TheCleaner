using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //---------------- Singleton ----------------
    private static AudioManager instance;
    void Awake() { instance=this; }
    //---------------- Public Static ----------------

    public static void PlayUiSound(string sfxName)
    {
        switch (sfxName)
        {
            case "click1":
                PlaySFX(instance.uiSounds[0]);
                break;
            
            case "click2":
                PlaySFX(instance.uiSounds[1]);
                break;
            
            default:
                Debug.LogWarning("Wrong sfx Name");
                break;
        }
    }

    public static void PlaySFX(AudioClip clip)
    {
        instance._playSFX(clip);
    }
    
    //---------------- Variables ----------------
    public AudioSource[] audioSources;
    
    public AudioClip[] uiSounds;
    
    
    //---------------- Unity Entrance ----------------
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    //---------------- Private method ----------------
    private void _playSFX(AudioClip clip)
    {
        foreach (var source in audioSources)
        {
            if (source.isPlaying) continue;
            source.PlayOneShot(clip);
        }
    }
    
}
