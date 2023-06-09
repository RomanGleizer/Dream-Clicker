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
    private const string SavedDataPath = "Assets/Resources/savedData.json";

    [SerializeField] public UpgradableItem[] ActiveButtons;
    [SerializeField] public UpgradableItem[] PassiveButtons;
    [SerializeField] public OneTimeItem[] OneTimeButtons;
    [SerializeField] public Task[] Tasks;
    [SerializeField] private PlayerData playerData;
    [SerializeField] public TextMeshProUGUI TextTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;

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
        LoadData();
        AddOfflinePassiveIncome();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AddOnlinePassiveIncome), 0f, PassiveIncomeRepeatRate);

        TextTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{playerData.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{playerData.TotalIncomes.Active} D";
    }

    public void Update()
    {
        playerData.lastOnlineTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        SaveUpgradableItemListData(playerData.UpgradableActiveItemList, ActiveButtons);
        SaveUpgradableItemListData(playerData.UpgradablePassiveItemList, PassiveButtons);
        SaveOneItemListData(playerData.OneTimeItems, OneTimeButtons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        TextTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }

    public void AddPassiveIncome()
    {
        playerData.TotalCurrencyCnt +=
            IsInGame 
            ? playerData.TotalIncomes.Passive * OfflinePassiveIncomeCf
            : playerData.TotalIncomes.Passive * OnlineassiveIncomeCf;
    }

    public void BuyTask(Task task)
    {
        task.Buy(playerData);
        Start();
    }

    public void LoadData()
    {
        var json = File.ReadAllText(SavedDataPath);
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (newData != null && playerData != null) playerData.Init(newData);
    }

    public void SaveUpgradableItemListData(
        List<UpgradableItem> lst, 
        UpgradableItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            InitilizeUpgradableItemList(lst, i, buttons);
    }

    public void SaveOneItemListData(
        List<OneTimeItem> lst,
        OneTimeItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            InitilizeOneItemList(lst, i, buttons);
    }

    public void SaveTaskListData(
        List<Task> lst,
        Task[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            InitilizeTaskItemList(lst, i, buttons);
    }

    public void InitilizeUpgradableItemList(
        List<UpgradableItem> lst, 
        int i, 
        UpgradableItem[] buttons)
    {
        if (buttons[i] != null)
        {
            lst[i].Level = buttons[i].Level;
            lst[i].Income = buttons[i].Income;
            lst[i].Price = buttons[i].Price;
        }
    }

    public void InitilizeOneItemList(
        List<OneTimeItem> lst, 
        int i, 
        OneTimeItem[] buttons)
    {
        if (buttons[i] != null) lst[i].Price = buttons[i].Price;
    }

    public void InitilizeTaskItemList(
        List<Task> lst,
        int i,
        Task[] buttons)
    {
        if (buttons[i] != null) lst[i].Cost = buttons[i].Cost;
    }

    public void AddOnlinePassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        TextTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }

    private void AddOfflinePassiveIncome()
    {
        var delta = DateTime.Now - DateTime.Parse(playerData.lastOnlineTime);
        var income = playerData.TotalIncomes.Passive * delta.TotalSeconds / PassiveIncomeRepeatRate * OfflinePassiveIncomeCf;
        playerData.TotalCurrencyCnt += income;
    }
}