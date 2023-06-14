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
    //public Action<int[]>

    [SerializeField] public string Name;
    [SerializeField] private Task task;
    [SerializeField] private int[] itemsToAllowBuyIndexex;
    [SerializeField] public int NumberInParent;
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
        if (!File.Exists(Application.dataPath + "/Game Data.json")) return;

        var json = File.ReadAllText(Application.dataPath + "/Game Data.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (gameObject.GetComponent<ActiveButton>()
            && newData.SerializableUpActiveItems.Count >= NumberInParent)
            InitializeTextes(newData.SerializableUpActiveItems);

        if (gameObject.GetComponent<PassiveButton>()
            && newData.SerializableUpPassiveItems.Count >= NumberInParent)
            InitializeTextes(newData.SerializableUpPassiveItems);

        if ((gameObject.GetComponent<Button>()
            && newData.SerializableUpActiveItems.Count < NumberInParent)
            || (gameObject.GetComponent<Button>()
            && newData.SerializableUpPassiveItems.Count < NumberInParent))
            InitializeTextes();
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (playerData.TotalCurrencyCnt < Price || Level == MaxLevel) return;

        /*foreach (var item in itemsToAllowBuy)
        {
            if(item)
        }
        
        for (int i = 0; i < itemsToAllowBuy.Length; i++)
        {
            if (itemsToAllowBuy[i].Level > 0) isPossibleToBuy = true;
            else
            {
                isPossibleToBuy = false;
                break;
            }
        }
        */

        if ((task.Text.text == "Приобретено" && isPossibleToBuy) 
            || (task.Text.text == "Приобретено" && itemsToAllowBuyIndexex.Length == 0)
            || task.Text.text == "0 D")
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

            InitializeTextes();
        }
    }

    private void InitializeTextes()
    {
        levelText.text = $"Приобретено: {Level}";
        if (Type == IncomeType.Active)
            incomeText.text = $"Доход: {Income} D";
        else incomeText.text = $"Доход: {Income} D/s";
        priceText.text = Level == MaxLevel ? "Макс. ур." : $"{Price} D";
    }

    private void InitializeTextes<T>(List<T> lst)
        where T : SerializableUpItem
    {
        var currentLevel = Level = lst[NumberInParent - 1].Level;
        var currentIncome = Income = lst[NumberInParent - 1].Income;
        var currentPrice = Price = lst[NumberInParent - 1].Price;
        SetTextes(currentLevel, currentIncome, currentPrice);

        levelText.text = $"Приобретено: {currentLevel}";
        if (Type == IncomeType.Active)
            incomeText.text = $"Доход: {currentIncome} D";
        else incomeText.text = $"Доход: {currentIncome} D/s";
        priceText.text = currentLevel == MaxLevel ? "Макс. ур." : $"{currentPrice} D";
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
