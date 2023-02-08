using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSaveController : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] float musicVolume = 0.5f;

    private void Start()
    {
        //LoadValue();
        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = musicVolume;
        VolumeSlider.value = musicVolume;
    }

    void Update()
    {

        // Setting volume option of Audio Source to be equal to musicVolume
        audioSrc.volume = musicVolume;
    }

    public void SaveVolumeButton()
    {
        float VolumeValue = VolumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", VolumeValue);
        LoadValue();
    }

    public void LoadValue()
    {
        float VolumeValue = PlayerPrefs.GetFloat("VolumeValue");
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }



    
    

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
