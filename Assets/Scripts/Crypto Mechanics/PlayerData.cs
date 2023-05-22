﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private string Name;
        [SerializeField] public double TotalCurrencyCnt;
        public List<string> UpgradableItemList;
        public List<Task> Tasks; 
        public TotalIncomes TotalIncomes;

        private void Start()
        {
            UpgradableItemList = new List<string>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }

        public string GetTotalCurrency()
        {
            return TotalCurrencyCnt.ToString() + " D";
        }

        public string GetPassive()
        {
            return TotalIncomes.Passive.ToString() + " D/c";
        }
    }
}