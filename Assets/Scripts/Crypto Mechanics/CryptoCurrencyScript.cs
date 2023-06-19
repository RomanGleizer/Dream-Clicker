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

    private const double OfflinePassiveIncomeCf = 0.1;
    private const double OnlineassiveIncomeCf = 0.3;
    private const float PassiveIncomeRepeatRate = 1;
    private string _lastVisitDataPath = null;

    private void Awake()
    {
        AddOfflinePassiveIncome();
        InvokeRepeating(nameof(AddOnlinePassiveIncome), 1f, PassiveIncomeRepeatRate);
        _lastVisitDataPath = Application.dataPath + "/Last Visit Data.json";
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    public void AddOnlinePassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    private void AddOfflinePassiveIncome()
    {
        if (!File.Exists(_lastVisitDataPath)) return;

        var delta = DateTime.Now - DateTime.Parse(File.ReadAllText(_lastVisitDataPath));
        var income = playerData.TotalIncomes.Passive * (delta.TotalSeconds / PassiveIncomeRepeatRate) * OfflinePassiveIncomeCf;
        playerData.TotalCurrencyCnt += income;
    }
}