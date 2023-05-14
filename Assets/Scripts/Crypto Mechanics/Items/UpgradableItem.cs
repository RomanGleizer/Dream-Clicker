using Crypto_Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class UpgradableItem : Item
{
    private const int MaxLevel = 10;
    private const double UpgradeCoefficient = 1.8;
    public int Level { get; private set; }
    public readonly IncomeType Type;
    public double Income { get; protected set; }

    public UpgradableItem(
        string name,
        IncomeType type,
        double income,
        double price,
        Image image,
        string description
    ) : base(name, price, image, description)
    {
        Type = type;
        Income = income;
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (Level == MaxLevel) return;
        if (Level == 0) playerData.UpgradableItemList.Add(this);
        else
        {
            var previousIncome = Income;
            Income *= UpgradeCoefficient;
            var deltaIncome = Income - previousIncome;

            if (Type == IncomeType.Active)
                playerData.TotalIncomes.Active += deltaIncome;
            else
                playerData.TotalIncomes.Passive += deltaIncome;
        }

        playerData.TotalCurrencyCnt -= Price;
        Level++;
        Price *= UpgradeCoefficient;
    }
}
