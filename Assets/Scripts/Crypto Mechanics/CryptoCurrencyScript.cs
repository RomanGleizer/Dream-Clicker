using System;
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
        InvokeRepeating("GetPassiveIncome", 1f, 1f);

        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{playerData.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{playerData.TotalIncomes.Active} D";
    }

    public void Update()
    {
        SaveUpgradableActiveItemListData();
        SaveUpgradablePassiveItemListData();
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

    public void SaveUpgradableActiveItemListData()
    {
        for (int i = 0; i < playerData.UpgradableActiveItemList.Count; i++)
            InitilizeUpgradableActiveItemList(i, ActiveButtons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
        print(json);
    }

    public void SaveUpgradablePassiveItemListData()
    {
        for (int i = 0; i < playerData.UpgradablePassiveItemList.Count; i++)
            InitilizeUpgradablePassiveItemList(i, PassiveButtons);

        var json = JsonUtility.ToJson(new SerializablePlayerData(playerData));
        File.WriteAllText(SavedDataPath, json);
        print(json);
    }

    public void LoadData()
    {
        var json = File.ReadAllText(SavedDataPath);
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (newData != null && playerData != null) playerData.Init(newData);
    }

    public void InitilizeUpgradableActiveItemList(int i, UpgradableItem[] buttons)
    {
        if (buttons[i] != null)
        {
            playerData.UpgradableActiveItemList[i].Level = buttons[i].Level;
            playerData.UpgradableActiveItemList[i].Income = buttons[i].Income;
            playerData.UpgradableActiveItemList[i].Price = buttons[i].Price;
        }
    }

    public void InitilizeUpgradablePassiveItemList(int i, UpgradableItem[] buttons)
    {
        if (buttons[i] != null)
        {
            playerData.UpgradablePassiveItemList[i].Level = buttons[i].Level;
            playerData.UpgradablePassiveItemList[i].Income = buttons[i].Income;
            playerData.UpgradablePassiveItemList[i].Price = buttons[i].Price;
        }
    }

    private void GetPassiveIncome()
    {
        playerData.TotalCurrencyCnt += playerData.TotalIncomes.Passive;
        textTotalCurrencyCnt.text = $"{Math.Round(playerData.TotalCurrencyCnt, 1)} D";
    }
}