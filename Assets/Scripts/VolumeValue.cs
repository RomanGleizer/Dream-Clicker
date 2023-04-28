using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource aud_sour;
    private float sound_value = 1f;

    // Start is called before the first frame update
    void Start()
    {
        aud_sour = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        aud_sour.volume = sound_value;
    }

    void SetVolume(float vol)
    {
        sound_value = vol;
    }
}
