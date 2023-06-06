using System;
using System.Collections.Generic;
using System.IO;
using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    private const string SavedDataPath = "Assets/Resources/savedData.json";

    [SerializeField] public UpgradableItem[] ActiveButtons;
    [SerializeField] public UpgradableItem[] PassiveButtons;
    [SerializeField] public OneTimeItem[] OneTimeButtons;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] private TextMeshProUGUI textPassive;
    [SerializeField] private TextMeshProUGUI textCurrencyCntPerClick;

    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(playerData);
        Start();
    }

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        InvokeRepeating("GetPassiveIncome", 10f, 10f);

        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{playerData.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{playerData.TotalIncomes.Active} D";
    }

    public void Update()
    {
        SaveUpgradableItemListData(playerData.UpgradableActiveItemList, ActiveButtons);
        SaveUpgradableItemListData(playerData.UpgradablePassiveItemList, PassiveButtons);
        SaveUpgradableItemListData(playerData.OneTimeItems, OneTimeButtons);
    }

    public void Tap()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }

    public void AddPassiveIncome()
    {
        playerData.TotalCurrencyCnt +=
            IsInGame ? playerData.TotalIncomes.Passive : playerData.TotalIncomes.Passive * PassiveIncomeCoefficient;
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

    public void SaveUpgradableItemListData(List<UpgradableItem> lst, UpgradableItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            InitilizeUpgradableItemList(lst, i, buttons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
    }

    public void SaveUpgradableItemListData(List<OneTimeItem> lst, OneTimeItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            InitilizeUpgradableItemList(lst, i, buttons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
    }

    public void InitilizeUpgradableItemList(List<UpgradableItem> lst, int i, UpgradableItem[] buttons)
    {
        if (buttons[i] != null)
        {
            lst[i].Level = buttons[i].Level;
            lst[i].Income = buttons[i].Income;
            lst[i].Price = buttons[i].Price;
        }
    }

    public void InitilizeUpgradableItemList(List<OneTimeItem> lst, int i, OneTimeItem[] buttons)
    {
        if (buttons[i] != null)
        {
            lst[i].Price = buttons[i].Price;
            lst[i].Name = buttons[i].Name;
        }
    }

    private void GetPassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }
}