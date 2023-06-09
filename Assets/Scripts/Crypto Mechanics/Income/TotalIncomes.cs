using System;
using UnityEngine;

[Serializable]
public class TotalIncomes
{
    public double SumIncome => Passive + Active;
    public double Passive;
    public double Active = 1;
}
