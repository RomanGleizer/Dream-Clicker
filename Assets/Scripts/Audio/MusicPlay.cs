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

    private void Start()
    {
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        AudioSource = ObjectMusic.GetComponent<AudioSource>();

        musicVolume = 1f;
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
    
}
