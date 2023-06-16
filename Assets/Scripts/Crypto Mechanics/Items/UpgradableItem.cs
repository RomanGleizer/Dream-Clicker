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

    [SerializeField] public string Name;
    [SerializeField] public Task task;
    [SerializeField] public UpgradableItem[] items;
    [SerializeField] public int NumberInParent;
    [SerializeField] private PlayerData playerData;
    [SerializeField] public int Level;
    [SerializeField] public IncomeType Type;
    [SerializeField] public double Income;
    [SerializeField] public double Price;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;

    public bool isPossibleToBuy;

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
