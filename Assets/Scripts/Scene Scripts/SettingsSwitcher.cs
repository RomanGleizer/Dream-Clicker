using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _gameElements;
    [SerializeField] private GameObject _settings;

    public void SwitchSetting()
    {
        _gameElements.SetActive(_gameElements.activeSelf!);
        _settings.SetActive(_gameElements.activeSelf);
    }
}