using System;
using System.Collections.Generic;
using System.Linq;
using Crypto_Mechanics.Items;

namespace Crypto_Mechanics.Serialization
{
    [Serializable]
    public class SerializablePlayerData
    {
        public string name;
        public double totalCurrencyCnt;
        public List<SerializableUpItem> serializableUpItems;
        public List<Task> tasks;
        public TotalIncomes totalIncomes;

        public SerializablePlayerData(PlayerData data)
        {
            name = data.PlayerName;
            totalCurrencyCnt = data.TotalCurrencyCnt;
            serializableUpItems = data.UpgradableItemList.Select(item => new SerializableUpItem(item)).ToList();
            tasks = data.Tasks;
            totalIncomes = data.TotalIncomes;
        }
    }
}