using Crypto_Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedBalance
{
    public string Name;
    public double TotalCurrencyCnt;
    public TotalIncomes totalIncomes;

    public SavedBalance(PlayerData data)
    {
        Name = data.PlayerName;
        TotalCurrencyCnt = data.TotalCurrencyCnt;
        totalIncomes = data.TotalIncomes;
    }
}
