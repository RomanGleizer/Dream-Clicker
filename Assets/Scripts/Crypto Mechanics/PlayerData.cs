using System;
using System.Collections.Generic;
using System.Linq;
using Crypto_Mechanics.Serialization;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] public string PlayerName;
        [SerializeField] public double TotalCurrencyCnt;
        public List<UpgradableItem> UpgradableItemList;
        public List<Task> Tasks;
        public TotalIncomes TotalIncomes;

        public PlayerData()
        {
            UpgradableItemList = new List<UpgradableItem>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }

        public PlayerData Init(SerializablePlayerData playerData)
        {
            PlayerName = playerData.name;
            TotalCurrencyCnt = playerData.totalCurrencyCnt;
            UpgradableItemList = playerData.upgradableItemList.Select(item =>
            {
                var upItemObject = new GameObject(playerData.name);
                var playerDataComponent = upItemObject.AddComponent<UpgradableItem>();
                return playerDataComponent.Init(item);
            }).ToList();
            Tasks = playerData.tasks;
            TotalIncomes = playerData.totalIncomes;
            return this;
        }
    }
}