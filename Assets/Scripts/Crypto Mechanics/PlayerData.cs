using System.Collections.Generic;
using CryptoProject.Income;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Name { get; set; }
    public double TotalCurrencyCnt { get; set; }
    public List<IncomeItem> IncomeList;
    public Incomes Incomes;
}