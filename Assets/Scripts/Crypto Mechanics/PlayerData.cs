using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            for (int i = 0; i < UpgradableActiveItemList.Count; i++)
                InitilizeUpgradableItemList(
                    i, 
                    CurrencyScript.ActiveButtons, 
                    UpgradableActiveItemList);

            for (int i = 0; i < UpgradablePassiveItemList.Count; i++)
                InitilizeUpgradableItemList(
                    i, 
                    CurrencyScript.PassiveButtons, 
                    UpgradablePassiveItemList);

            for (int i = 0; i < OneTimeItems.Count; i++)
                InitilizeOneTimeItemList(
                    i, 
                    CurrencyScript.OneTimeButtons, 
                    OneTimeItems);

            for (int i = 0; i < Tasks.Count; i++)
                InitilizeTaskList(
                    i, 
                    CurrencyScript.Tasks, 
                    Tasks);
        }

        private void InitilizeUpgradableItemList(
            int i, 
            UpgradableItem[] buttons, 
            List<UpgradableItem> lst)
        {
            if (buttons[i] != null)
            {
                buttons[i].Level = lst[i].Level;
                buttons[i].Income = lst[i].Income;
                buttons[i].Price = lst[i].Price;
            }
        }

        private void InitilizeOneTimeItemList(
            int i, 
            OneTimeItem[] buttons, 
            List<OneTimeItem> lst)
        {
            if (buttons[i] != null)
                buttons[i].Price = lst[i].Price;
        }

        private void InitilizeTaskList(
            int i, 
            Task[] buttons, 
            List<Task> lst)
        {
            if (buttons[i] != null)
                buttons[i].Cost = lst[i].Cost;
        }
    }
}