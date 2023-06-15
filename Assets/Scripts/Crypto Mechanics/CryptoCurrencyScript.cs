using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using TMPro;
using UnityEngine;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    [SerializeField] public UpgradableItem[] ActiveButtons;
    [SerializeField] public UpgradableItem[] PassiveButtons;
    [SerializeField] public OneTimeItem[] OneTimeButtons;
    [SerializeField] public Task[] Tasks;
    [SerializeField] private PlayerData playerData;
    [SerializeField] public TextMeshProUGUI TextTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;
    [SerializeField] public PurshareManager purshareManager;

    public bool IsInGame;
    private const double OfflinePassiveIncomeCf = 0.1;
    private const double OnlineassiveIncomeCf = 0.3;
    private const float PassiveIncomeRepeatRate = 10;

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(playerData);
        Start();
    }

    private void Awake()
    {
        InvokeRepeating(nameof(AddOfflinePassiveIncome), 0f, PassiveIncomeRepeatRate);
        InvokeRepeating(nameof(AddOnlinePassiveIncome), 0f, PassiveIncomeRepeatRate);
    }

    private void Start()
    {
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
        textPassive.text = $"{(playerData.TotalIncomes.Passive > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Passive.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Passive).ToString().Length - 1}" : playerData.TotalIncomes.Passive)} D/s";
        textCurrencyCntPerClick.text = $"{(playerData.TotalIncomes.Active > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Active.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Active).ToString().Length - 1}" : playerData.TotalIncomes.Active)} D";
    }

    public void Update()
    {
        SaveData();
        Start();
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    public void BuyTask(Task task)
    {
        task.Buy(playerData);
        Start();
    }

    public void LoadData()
    {
    }

    public void SaveUpgradableItemListData(List<UpgradableItem> lst, UpgradableItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null)
            {
                lst[i].Level = buttons[i].Level;
                lst[i].Income = buttons[i].Income;
                lst[i].Price = buttons[i].Price;
            }
    }

    public void SaveOneItemListData(List<OneTimeItem> lst, OneTimeItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null) lst[i].Price = buttons[i].Price;
    }

    public void SaveTaskListData(List<Task> lst, Task[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null) lst[i].Cost = buttons[i].Cost;
    }

    public void AddOnlinePassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
    }

    private void AddOfflinePassiveIncome()
    {
        if (!File.Exists(Application.dataPath + "/Last Visit Data.json")) return;

        var delta = DateTime.Now - DateTime.Parse(File.ReadAllText(Application.dataPath + "/Last Visit Data.json"));
        var income = playerData.TotalIncomes.Passive * (delta.TotalSeconds / PassiveIncomeRepeatRate) * OfflinePassiveIncomeCf;
        playerData.TotalCurrencyCnt += income;
    }

    public void SaveData()
    {
        SaveUpgradableItemListData(playerData.UpgradableActiveItemList, ActiveButtons);
        File.WriteAllText(Application.dataPath + "/Actives.json",
            JsonUtility.ToJson(new SavedActives(playerData)));

        SaveUpgradableItemListData(playerData.UpgradablePassiveItemList, PassiveButtons);
        File.WriteAllText(Application.dataPath + "/Passives.json",
            JsonUtility.ToJson(new SavedPassives(playerData)));

        SaveOneItemListData(playerData.OneTimeItems, OneTimeButtons);
        File.WriteAllText(Application.dataPath + "/One Time Items.json",
            JsonUtility.ToJson(new SavedOneTimeItems(playerData)));

        SaveTaskListData(playerData.Tasks, Tasks);
        File.WriteAllText(Application.dataPath + "/Tasks.json",
            JsonUtility.ToJson(new SavedTasks(playerData)));

        File.WriteAllText(Application.dataPath + "/Balance.json",
            JsonUtility.ToJson(new SavedBalance(playerData)));

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(Application.dataPath + "/savedData.json", json);
    }
}