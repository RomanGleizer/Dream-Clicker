using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _gameElements;
    [SerializeField] private GameObject _passive;

    public void SwitchPassive()
    {
        _passive.SetActive(_gameElements.activeSelf);
        _gameElements.SetActive(!_gameElements.activeSelf);
    }
}