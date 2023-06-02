using System;
using Crypto_Mechanics;
using TMPro;
using UnityEngine;

public class CryptoCurrencyScript : MonoBehaviour, ICryptoCurrency
{
<<<<<<< HEAD
    [SerializeField] public PlayerData _data;
    [SerializeField] public TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;
=======
    [SerializeField] public PlayerData data;
    [SerializeField] public TextMeshProUGUI textTotalCurrencyCnt;
    [SerializeField] public TextMeshProUGUI textPassive;
    [SerializeField] public TextMeshProUGUI textCurrencyCntPerClick;
    private const string SavedDataPath = "Assets/Resources/savedData.json";
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)

    public bool IsInGame;
    private const double PassiveIncomeCoefficient = 0.3;

    public void BuyOrUpgrade(Item item)
    {
<<<<<<< HEAD
        item.BuyOrUpgrade(_data);
=======
        item.BuyOrUpgrade(data);
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
        Start();
    }

    private void Start()
    {
<<<<<<< HEAD
        textTotalCurrencyCnt.text = $"{Math.Round(_data.TotalCurrencyCnt, 1)} D";
        textPassive.text = $"{_data.TotalIncomes.Passive} D/s";
        textCurrencyCntPerClick.text = $"{_data.TotalIncomes.Active} D";
=======
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
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
    }

    public void Tap()
    {
<<<<<<< HEAD
        _data.TotalCurrencyCnt += _data.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(_data.TotalCurrencyCnt, 1)} D";
    } 

    public void AddPassiveIncome()
    {
        _data.TotalCurrencyCnt +=
            IsInGame ? _data.TotalIncomes.Passive : _data.TotalIncomes.Passive * PassiveIncomeCoefficient;
=======
        data.TotalCurrencyCnt += data.TotalIncomes.Active;
        textTotalCurrencyCnt.text = $"{Math.Round(data.TotalCurrencyCnt, 1)} D";
    }

    public void AddPassiveIncome()
    {
        data.TotalCurrencyCnt +=
            IsInGame ? data.TotalIncomes.Passive : data.TotalIncomes.Passive * PassiveIncomeCoefficient;
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
    }

    public void BuyTask(Task task)
    {
<<<<<<< HEAD
        task.Buy(_data);
        Start();
    }
}
=======
        task.Buy(data);
        Start();
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
        
        data.Init(newData);
    }
 }
>>>>>>> parent of 5ef249d (Merge pull request #16 from RomanGleizer/Save-System-Branch)
