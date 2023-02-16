using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //---------------- Singleton ----------------
    private static AudioManager instance;
    public float fadeTime = 1;



    void Awake() { instance = this; }
    //---------------- Public Static ----------------

    public void PlayUiSound(string sfxName) // why public static void?
    {
        switch (sfxName)
        {
            case "ui_click1":
                PlaySFX(instance.uiSounds[0]);
                break;

            case "ui_confirm":
                PlaySFX(instance.uiSounds[1]);
                break;

            case "ui_drag_03":
                PlaySFX(instance.uiSounds[2]);
                break;
            case "ui_click":
                PlaySFX(instance.uiSounds[3]);
                break; 
            case "ui_highlight":
                PlaySFX(instance.uiSounds[4]);
                break;
            case "ui_click04":
                PlaySFX(instance.uiSounds[5]);
                break;

            default:
                Debug.LogWarning("Wrong sfx Name");
                break;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        instance._playSFX(clip);
    }



    public void FadeAudio (AudioSource audioSource)
    {     
            audioSource.volume = Mathf.Lerp(1, 0,3 );
      
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
