using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class optionScript : MonoBehaviour
{
    public AudioSource BGMSource;
    public Slider BGM;
    public Slider SFX;
    float sfxValue = 0.1f;
    public GameObject optionScreen;
    public Button Selectable;
    void Start()
    {
        BGM.value = BGMSource.volume;
        SFX.value = sfxValue;
    }

    public void ChangeBGMVolume() 
    {
        BGMSource.volume = BGM.value;
    }

    public void ChangeSFXVolume() 
    {

        sfxValue = SFX.value;

    }

    public void OpenOptions() 
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
        optionScreen.SetActive(true);
        BGM.Select();
    
    }

    public void ExitOptions() 
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
        foreach (AudioSource item in GameObject.FindObjectsOfType<AudioSource>())
        {
            if (item.tag != "BGM")
            {
                item.volume = sfxValue;
            }
        }
        optionScreen.SetActive(false);
        Selectable.Select();

    }
}
