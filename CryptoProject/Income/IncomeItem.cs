namespace CryptoProject.Income;

public class IncomeItem
{
    public readonly string Name;
    public readonly IncomeType Type;
    public readonly double Quantity;

    public IncomeItem(string name, IncomeType type, double quantity)
    {
        Name = name;
        Type = type;
        Quantity = quantity;
    }
}