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

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(_data);
        TextIncome.text = _data.TotalCurrencyCnt.ToString();
    }

    private void Start()
    {
        TextIncome.text = _data.TotalCurrencyCnt.ToString();
    }

    public void Tap()
    {
        _data.TotalCurrencyCnt += _data.TotalIncomes.Active;
        _data.TotalCurrencyCnt = Math.Ceiling(_data.TotalCurrencyCnt);
        TextIncome.text = _data.TotalCurrencyCnt.ToString();
    } 

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt +=
            IsInGame ? _data.TotalIncomes.Passive : _data.TotalIncomes.Passive * PassiveIncomeCoefficient;
    }

    public void BuyTask(Task task)
    {
        if (_data.TotalCurrencyCnt >= task.Cost && task.Requirements.All(x => _data.UpgradableItemList.Contains(x)))
        {
            Spend(task.Cost);
            _data.TotalCurrencyCnt += task.SingleBonus;
            GetComponent<Image>().sprite = task.ButtonPressed;
            task.IsGet = true;
        }
    }
    public void Spend(double count)
    {
        _data.TotalCurrencyCnt -= count;
    }
}