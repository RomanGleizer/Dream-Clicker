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
        if (newData is null)
        {
            Text.text = $"{Price} D";
            return;
        }

        if (newData.SerializableOneTimeItems[NumberInParent - 1].Text == "�����������")
            Text.text = "�����������";
        else Text.text = $"{Price} D";
    }

    public override void BuyOrUpgrade(PlayerData playerData)
    {
        foreach (var item in items)
        {
            if (item.Text.text == "�����������") isPossibleToBuy = true;
            else
            {
                isPossibleToBuy = false;
                break;
            }
        }

        if (playerData.TotalCurrencyCnt < Price && Text.text == "�����������" && !isPossibleToBuy) 
            return;

        playerData.TotalCurrencyCnt -= Price;
        Text.text = "�����������";
    }
}
