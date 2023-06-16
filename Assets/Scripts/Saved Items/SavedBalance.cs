using Crypto_Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedBalance
{
    public double TotalCurrencyCnt;
    public TotalIncomes totalIncomes;

    public SavedBalance(PlayerData data)
    {
        TotalCurrencyCnt = data.TotalCurrencyCnt;
        totalIncomes = data.TotalIncomes;
    }
}