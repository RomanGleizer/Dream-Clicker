using TMPro;
using UnityEngine;

public class OneTimeItem : Item
{
    [SerializeField] private CryptoCurrencyScript currencyScript;
    [SerializeField] public double Price;
    [SerializeField] public int NumberInParent;
    [SerializeField] public Task task;
    [SerializeField] public OneTimeItem[] items;
    [SerializeField] public TextMeshProUGUI Text;

    public bool isPossibleToBuy;

    public void InitializeTextes(SavedOneTimeItems items)
    {
        if (items.SerializableOneTimeItems.Count > 0)
        {
            if (items.SerializableOneTimeItems[NumberInParent - 1].Text != "Приобретено")
                Text.text = Price.ToString() + " D";
            else Text.text = "Приобретено";
        }
        else Text.text = Price.ToString() + " D";
    }
}