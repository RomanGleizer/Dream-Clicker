using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{

    private int _number;

    public void IncrementNumber()
    {
        _number++;
        _text.text = _number.ToString();
    }
}
