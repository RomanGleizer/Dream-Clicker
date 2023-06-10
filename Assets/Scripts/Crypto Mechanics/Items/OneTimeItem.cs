using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using System.IO;
using TMPro;
using UnityEngine;

public class OneTimeItem : Item
{
    [SerializeField] private CryptoCurrencyScript currencyScript;
    [SerializeField] public double Price;
    [SerializeField] public int NumberInParent;
    [SerializeField] private Task task;
    [SerializeField] private OneTimeItem[] items;
    [SerializeField] public TextMeshProUGUI Text;

    private bool isPossibleToBuy;

    private void Start()
    {
        if (!File.Exists(Application.dataPath + "/Game Data.json")) return;
        
        var json = File.ReadAllText(Application.dataPath + "/Game Data.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (newData.SerializableOneTimeItems.Count > 0)
        {
            if (newData.SerializableOneTimeItems[NumberInParent - 1].Text != "Приобретено")
                Text.text = Price.ToString() + " D";
            else Text.text = "Приобретено";
        }
        else Text.text = Price.ToString() + " D";
    }

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

        if (playerData.TotalCurrencyCnt < Price && Text.text == "Приобретено") 
            return;
        else if (playerData.TotalCurrencyCnt >= Price 
            && task.Text.text == "Приобретено" 
            && (isPossibleToBuy || items.Length == 0))
        {
            playerData.TotalCurrencyCnt -= Price;
            Text.text = "Приобретено";
            currencyScript.TextTotalCurrencyCnt.text = playerData.TotalCurrencyCnt.ToString();
        }
    }
}
