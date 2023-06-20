using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _gameElements;
    [SerializeField] private GameObject _settings;

    public void SwitchSetting()
    {
        if (_gameElements.activeSelf)
        {
            _gameElements.SetActive(false);
            _settings.SetActive(true);
        }
        else
        {
            _gameElements.SetActive(true);
            _settings.SetActive(false);
        }
    }
}
