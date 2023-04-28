namespace CryptoProject.Income;

public abstract class Incomes
{
    public double TotalIncome => Passive + Active;
    public double Passive;
    public double Active;
}