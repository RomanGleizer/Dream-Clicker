using Crypto_Mechanics;
using System;
using System.IO;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private CryptoCurrencyScript currencyScript;
    [SerializeField] private DataSaver dataSaver;
    [SerializeField] private PlayerData data;
    [SerializeField] private PurshareManager purshareManager;
    private string _activesDataPath = null;
    private string _passivesDataPath = null;
    private string _oneTimeItemsDataPath = null;
    private string _taskDataPath = null;
    private string _balanceDataPath = null;

    private Action[] saveOperations;

    private void Awake()
    {
        saveOperations = new Action[5]
        {
            LoadActives,
            LoadPassives,
            LoadOneTimeItems,
            LoadTasks,
            LoadBalance
        };

        _activesDataPath = Application.persistentDataPath + "/Actives.json";
        _passivesDataPath = Application.persistentDataPath + "/Passives.json";
        _oneTimeItemsDataPath = Application.persistentDataPath + "/OneTimeItems.json";
        _taskDataPath = Application.persistentDataPath + "/Tasks.json";
        _balanceDataPath = Application.persistentDataPath + "/Balance.json";
    }

    private void Start()
    {
        if (File.Exists(_activesDataPath)) LoadActives();
        if (File.Exists(_passivesDataPath)) LoadPassives();
        if (File.Exists(_oneTimeItemsDataPath)) LoadOneTimeItems();
        if (File.Exists(_taskDataPath)) LoadTasks();
        if (File.Exists(_balanceDataPath)) LoadBalance();

        if (!File.Exists(_activesDataPath)
            || !File.Exists(_passivesDataPath)
            || !File.Exists(_oneTimeItemsDataPath)
            || !File.Exists(_taskDataPath)
            || !File.Exists(_balanceDataPath))
        {
            SaveAllData();
            foreach (var operation in saveOperations)
                operation();
        }
    }

    public void LoadActives()
    {
        var activesData = GetData<SavedActives>(_activesDataPath);

        for (int i = 0; i < activesData.SerializableUpActiveItems.Count; i++)
        {
            var item = activesData.SerializableUpActiveItems[i];
            if (purshareManager.IsUpItemWasBought(item))
                dataSaver.ActiveButtons[i].InitializeTextes(activesData.SerializableUpActiveItems);
        }
    }

    public void LoadPassives()
    {
        var passivesData = GetData<SavedPassives>(_passivesDataPath);

        for (int i = 0; i < passivesData.SerializableUpPassiveItems.Count; i++)
        {
            var item = passivesData.SerializableUpPassiveItems[i];
            if (purshareManager.IsUpItemWasBought(item))
                dataSaver.PassiveButtons[i].InitializeTextes(passivesData.SerializableUpPassiveItems);
        }
    }

    public void LoadOneTimeItems()
    {
        var data = GetData<SavedOneTimeItems>(_oneTimeItemsDataPath);

        for (int i = 0; i < data.SerializableOneTimeItems.Count; i++)
        {
            var item = data.SerializableOneTimeItems[i];
            if (purshareManager.IsOneTimeItemWasBought(item))
                dataSaver.OneTimeButtons[i].InitializeTextes(data);
        }
    }

    public void LoadTasks()
    {
        var tasksData = GetData<SavedTasks>(_taskDataPath);

        for (int i = 0; i < tasksData.Tasks.Count; i++)
        {
            var item = tasksData.Tasks[i];
            if (purshareManager.IsTaskWasBought(item))
                data.Tasks[i].InitializeTextes(tasksData);
        }
    }

    public void LoadBalance()
    {
        if (!File.Exists(_balanceDataPath)) return;

        var balance = GetData<SavedBalance>(_balanceDataPath);
        data.TotalCurrencyCnt = balance.TotalCurrencyCnt;
        data.TotalIncomes.Active = balance.totalIncomes.Active;
        data.TotalIncomes.Passive = balance.totalIncomes.Passive;

        currencyScript.TextTotalCurrencyCnt.text = $"{(data.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(data.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}?{Math.Round(data.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(data.TotalCurrencyCnt, 1))} D";
        currencyScript.TextCurrencyCntPerClick.text = $"{(data.TotalIncomes.Active > 10000 ? $"{Math.Round(double.Parse(data.TotalIncomes.Active.ToString().Substring(0, 3)) / 100, 2)}?{Math.Round(data.TotalIncomes.Active).ToString().Length - 1}" : data.TotalIncomes.Active)} D";
        currencyScript.TextPassive.text = $"{(data.TotalIncomes.Passive > 10000 ? $"{Math.Round(double.Parse(data.TotalIncomes.Passive.ToString().Substring(0, 3)) / 100, 2)}?{Math.Round(data.TotalIncomes.Passive).ToString().Length - 1}" : data.TotalIncomes.Passive)} D/s";
    }

    private void SaveAllData()
    {
        dataSaver.SaveUpgradableItemListData(
            data.UpgradableActiveItemList,
            dataSaver.ActiveButtons,
            _activesDataPath,
            new SavedPassives(data));

        dataSaver.SaveUpgradableItemListData(
            data.UpgradablePassiveItemList,
            dataSaver.PassiveButtons,
            _passivesDataPath,
            new SavedPassives(data));

        dataSaver.SaveOneItemListData(
            data.OneTimeItems,
            dataSaver.OneTimeButtons,
            _oneTimeItemsDataPath,
            new SavedOneTimeItems(data));

        dataSaver.SaveTaskListData(
            data.Tasks,
            dataSaver.Tasks,
            _taskDataPath,
            new SavedTasks(data));
    }

    private T GetData<T>(string path)
        => JsonUtility.FromJson<T>(File.ReadAllText(path));
}