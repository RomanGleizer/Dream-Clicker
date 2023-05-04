using UnityEngine;


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
}