using Crypto_Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class OneTimeItem : Item
{
    [SerializeField] private double price;
    public override void BuyOrUpgrade(PlayerData playerData)
    {
        if (playerData.TotalCurrencyCnt < price) return;
        playerData.TotalCurrencyCnt -= price;
    }
}
