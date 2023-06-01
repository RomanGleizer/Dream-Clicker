using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlay : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject ObjectMusic;
    private float musicVolume;
    private AudioSource AudioSource;
    private bool muted;
    private float previousValue;
    

    private void Start()
    {
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        AudioSource = ObjectMusic.GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    private void Update()
    {
        AudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void VolumeUpdater(float value)
    {
        musicVolume = value;
    }

    public void MuteButton(SoundSwitch button)
    {
        if (muted)
        {
            VolumeUpdater(previousValue);
            volumeSlider.value = previousValue;
        }
        else
        {
            previousValue = musicVolume;
            VolumeUpdater(0f);
            volumeSlider.value = 0f;
        }
        button.Mute(muted);
        muted = !muted;
    }
}
