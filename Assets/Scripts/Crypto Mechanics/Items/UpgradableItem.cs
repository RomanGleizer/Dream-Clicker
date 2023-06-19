using System;
using System.Collections.Generic;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;

[Serializable] 
public class UpgradableItem : Item
{
    public const int MaxLevel = 25;
    public const double UpgradeActiveCoefficient = 1.3;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;

    public Task Task;
    public UpgradableItem[] items;
    public int NumberInParent;
    public int Level;
    public IncomeType Type;
    public double Income;
    public double Price;
    public bool IsPossibleToBuy;

    public void InitializeTextes<T>(List<T> lst)
        where T : SerializableUpItem
    {
        var currentLevel = Level = lst[NumberInParent - 1].Level;
        var currentIncome = Income = lst[NumberInParent - 1].Income;
        var currentPrice = Price = lst[NumberInParent - 1].Price;
        SetTextes(currentLevel, currentIncome, currentPrice);
    }

    public void SetTextes(int level, double income, double price)
    {
        levelText.text = $"Приобретено: {level}";
        if (Type == IncomeType.Active)
            incomeText.text = $"Доход: {income} D";
        else incomeText.text = $"Доход: {income} D/s";
        priceText.text = level == MaxLevel ? "Макс. ур." : $"{price} D";
    }
}
