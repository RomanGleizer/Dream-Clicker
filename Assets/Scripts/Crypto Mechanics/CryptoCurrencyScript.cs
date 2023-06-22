using System;
using System.IO;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    [SerializeField] private PlayerData playerData;

    public TextMeshProUGUI TextTotalCurrencyCnt;
    public TextMeshProUGUI TextPassive;
    public TextMeshProUGUI TextCurrencyCntPerClick;
    
    public PassiveSwitcher passiveSwitcher;
    public TextMeshProUGUI textPassiveDialogIncomeCnt;
    public TextMeshProUGUI textPassiveDialogTimeCnt;

    private const double OfflinePassiveIncomeCf = 0.1;
    private const float PassiveIncomeRepeatRate = 1;
    private string _lastVisitDataPath;

    private void Awake()
    {
        _lastVisitDataPath = Application.persistentDataPath + "/Last Visit Data.json";
    }

    private void Start()
    {
        AddOfflinePassiveIncome();
        InvokeRepeating(nameof(AddOnlinePassiveIncome), 1f, PassiveIncomeRepeatRate);
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString()[..4]) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    public void AddOnlinePassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString()[..4]) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    private void AddOfflinePassiveIncome()
    {
        if (!File.Exists(_lastVisitDataPath)) return;
        passiveSwitcher.SwitchPassive();

        var deltaTime = DateTime.Now - DateTime.Parse(File.ReadAllText(_lastVisitDataPath));
        var income = playerData.TotalIncomes.Passive * deltaTime.TotalSeconds / PassiveIncomeRepeatRate * OfflinePassiveIncomeCf;
        textPassiveDialogIncomeCnt.text = $"{Math.Round(income, 1)} D";
        textPassiveDialogTimeCnt.text = $"С момента выхода из игры прошло {deltaTime:hh\\:mm\\:ss}";
        playerData.TotalCurrencyCnt += income;
    }
}