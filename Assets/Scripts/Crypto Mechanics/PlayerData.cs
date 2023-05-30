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

        public void Init(SerializablePlayerData playerData)
        {
            if (playerData is null) return;
            PlayerName = playerData.name;
            TotalCurrencyCnt = playerData.totalCurrencyCnt;
            UpgradableItemList = playerData.serializableUpItems.Select(item =>
            {
                var upItemObject = new GameObject("UpItemObject");
                var playerDataComponent = upItemObject.AddComponent<UpgradableItem>();
                playerDataComponent.Init(item);
                return playerDataComponent;
            }).ToList();
            Tasks = playerData.tasks;
            TotalIncomes = playerData.totalIncomes;
        }
    }
}