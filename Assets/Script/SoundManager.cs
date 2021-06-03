using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{   
    public AudioSource[] soundEffectChannel;
    AudioSource BGMSource;

    //we have to assign audioclip in here, and assign it's name in SoundsNames so that we can find audioclip in SFXs(Dictionary)
    public AudioClip[] SampleSounds;
    public string[] SoundsNames;

    public AudioClip[] SampleBGM;
    public string[] BGMNames;

    Dictionary<string, AudioClip> SFXs;

    public GameObject SettingPanel;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public float BGMVolume;
    public float SFXvolume;
    
    //We can add more audio sources for multi channel
    const int MAX_NUMBER_OF_CHANNEL = 10;
    int numberOfChannel;
    int count = 0;
    void Start()
    {
        SFXs = new Dictionary<string, AudioClip>();

        numberOfChannel = soundEffectChannel.Length;

        BGMSource = GetComponent<AudioSource>();
        BGMSource.clip = SampleBGM[0];

        BGMSource.volume = BGMVolume = 0.1f;                

        AssignAudioClip(SampleSounds, SoundsNames);

        SFXValueChange(0.5f);

    }

    AudioSource FindEmptyChannel()
    {
        //for (int i = 0; i < numberOfChannel; i++)
        //{
        //    if (soundEffectChannel[i].isPlaying == false)
        //        return soundEffectChannel[i];
        //}

        if (count >= 10)
            count = 0;

        return soundEffectChannel[count++];
    }

    //Assign all sounds which we want to use
    void AssignAudioClip(AudioClip[] audios, string[] audiosName)
    {
        for (int i = 0; i < audios.Length; i++)        
            SFXs.Add(audiosName[i], audios[i]);
    }

    //Play the sound. command is : SoundManager.instance.PlaySound("soundname"(string));
    public void PlaySound(string soundName)
    {        
        AudioClip output;
        SFXs.TryGetValue(soundName, out output);

        if (output == null)
        {
            Debug.Log("SOUND_MANAGER : ERROR_NO_SUCH '" +  soundName + "' IN SOUND ASSET, (Please type sound's name correctly)");
            return;
        }

        AudioSource audio = FindEmptyChannel();

        if (audio == null)
        {
            Debug.Log("SOUND_MANAGER : ERROR_NO_EMPTY_CHANNEL, (Please increase the capacity of channel)");
            return;
        }

        audio.clip = output;
        audio.Play();        
    }

    //Play BGM, it is called when game is started, so we don't have to call it in other scene.
    //If we want to change the BGM, just call : SoundManger:instance.ChangeBGM("BGMname"(string));
    public void PlayBGM()
    {        
        if(!BGMSource.isPlaying)
            BGMSource.Play();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }

    public void ChooseBGM(int index)
    {
        BGMSource.clip = SampleBGM[index];
    }

    //instruction is written in PlayBGM, change the BGM
    void ChangeBGM(string BGM_name)
    {     
        BGMSource.Stop();        
        //BGMSource.clip = output;

        BGMSource.Play();
    }

    //It is for setting of BGM. We can adjust it in sound setting panel
    public void IsBGMon(bool isOn)
    {
        if (isOn)
        {
            BGMSource.volume = 0.0f;
            BGMVolume = 0.0f;
        }
        else
        {
            BGMSource.volume = 0.0f;
            BGMVolume = 0.0f;
        }
    }
    
    //It is for setting of BGM. We can adjust it in sound setting panel
    public void IsSFXon(bool isOn)
    {
        if (isOn)
        {
            for(int i = 0; i < numberOfChannel; i++)
                soundEffectChannel[i].volume = 1.0f;
        }
        else
        {
            for (int i = 0; i < numberOfChannel; i++)
                soundEffectChannel[i].volume = 0.0f;
        }
    }

    public void BGMValueChange(float value)
    {
        //BGMSource.volume = BGMSlider.value;        

        BGMSource.volume = BGMVolume = value;
        
    }

    public void SFXValueChange(float value)
    {        
        //for (int i = 0; i < numberOfChannel; i++)
        //    soundEffectChannel[i].volume = SFXSlider.value;

        SFXvolume = value;

        for (int i = 0; i < numberOfChannel; i++)
            soundEffectChannel[i].volume = value;
    }

    //Close the setting panel
    public void CloseSettingPanel()
    {
        PlaySound("ClickSound");
        Cursor.lockState = CursorLockMode.Locked;
        SettingPanel.SetActive(false);
    }
}
