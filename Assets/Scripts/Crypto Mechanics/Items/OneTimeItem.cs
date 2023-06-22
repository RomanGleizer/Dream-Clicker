using TMPro;
using UnityEngine;

public class OneTimeItem : Item
{
    [SerializeField] private CryptoCurrencyScript currencyScript;

    public double Price;
    public int NumberInParent;
    public Task task;
    public OneTimeItem[] items;
    public TextMeshProUGUI Text;
    public bool IsPossibleToBuy;
    public bool IsItemWasBought;

    public void InitializeTextes(SavedOneTimeItems items)
    {
        if (items.SerializableOneTimeItems.Count > 0)
        {
            if (items.SerializableOneTimeItems[NumberInParent - 1].Text != "�����������")
                Text.text = Price + " D";
            else Text.text = "�����������";
        }
        else Text.text = Price + " D";
    }
}