using CryptoProject.Income;
using UnityEngine;

namespace CryptoProject;

public class CryptoCurrencyScript : ICryptoCurrency
{
    private readonly PlayerData _data;
    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;

    public CryptoCurrencyScript(PlayerData data)
    {
        _data = data;
    }

    public void AddIncomeItem(IncomeItem item)
    {
        _data.IncomeList.Add(item);
        if (item.Type == IncomeType.Active)
            _data.Incomes.Active += item.Quantity;
        else
            _data.Incomes.Passive += item.Quantity;
    }

    public void Tap()
    {
        _data.TotalCurrencyCnt += _data.Incomes.Active;
    }

    public void Spend(double count)
    {
        _data.TotalCurrencyCnt -= count;
    }

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt += IsInGame ? _data.Incomes.Passive : _data.Incomes.Passive * PassiveIncomeCoefficient;
    }
}