using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwitch : MonoBehaviour
{
    public Sprite button, button_pressed;
    public GameObject s_on, s_off;

    void Start()
    {
        if(gameObject.name == "Sound")
        {
            if (PlayerPrefs.GetString("Sound") != "no")
            {
                s_on.SetActive(false);
                s_off.SetActive(true);
            }
            else
            {
                s_on.SetActive(true);
                s_off.SetActive(false);
            }
        }
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = button_pressed;
    }

    void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = button;
    }

    void OnMouseUpAsButton()
    {
        switch(gameObject.name)
        {
            case "Sound":
                if (PlayerPrefs.GetString("Sound") != "no")
                {
                    PlayerPrefs.SetString("Sound", "no");
                    s_on.SetActive(false);
                    s_off.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetString("Sound", "yes");
                    s_on.SetActive(true);
                    s_off.SetActive(false);
                }
                break;
        }
    }
}
