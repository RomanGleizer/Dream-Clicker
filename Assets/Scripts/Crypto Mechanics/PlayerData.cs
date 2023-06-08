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
        [SerializeField] private CryptoCurrencyScript currencyScript;
        [SerializeField] public string PlayerName;
        [SerializeField] public double TotalCurrencyCnt;
        [SerializeField] public List<UpItem> UpgradableActiveItemList;
        [SerializeField] public List<UpItem> UpgradablePassiveItemList;
        [SerializeField] public List<OneTimeItem> OneTimeItems;
        [SerializeField] public List<Task> Tasks;
        [SerializeField] public TotalIncomes TotalIncomes;

        public PlayerData()
        {
            UpgradableActiveItemList = new List<UpItem>();
            UpgradablePassiveItemList = new List<UpItem>();
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
            
            InitializeUpButtonsList(currencyScript.ActiveButtons, UpgradableActiveItemList);
            InitializeUpButtonsList(currencyScript.PassiveButtons, UpgradablePassiveItemList);
            InitializeOneTimeButtonsList(currencyScript.OneTimeButtons, OneTimeItems);
        }

        private void InitializeUpButtonsList(IReadOnlyList<UpItem> buttons,
            IReadOnlyList<UpItem> lst)
        {
            for (var i = 0; i < lst.Count; i++)
            {
                if (buttons[i] == null) return;
                buttons[i].Level = lst[i].Level;
                buttons[i].Income = lst[i].Income;
                buttons[i].Price = lst[i].Price;
            }
        }

        private void InitializeOneTimeButtonsList(IReadOnlyList<OneTimeItem> buttons, IReadOnlyList<OneTimeItem> lst)
        {
            for (var i = 0; i < lst.Count; i++)
            {
                if (buttons[i] != null)
                    buttons[i].Price = lst[i].Price;
            }
        }
    }
}