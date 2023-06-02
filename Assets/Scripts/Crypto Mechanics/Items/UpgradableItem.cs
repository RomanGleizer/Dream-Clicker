using System;
using Crypto_Mechanics;
<<<<<<< HEAD
=======
using Crypto_Mechanics.Items;
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradableItem : Item
{
    [SerializeField] public string Name;
    private const int MaxLevel = 25;
    private const double UpgradeCoefficient = 1.3;
<<<<<<< HEAD
    [SerializeField] private int Level;
    [SerializeField] private IncomeType Type;
    [SerializeField] private double Income;
    [SerializeField] private double price;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;

=======
    [SerializeField] public int Level;
    [SerializeField] public IncomeType Type;
    [SerializeField] public double Income;
    [SerializeField] public double price;
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI incomeText;
    [SerializeField] public TextMeshProUGUI priceText;

    public void Init(SerializableUpItem upItem)
    {
        Name = upItem.name;
        Level = upItem.level;
        Type = upItem.type;
        Income = upItem.income;
        price = upItem.price;
    }

>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
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
<<<<<<< HEAD
        if (Level == 0) playerData.UpgradableItemList.Add(Name);
=======
        if (Level == 0) playerData.UpgradableItemList.Add(this);
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
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
