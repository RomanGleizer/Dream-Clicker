using System;
using System.IO;
using Crypto_Mechanics;
using Crypto_Mechanics.Serialization;
using TMPro;
using UnityEngine;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
    [SerializeField] public PlayerData data;
    [SerializeField] public TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;
    private const string SavedDataPath = "Assets/Resources/savedData.json";

    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;

    public void BuyOrUpgrade(Item item)
    {
        item.BuyOrUpgrade(data);
        Start();
    }

    private void Start()
    {
        textTotalCurrencyCnt.text = $"{Math.Round(data.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{data.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{data.TotalIncomes.Active} D";
    }

    private void OnEnable()
    {
        LoadData();
    }
    
    private void OnDisable()
    {
        SaveData();
    }

    public void Tap()
    {
        data.TotalCurrencyCnt += data.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(data.TotalCurrencyCnt, 1)} D";
    }

    public void AddPassiveIncome()
    {
        data.TotalCurrencyCnt +=
            IsInGame ? data.TotalIncomes.Passive : data.TotalIncomes.Passive * PassiveIncomeCoefficient;
        SaveData();
    }

    public void BuyTask(Task task)
    {
        task.Buy(data);
        Start();
        SaveData();
    }

    private void SaveData()
    {
        var json = JsonUtility.ToJson(new SerializablePlayerData(data));
        File.WriteAllText(SavedDataPath, json);
        Debug.Log(json);
    }

    private void LoadData()
    {
        var json = File.ReadAllText(SavedDataPath);
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);
        
        var playerDataObject = new GameObject("PlayerDataObject");
        var playerDataComponent = playerDataObject.AddComponent<PlayerData>();
        data = playerDataComponent.Init(newData);
    }
 }