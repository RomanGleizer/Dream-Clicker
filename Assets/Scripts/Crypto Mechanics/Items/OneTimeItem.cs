using Crypto_Mechanics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneTimeItem : Item
{
    [SerializeField] public double Price;
    [SerializeField] private Task task;
    [SerializeField] private OneTimeItem[] items;
    [SerializeField] public string Name;
    [SerializeField] public TextMeshProUGUI Text;

    private bool isPossibleToBuy;

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Text.text == "Приобретено") isPossibleToBuy = true;
            else
            {
                isPossibleToBuy = false;
                break;
            }
        }

        if (playerData.TotalCurrencyCnt < Price && Text.text == "Приобретено" && !isPossibleToBuy) 
            return;

        playerData.TotalCurrencyCnt -= Price;
        Text.text = "Приобретено";
    }
}
