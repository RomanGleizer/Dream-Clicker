using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Name { get; set; }
    public double TotalCurrencyCnt { get; set; }
    public List<UpgradableItem> UpgradableItemList;
    public List<OneTimeItem> OneTimeItemList;
    public readonly TotalIncomes TotalIncomes;

    public PlayerData(string name)
    {
        Name = name;
        UpgradableItemList = new List<UpgradableItem>();
        OneTimeItemList = new List<OneTimeItem>();
        TotalIncomes = new TotalIncomes();
    }
}