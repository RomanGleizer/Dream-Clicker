using System.Collections.Generic;
using System.Linq;
using Crypto_Mechanics.Serialization;
using UnityEngine;

namespace Crypto_Mechanics
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private CryptoCurrencyScript currencyScript;
        [SerializeField] public string PlayerName;
        [SerializeField] public double TotalCurrencyCnt;
        [SerializeField] public List<UpgradableItem> UpgradableActiveItemList;
        [SerializeField] public List<UpgradableItem> UpgradablePassiveItemList;
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
                InitilizeUpgradableActiveItemList(i, currencyScript.ActiveButtons);

            for (int i = 0; i < UpgradablePassiveItemList.Count; i++)
                InitilizeUpgradablePassiveItemList(i, currencyScript.PassiveButtons);

            //UpgradableItemList = playerData.serializableUpItems.Select(item =>
            //{
            //    var upItemObject = new GameObject("UpItemObject");
            //    var playerDataComponent = upItemObject.AddComponent<UpgradableItem>();
            //    playerDataComponent.Init(item);
            //    return playerDataComponent;
            //}).ToList();
        }

        private void InitilizeUpgradableActiveItemList(int i, UpgradableItem[] buttons)
        {
            if (buttons[i] != null)
            {
                buttons[i].Level = UpgradableActiveItemList[i].Level;
                buttons[i].Income = UpgradableActiveItemList[i].Income;
                buttons[i].Price = UpgradableActiveItemList[i].Price;
            }
        }

        private void InitilizeUpgradablePassiveItemList(int i, UpgradableItem[] buttons)
        {
            if (buttons[i] != null)
            {
                buttons[i].Level = UpgradablePassiveItemList[i].Level;
                buttons[i].Income = UpgradablePassiveItemList[i].Income;
                buttons[i].Price = UpgradablePassiveItemList[i].Price;
            }
        }
    }
}