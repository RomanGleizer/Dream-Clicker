using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    public Sprite button, button_pressed;

    public void Mute(bool muted)
    {
        GetComponent<Image>().sprite = muted ? button : button_pressed;
    }
}
