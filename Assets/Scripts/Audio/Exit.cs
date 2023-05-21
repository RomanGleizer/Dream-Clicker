using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Sprite button, button_pressed;

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
        Application.Quit();
    }
}
