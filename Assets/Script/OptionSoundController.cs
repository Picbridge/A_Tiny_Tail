using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionSoundController : MonoBehaviour
{
    public Toggle BGMToggle;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public GameObject SliderCheckmark;
    
    PlayPlayerSound playerSound;

    private void Start()
    {
        Character character = GameObject.FindObjectOfType<Character>();

        if (character)
            playerSound = character.GetComponentInChildren<PlayPlayerSound>();

        BGMSlider.value = SoundManager.instance.BGMVolume;
        SFXSlider.value = SoundManager.instance.SFXvolume;
    }

    public void OnBGMValueChange()
    {
        if (SliderCheckmark.activeInHierarchy == true && BGMSlider.value != 0)
        {
            BGMToggle.isOn = !BGMToggle.isOn;
            SliderCheckmark.SetActive(false);
        }

        SoundManager.instance.BGMValueChange(BGMSlider.value);
        
    }

     public void OnSFXValueChange()
    {        
        if (SliderCheckmark.activeInHierarchy == true && SFXSlider.value != 0)
        {
            BGMToggle.isOn = !BGMToggle.isOn;
            SliderCheckmark.SetActive(false);
        }

        SoundManager.instance.SFXValueChange(SFXSlider.value);        

        if (playerSound)
            playerSound.UpdateCharVolume();
    }

    public void MuteAll()
    {        
        SliderCheckmark.SetActive(BGMToggle.isOn);

        SoundManager.instance.IsBGMon(!BGMToggle.isOn);
        SoundManager.instance.IsSFXon(!BGMToggle.isOn);

        if (BGMToggle.isOn == true)
        {
            BGMSlider.value = 0;
            SFXSlider.value = 0;

            if (playerSound)
                playerSound.UpdateCharVolume();

        }

    }

}
