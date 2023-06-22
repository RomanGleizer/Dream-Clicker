using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _gameElements;
    [SerializeField] private GameObject _settings;

    public void SwitchSetting()
    {
        _gameElements.SetActive(_settings.activeSelf);
        _settings.SetActive(!_gameElements.activeSelf);
    }
}