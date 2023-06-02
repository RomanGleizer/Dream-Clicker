using System;
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

        public PlayerData()
        {
            UpgradableItemList = new List<string>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }
    }
}