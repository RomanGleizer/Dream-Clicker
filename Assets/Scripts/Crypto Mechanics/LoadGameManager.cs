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
    [SerializeField] private UpgradableItem[] actives;
    [SerializeField] private UpgradableItem[] passives;
    [SerializeField] private OneTimeItem[] oneTimeItems;
    [SerializeField] private Task[] tasks;

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

        if (File.Exists(Application.dataPath + "/Actives.json")) LoadActives();
        if (File.Exists(Application.dataPath + "/Passives.json")) LoadPassives();
        if (File.Exists(Application.dataPath + "/OneTimeItems.json")) LoadOneTimeItems();
        if (File.Exists(Application.dataPath + "/Tasks.json")) LoadTasks();
        if (File.Exists(Application.dataPath + "/Balance.json")) LoadBalance();

        if (File.Exists(Application.dataPath + "/Actives.json")
            || File.Exists(Application.dataPath + "/Passives.json")
            || File.Exists(Application.dataPath + "/OneTimeItems.json")
            || File.Exists(Application.dataPath + "/Tasks.json")
            || File.Exists(Application.dataPath + "/Balance.json"))
        {
            dataSaver.SaveGameData();
            foreach (var operation in saveOperations)
                operation();
        }
    }

    public void LoadActives()
    {
        var activesData = GetData<SavedActives>(Application.dataPath + "/Actives.json");

        for (int i = 0; i < activesData.SerializableUpActiveItems.Count; i++)
        {
            var item = activesData.SerializableUpActiveItems[i];
            if (purshareManager.IsUpItemWasBought(item))
                actives[i].InitializeTextes(activesData.SerializableUpActiveItems);
        }
    }

    public void LoadPassives()
    {
        var passivesData = GetData<SavedPassives>(Application.dataPath + "/Passives.json");

        for (int i = 0; i < passivesData.SerializableUpPassiveItems.Count; i++)
        {
            var item = passivesData.SerializableUpPassiveItems[i];
            if (purshareManager.IsUpItemWasBought(item))
                passives[i].InitializeTextes(passivesData.SerializableUpPassiveItems);
        }
    }

    public void LoadOneTimeItems()
    {
        var data = GetData<SavedOneTimeItems>(Application.dataPath + "/OneTimeItems.json");

        for (int i = 0; i < data.SerializableOneTimeItems.Count; i++)
        {
            var item = data.SerializableOneTimeItems[i];
            if (purshareManager.IsOneTimeItemWasBought(item))
                oneTimeItems[i].InitializeTextes(data);
        }
    }

    public void LoadTasks()
    {
        var tasksData = GetData<SavedTasks>(Application.dataPath + "/Tasks.json");

        for (int i = 0; i < tasksData.Tasks.Count; i++)
        {
            var item = tasksData.Tasks[i];
            if (purshareManager.IsTaskWasBought(item))
                tasks[i].InitializeTextes(tasksData);
        }
    }

    public void LoadBalance()
    {
        var balance = GetData<SavedBalance>(Application.dataPath + "/Balance.json");
        data.TotalCurrencyCnt = balance.TotalCurrencyCnt;
        data.TotalIncomes.Active = balance.totalIncomes.Active;
        data.TotalIncomes.Passive = balance.totalIncomes.Passive;

        currencyScript.TextTotalCurrencyCnt.text = $"{(data.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(data.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Å{Math.Round(data.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(data.TotalCurrencyCnt, 1))} D";
        currencyScript.textCurrencyCntPerClick.text = $"{(data.TotalIncomes.Active > 10000 ? $"{Math.Round(double.Parse(data.TotalIncomes.Active.ToString().Substring(0, 3)) / 100, 2)}Å{Math.Round(data.TotalIncomes.Active).ToString().Length - 1}" : data.TotalIncomes.Active)} D";
        currencyScript.textPassive.text = $"{(data.TotalIncomes.Passive > 10000 ? $"{Math.Round(double.Parse(data.TotalIncomes.Passive.ToString().Substring(0, 3)) / 100, 2)}Å{Math.Round(data.TotalIncomes.Passive).ToString().Length - 1}" : data.TotalIncomes.Passive)} D/s";
    }

    private T GetData<T>(string path)
        => JsonUtility.FromJson<T>(File.ReadAllText(path));
}