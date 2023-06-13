using System;
using System.Collections.Generic;
using System.Globalization;
using Crypto_Mechanics.Serialization;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] public string lastOnlineTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        [SerializeField] public CryptoCurrencyScript CurrencyScript;
        [SerializeField] public string PlayerName;
        [SerializeField] public double TotalCurrencyCnt;
        [SerializeField] public List<UpgradableItem> UpgradableActiveItemList;
        [SerializeField] public List<UpgradableItem> UpgradablePassiveItemList;
        [SerializeField] public List<OneTimeItem> OneTimeItems;
        [SerializeField] public List<Task> Tasks;
        [SerializeField] public TotalIncomes TotalIncomes;

        public PlayerData()
        {
            UpgradableActiveItemList = new List<UpgradableItem>();
            UpgradablePassiveItemList = new List<UpgradableItem>();
            OneTimeItems = new List<OneTimeItem>();
            Tasks = new List<Task>();
            TotalIncomes = new TotalIncomes();
        }

        public void Init(SerializablePlayerData playerData)
        {
            if (playerData is null) return;

            PlayerName = playerData.Name;
            TotalCurrencyCnt = playerData.TotalCurrencyCnt;
            Tasks = playerData.Tasks;
            TotalIncomes = playerData.totalIncomes;

            InitilizeUpgradableItemList(CurrencyScript.ActiveButtons, UpgradableActiveItemList);
            InitilizeUpgradableItemList(CurrencyScript.PassiveButtons, UpgradablePassiveItemList);
            InitilizeOneTimeItemList(CurrencyScript.OneTimeButtons, OneTimeItems);
            InitilizeTaskList(CurrencyScript.Tasks, Tasks);
        }

        private void InitilizeUpgradableItemList(UpgradableItem[] buttons, List<UpgradableItem> lst)
        {
            for (int i = 0; i < lst.Count; i++)
                if (buttons[i] != null)
                    buttons[i] = new UpgradableItem
                    {
                        Level = lst[i].Level,
                        Income = lst[i].Income,
                        Price = lst[i].Price
                    };
        }

        private void InitilizeOneTimeItemList(OneTimeItem[] buttons, List<OneTimeItem> lst)
        {
            for (int i = 0; i < OneTimeItems.Count; i++)
                if (buttons[i] != null)
                    buttons[i] = new OneTimeItem { Price = lst[i].Price };
        }

        private void InitilizeTaskList(Task[] buttons, List<Task> lst)
        {
            for (int i = 0; i < Tasks.Count; i++)
                if (buttons[i] != null)
                    buttons[i] = new Task { Cost = lst[i].Cost };
        }
    }
}