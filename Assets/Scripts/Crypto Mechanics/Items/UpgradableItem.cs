using System;
using Crypto_Mechanics;
using Crypto_Mechanics.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable] 
public class UpgradableItem : Item
{
    [SerializeField] public string Name;
    private const int MaxLevel = 25;
    private const double UpgradeCoefficient = 1.3;
    [SerializeField] public int Level;
    [SerializeField] public IncomeType Type;
    [SerializeField] public double Income;
    [SerializeField] public double price;
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI incomeText;
    [SerializeField] public TextMeshProUGUI priceText;

    public UpgradableItem Init(SerializableUpItem upItem)
    {
        Name = upItem.name;
        Level = upItem.level;
        Type = upItem.type;
        Income = upItem.income;
        price = upItem.price;
        return this;
    }

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
        if (Level == 0) playerData.UpgradableItemList.Add(this);
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
