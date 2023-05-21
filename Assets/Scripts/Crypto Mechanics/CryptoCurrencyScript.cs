using System;
using System.Linq;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    [SerializeField] PlayerData _data;
    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;
    [SerializeField] public TextMeshProUGUI TextIncome;
    [SerializeField] public TextMeshProUGUI TextPassive;

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(_data);
        TextIncome.text = _data.GetTotalCurrency();
        TextPassive.text = _data.GetPassive();
    }

    private void Start()
    {
        TextIncome.text = _data.GetTotalCurrency();;
    }

    public void Tap()
    {
        _data.TotalCurrencyCnt += _data.TotalIncomes.Active;
        _data.TotalCurrencyCnt = Math.Ceiling(_data.TotalCurrencyCnt);
        TextIncome.text = _data.GetTotalCurrency();;
    } 

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt +=
            IsInGame ? _data.TotalIncomes.Passive : _data.TotalIncomes.Passive * PassiveIncomeCoefficient;
    }

    public void BuyTask(Task task)
    {
        task.Buy(_data);
    }
}