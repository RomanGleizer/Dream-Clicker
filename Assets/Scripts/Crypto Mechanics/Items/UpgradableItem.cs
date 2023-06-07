using System;
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
    private const double UpgradeCoefficient = 1.3;

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
        var json = File.ReadAllText("Assets/Resources/savedData.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (gameObject.GetComponent<ActiveButton>()  
            && newData.SerializableUpActiveItems.Count >= NumberInParent)
        {
            var currentLevel = Level = newData.SerializableUpActiveItems[NumberInParent - 1].Level;
            var currentIncome = Income = newData.SerializableUpActiveItems[NumberInParent - 1].Income;
            var currentPrice = Price = newData.SerializableUpActiveItems[NumberInParent - 1].Price;

            levelText.text = $"Приобретено: {currentLevel}";
            incomeText.text = $"Доход: {currentIncome} D/s";
            priceText.text = currentLevel == MaxLevel ? "Макс. ур." : $"{currentPrice} D";
        }

        if (gameObject.GetComponent<PassiveButton>()
            && newData.SerializableUpPassiveItems.Count >= NumberInParent)
            {
                var currentLevel = Level = newData.SerializableUpPassiveItems[NumberInParent - 1].Level;
                var currentIncome = Income = newData.SerializableUpPassiveItems[NumberInParent - 1].Income;
                var currentPrice = Price = newData.SerializableUpPassiveItems[NumberInParent - 1].Price;

                levelText.text = $"Приобретено: {currentLevel}";
                incomeText.text = $"Доход: {currentIncome} D/s";
                priceText.text = currentLevel == MaxLevel ? "Макс. ур." : $"{currentPrice} D";
            }

        if (gameObject.GetComponent<Button>() && newData.SerializableUpActiveItems.Count < NumberInParent)
            InitializeTextes();

        if (gameObject.GetComponent<Button>() && newData.SerializableUpPassiveItems.Count < NumberInParent)
            InitializeTextes();
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

        if ((task.Text.text == "Приобретено" && isPossibleToBuy) 
            || (task.Text.text == "Приобретено" && items.Length == 0)
            || task.Text.text == "0 D")
        {
            var deltaIncome = Income;

            var previousIncome = Income;
            Income = Math.Round(Income * UpgradeCoefficient, 1);
            deltaIncome = Income - previousIncome;
            if (Level == 0) deltaIncome = Income;

            if (Type == IncomeType.Active)
                playerData.TotalIncomes.Active += deltaIncome;
            else if (Type == IncomeType.Passive)
                playerData.TotalIncomes.Passive += deltaIncome;

            playerData.TotalCurrencyCnt -= Price;
            Level++;
            Price = Math.Round(Price * UpgradeCoefficient, 1);

            InitializeTextes();
        }
    }

    private void InitializeTextes()
    {
        levelText.text = $"Приобретено: {Level}";
        incomeText.text = $"Доход: {Income} D/s";
        priceText.text = Level == MaxLevel ? "Макс. ур." : $"{Price} D";
    }
}
