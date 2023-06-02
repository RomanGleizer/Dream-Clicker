using System;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradableItem : Item
{
    [SerializeField] public string Name;
    private const int MaxLevel = 25;
    private const double UpgradeCoefficient = 1.3;
    [SerializeField] private int Level;
    [SerializeField] private IncomeType Type;
    [SerializeField] private double Income;
    [SerializeField] private double price;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Start()
    {
        levelText.text = $"Приобретено: {Level}";
        incomeText.text = $"Доход: {Income} D/s";
        priceText.text = Level == MaxLevel ? "Макс. ур." : $"{price} D";
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (playerData.TotalCurrencyCnt < price || Level == MaxLevel) return;
        var deltaIncome = Income;
        if (Level == 0) playerData.UpgradableItemList.Add(Name);
        else
        {
            var previousIncome = Income;
            Income = Math.Round(Income * UpgradeCoefficient, 1);
            deltaIncome = Income - previousIncome;
        }
        if (Type == IncomeType.Active)
            playerData.TotalIncomes.Active += deltaIncome;
        else
            playerData.TotalIncomes.Passive += deltaIncome;
        playerData.TotalCurrencyCnt -= price;
        Level++;
        price = Math.Round(price * UpgradeCoefficient, 1);

        Start();
    }
}
