using System;
using System.Collections.Generic;
using System.IO;
using Crypto_Mechanics;
using Crypto_Mechanics.Items;
using Crypto_Mechanics.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable] 
public class UpgradableItem : Item
{
    public const int MaxLevel = 25;
    private const double UpgradeActiveCoefficient = 1.3;

    [SerializeField] public string Name;
    [SerializeField] private Task task;
    [SerializeField] private UpgradableItem[] items;
    [SerializeField] public int NumberInParent;
    [SerializeField] private PlayerData playerData;
    [SerializeField] public int Level;
    [SerializeField] public IncomeType Type;
    [SerializeField] public double Income;
    [SerializeField] public double Price;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;

    private bool isPossibleToBuy;

    public void Init(SerializableUpActiveItem upItem)
    {
        Name = upItem.Name;
        Level = upItem.Level;
        Type = upItem.Type;
        Income = upItem.Income;
        Price = upItem.Price;
    }

    private void Start()
    {
        if (!File.Exists(Application.dataPath + "/savedData.json")) return;

        var json = File.ReadAllText(Application.dataPath + "/savedData.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (!File.Exists(Application.dataPath + "/Actives.json")) return;
        var activesData = JsonUtility.FromJson<SavedActives>(
            File.ReadAllText(Application.dataPath + "/Actives.json"));

        if (!File.Exists(Application.dataPath + "/Passives.json")) return;
        var passivesData = JsonUtility.FromJson<SavedPassives>(
            File.ReadAllText(Application.dataPath + "/Passives.json"));

        if (gameObject.GetComponent<ActiveButton>()
            && activesData.SerializableUpActiveItems.Count >= NumberInParent)
            InitializeTextes(activesData.SerializableUpActiveItems);

        if (gameObject.GetComponent<PassiveButton>()
            && passivesData.SerializableUpPassiveItems.Count >= NumberInParent)
            InitializeTextes(passivesData.SerializableUpPassiveItems);

        if ((gameObject.GetComponent<Button>() 
            && activesData.SerializableUpActiveItems.Count < NumberInParent)
            || (gameObject.GetComponent<Button>() 
            && passivesData.SerializableUpPassiveItems.Count < NumberInParent))
            SetTextes(Level, Income, Price);
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (playerData.TotalCurrencyCnt < Price || Level == MaxLevel) return;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Level > 0) isPossibleToBuy = true;
            else
            {
                isPossibleToBuy = false;
                break;
            }
        }
        // task.Text.text == "0 D"
        if ((task.Text.text == "Приобретено" && isPossibleToBuy) 
            || ((task.Text.text == "Приобретено" || task.Text.text == "Пусто") && items.Length == 0))
        {
            var deltaIncome = Income;
            var previousIncome = Income;

            Income = Math.Round(Income * UpgradeActiveCoefficient, 1);
            deltaIncome = Income - previousIncome;

            if (Level == 0) deltaIncome = Income;
            if (Type == IncomeType.Active)
                playerData.TotalIncomes.Active += deltaIncome;
            else if (Type == IncomeType.Passive)
                playerData.TotalIncomes.Passive += deltaIncome;

            playerData.TotalCurrencyCnt -= Price;
            Level++;
            
            Price = Math.Round(Price * UpgradeActiveCoefficient, 1);

            SetTextes(Level, Income, Price);
        }
    }

    private void InitializeTextes<T>(List<T> lst)
        where T : SerializableUpItem
    {
        var currentLevel = Level = lst[NumberInParent - 1].Level;
        var currentIncome = Income = lst[NumberInParent - 1].Income;
        var currentPrice = Price = lst[NumberInParent - 1].Price;
        SetTextes(currentLevel, currentIncome, currentPrice);
    }

    private void SetTextes(int level, double income, double price)
    {
        levelText.text = $"Приобретено: {level}";
        if (Type == IncomeType.Active)
            incomeText.text = $"Доход: {income} D";
        else incomeText.text = $"Доход: {income} D/s";
        priceText.text = level == MaxLevel ? "Макс. ур." : $"{price} D";
    }
}
