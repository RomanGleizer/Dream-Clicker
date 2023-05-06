using UnityEngine;
using UnityEngine.UI;

public class OneTimeItem : Item
{
    public OneTimeItem(string name, double price, Image image, string description) 
        : base(name, price, image, description)
    {
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        playerData.TotalCurrencyCnt -= Price;
        playerData.OneTimeItemList.Add(this);
    }
}
