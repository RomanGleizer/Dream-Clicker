using System;
using UnityEngine;

[Serializable]
public class SerializableUpItem
{
    public string Name;
    public int Level;
    public double Income;
    public double Price;
    public IncomeType Type;

    public SerializableUpItem(UpgradableItem upItem)
    {
        if (upItem != null)
        {
            Name = upItem.name;
            Level = upItem.Level;
            Income = upItem.Income;
            Price = upItem.Price;
            Type = upItem.Type;
        }
    }
}
