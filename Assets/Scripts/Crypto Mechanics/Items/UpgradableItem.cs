using System;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradableItem : Item
{
    [SerializeField] public string Name;
    private const int MaxLevel = 10;
    private const double UpgradeCoefficient = 1.8;
    [SerializeField] private int Level;
    [SerializeField] private IncomeType Type;
    [SerializeField] private double Income;
    [SerializeField] private double price;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = price.ToString();
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (playerData.TotalCurrencyCnt < price || Level == MaxLevel) return;
        double deltaIncome = Income;
        if (Level == 0) playerData.UpgradableItemList.Add(Name);
        else
        {
            var previousIncome = Income;
            Income *= UpgradeCoefficient;
            deltaIncome = Income - previousIncome;
        }
        if (Type == IncomeType.Active)
            playerData.TotalIncomes.Active += deltaIncome;
        else
            playerData.TotalIncomes.Passive += deltaIncome;
        playerData.TotalCurrencyCnt -= price;
        Level++;
        price *= UpgradeCoefficient;
        price = Math.Ceiling(price);
        if (Level == MaxLevel) _text.text = "Макс. ур.";
        else _text.text = price.ToString();
    }
}
