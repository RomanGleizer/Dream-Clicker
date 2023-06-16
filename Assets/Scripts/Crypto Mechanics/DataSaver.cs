using Crypto_Mechanics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] public UpgradableItem[] ActiveButtons;
    [SerializeField] public UpgradableItem[] PassiveButtons;
    [SerializeField] public OneTimeItem[] OneTimeButtons;
    [SerializeField] public Task[] Tasks;

    public void Update() 
        => SaveGameData();

    public void SaveGameData()
    {
        SaveUpgradableItemListData(playerData.UpgradableActiveItemList, ActiveButtons);
        File.WriteAllText(Application.dataPath + "/Actives.json",
            JsonUtility.ToJson(new SavedActives(playerData)));

        SaveUpgradableItemListData(playerData.UpgradablePassiveItemList, PassiveButtons);
        File.WriteAllText(Application.dataPath + "/Passives.json",
            JsonUtility.ToJson(new SavedPassives(playerData)));

        SaveOneItemListData(playerData.OneTimeItems, OneTimeButtons);
        File.WriteAllText(Application.dataPath + "/OneTimeItems.json",
            JsonUtility.ToJson(new SavedOneTimeItems(playerData)));

        SaveTaskListData(playerData.Tasks, Tasks);
        File.WriteAllText(Application.dataPath + "/Tasks.json",
            JsonUtility.ToJson(new SavedTasks(playerData)));

        File.WriteAllText(Application.dataPath + "/Balance.json",
            JsonUtility.ToJson(new SavedBalance(playerData)));
    }

    private void SaveUpgradableItemListData(List<UpgradableItem> lst, UpgradableItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null)
            {
                lst[i].Level = buttons[i].Level;
                lst[i].Income = buttons[i].Income;
                lst[i].Price = buttons[i].Price;
            }
    }

    private void SaveOneItemListData(List<OneTimeItem> lst, OneTimeItem[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null) lst[i].Price = buttons[i].Price;
    }

    private void SaveTaskListData(List<Task> lst, Task[] buttons)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null) lst[i].Cost = buttons[i].Cost;
    }
}
