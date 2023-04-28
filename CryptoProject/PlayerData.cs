using System.Collections.Generic;
using CryptoProject.Income;

namespace CryptoProject;

public class PlayerData
{
    public string Name { get; set; }
    public double TotalCurrencyCnt { get; set; }
    public List<IncomeItem> IncomeList;

    public Incomes Incomes;
}