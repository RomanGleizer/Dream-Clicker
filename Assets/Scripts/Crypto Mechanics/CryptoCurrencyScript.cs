﻿using System;
using System.Linq;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    [SerializeField] PlayerData _data;
    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;
    [SerializeField] public TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(_data);
        Start();
    }

    private void Start()
    {
        textTotalCurrencyCnt.text = $"{Math.Round(_data.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{_data.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{_data.TotalIncomes.Active} D";
    }

    public void Tap()
    {
        _data.TotalCurrencyCnt += _data.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(_data.TotalCurrencyCnt, 1)} D";
    } 

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt +=
            IsInGame ? _data.TotalIncomes.Passive : _data.TotalIncomes.Passive * PassiveIncomeCoefficient;
    }

    public void BuyTask(Task task)
    {
        task.Buy(_data);
        Start();
    }
}