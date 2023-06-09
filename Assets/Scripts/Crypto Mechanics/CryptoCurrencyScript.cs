using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    private const string SavedDataPath = "Assets/Resources/savedData.json";

    [SerializeField] public UpItem[] ActiveButtons;
    [SerializeField] public UpItem[] PassiveButtons;
    [SerializeField] public OneTimeItem[] OneTimeButtons;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] private TextMeshProUGUI textPassive;
    [SerializeField] private TextMeshProUGUI textCurrencyCntPerClick;

    private const double OfflinePassiveIncomeCf = 0.1;
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
    
    private void LoadData()
    {
        var json = File.ReadAllText(SavedDataPath);
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (newData != null && playerData != null) playerData.Init(newData);
    }

    private void Start()
    {
        InvokeRepeating(nameof(AddOnlinePassiveIncome), 0f, PassiveIncomeRepeatRate);

        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{playerData.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{playerData.TotalIncomes.Active} D";
    }

    public void Update()
    {
        playerData.lastOnlineTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        SaveUpgradableItemListData(playerData.UpgradableActiveItemList, ActiveButtons);
        SaveUpgradableItemListData(playerData.UpgradablePassiveItemList, PassiveButtons);
        SaveOneTimeItemListData(playerData.OneTimeItems, OneTimeButtons);
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }

    public void BuyTask(Task task)
    {
        task.Buy(playerData);
        Start();
    }

    private void SaveUpgradableItemListData(IReadOnlyList<UpItem> lst, IReadOnlyList<UpItem> buttons)
    {
        InitializeUpgradableItemList(lst, buttons);
        
        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        print(json);
        File.WriteAllText(SavedDataPath, json);
    }

    private void SaveOneTimeItemListData(IReadOnlyList<OneTimeItem> lst, IReadOnlyList<OneTimeItem> buttons)
    {
        InitializeOneTimeItemList(lst, buttons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
    }

    private void InitializeUpgradableItemList(IReadOnlyList<UpItem> items, IReadOnlyList<UpItem> buttons)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (buttons[i] is null) return;
            items[i].Level = buttons[i].Level;
            items[i].Income = buttons[i].Income;
            items[i].Price = buttons[i].Price;
        }
    }

    private void InitializeOneTimeItemList(IReadOnlyList<OneTimeItem> items, IReadOnlyList<OneTimeItem> buttons)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (buttons[i] is not null)
                items[i].Price = buttons[i].Price;
        }
    }

    public void AddOnlinePassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }

    private void AddOfflinePassiveIncome()
    {
        var delta = DateTime.Now - DateTime.Parse(playerData.lastOnlineTime);
        var income = playerData.TotalIncomes.Passive * delta.TotalSeconds / PassiveIncomeRepeatRate * OfflinePassiveIncomeCf;
        playerData.TotalCurrencyCnt += income;
    }
}