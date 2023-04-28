using CryptoProject.Income;

namespace CryptoProject;

public interface ICryptoCurrency
{
    public void AddIncomeItem(IncomeItem item);
    public void Tap();
    public void Spend(double count);
    public void AddPassiveIncome();

}