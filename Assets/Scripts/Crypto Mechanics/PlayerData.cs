using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Linq;
using Crypto_Mechanics.Serialization;
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
<<<<<<< HEAD
        [SerializeField] private string Name;
        [SerializeField] public double TotalCurrencyCnt;
        public List<string> UpgradableItemList;
        public List<Task> Tasks; 
=======
        [SerializeField] public string PlayerName;
        [SerializeField] public double TotalCurrencyCnt;
        public List<UpgradableItem> UpgradableItemList;
        public List<Task> Tasks;
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
        public TotalIncomes TotalIncomes;

        public PlayerData()
        {
<<<<<<< HEAD
            UpgradableItemList = new List<string>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }
=======
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
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
    }
}