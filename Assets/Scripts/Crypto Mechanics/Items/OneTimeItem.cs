using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneTimeItem : Item
{
    [SerializeField] public double Price;
    [SerializeField] public int NumberInParent;
    [SerializeField] private Task task;
    [SerializeField] private OneTimeItem[] items;
    [SerializeField] public TextMeshProUGUI Text;

    private bool isPossibleToBuy;

    private void Start()
    {
        var json = File.ReadAllText("Assets/Resources/savedData.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);
        if (newData == null) Text.text = Price.ToString() + " D";

        if (newData.SerializableOneTimeItems[NumberInParent - 1].Text == "Приобретено")
            Text.text = "Приобретено";
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

        if (playerData.TotalCurrencyCnt < Price && Text.text == "Приобретено" && !isPossibleToBuy) 
            return;

        playerData.TotalCurrencyCnt -= Price;
        Text.text = "Приобретено";
    }
}
