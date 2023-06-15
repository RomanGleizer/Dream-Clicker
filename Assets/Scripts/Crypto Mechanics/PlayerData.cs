using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Crypto_Mechanics.Serialization;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] public string lastOnlineTime;
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

        private void Start()
        {
            if (!File.Exists(Application.dataPath + "/Last Visit Data.json")) return;

            lastOnlineTime = DateTime
                .Parse(File.ReadAllText(Application.dataPath + "/Last Visit Data.json"))
                .ToString(CultureInfo.CurrentCulture);
        }

        private void Update()
        {
            File.WriteAllText(
                Application.dataPath + "/Last Visit Data.json",
                DateTime.Now.ToString(CultureInfo.CurrentCulture));
        }

        public void Init(SerializablePlayerData playerData)
        {
            if (playerData is null) return;

            PlayerName = playerData.Name;
            TotalCurrencyCnt = playerData.TotalCurrencyCnt;
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
                {
                    buttons[i].Level = lst[i].Level;
                    buttons[i].Income = lst[i].Income;
                    buttons[i].Price = lst[i].Price;
                }
        }

        private void InitilizeOneTimeItemList(OneTimeItem[] buttons, List<OneTimeItem> lst)
        {
            for (int i = 0; i < OneTimeItems.Count; i++)
                if (buttons[i] != null)
                    buttons[i].Price = lst[i].Price;
        }

        private void InitilizeTaskList(Task[] buttons, List<Task> lst)
        {
            for (int i = 0; i < Tasks.Count; i++)
                if (buttons[i] != null)
                    buttons[i].Cost = lst[i].Cost;
        }
    }
}