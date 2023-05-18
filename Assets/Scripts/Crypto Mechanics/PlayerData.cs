﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private string Name;
        [SerializeField] public double TotalCurrencyCnt;
        public List<UpgradableItem> UpgradableItemList;
        public List<Task> Tasks; 
        public TotalIncomes TotalIncomes;

        private void Start()
        {
            UpgradableItemList = new List<UpgradableItem>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }
    }
}