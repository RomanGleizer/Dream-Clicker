﻿using System;
using System.Collections.Generic;
using System.Linq;
using Crypto_Mechanics.Items;

namespace Crypto_Mechanics.Serialization
{
    [Serializable]
    public class SerializablePlayerData
    {
        public string Name;
        public double TotalCurrencyCnt;
        public List<SerializableUpActiveItem> SerializableUpActiveItems;
        public List<SerializableUpPassiveItem> SerializableUpPassiveItems;
        public List<SerializableOneTimeUpgradableItem> SerializableOneTimeItems;
        public List<SerializableTask> Tasks;
        public TotalIncomes totalIncomes;

        public SerializablePlayerData(PlayerData data)
        {
            Name = data.PlayerName;
            TotalCurrencyCnt = data.TotalCurrencyCnt;
            SerializableUpActiveItems = data.UpgradableActiveItemList.Select(item => new SerializableUpActiveItem(item)).ToList();
            SerializableUpPassiveItems = data.UpgradablePassiveItemList.Select(item => new SerializableUpPassiveItem(item)).ToList();
            SerializableOneTimeItems = data.OneTimeItems.Select(item => new SerializableOneTimeUpgradableItem(item)).ToList();
            Tasks = data.Tasks.Select(item => new SerializableTask(item)).ToList();
            totalIncomes = data.TotalIncomes;
        }
    }
}