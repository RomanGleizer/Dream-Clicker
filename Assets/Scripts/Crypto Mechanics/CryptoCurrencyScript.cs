using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    private readonly PlayerData _data;
    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;

    public CryptoCurrencyScript(PlayerData data)
    {
        _data = data;
    }

    public void BuyOrUpgrade(Item item)
    {
        if (_data.TotalCurrencyCnt >= item.Price) item.BuyOrUpgrade(_data);
    }

    public void Tap() => _data.TotalCurrencyCnt += _data.TotalIncomes.Active;

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt +=
            IsInGame ? _data.TotalIncomes.Passive : _data.TotalIncomes.Passive * PassiveIncomeCoefficient;
    }

    public void BuyTask(Task task)
    {
        if (_data.TotalCurrencyCnt >= task.Cost && task.Requirements.All(x => _data.IncomeList.Contains(x)))
        {
            Spend(task.Cost);
            _data.TotalCurrencyCnt += task.SingleBonus;
            GetComponent<Image>().sprite = task.ButtonPressed;
            task.IsGet = true;
        }
    }
    public void Spend(double count)
    {
        _data.TotalCurrencyCnt -= count;
    }
}