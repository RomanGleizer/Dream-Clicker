using Crypto_Mechanics;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    public UpgradableItem[] ActiveButtons;
    public UpgradableItem[] PassiveButtons;
    public OneTimeItem[] OneTimeButtons;
    public Task[] Tasks;

    public void Update() 
        => SaveMoneyData();

    public void SaveMoneyData() 
        => File.WriteAllText(Application.dataPath + "/Balance.json",
            JsonUtility.ToJson(new SavedBalance(playerData)));

    public void SaveUpgradableItemListData<T>(
        List<UpgradableItem> lst,
        UpgradableItem[] buttons,
        string path,
        T savedData)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null)
            {
                lst[i].Level = buttons[i].Level;
                lst[i].Income = buttons[i].Income;
                lst[i].Price = buttons[i].Price;
            }

        File.WriteAllText(path, JsonUtility.ToJson(savedData));
    }

    public void SaveOneItemListData<T>(
        List<OneTimeItem> lst, 
        OneTimeItem[] buttons, 
        string path,
        T savedData)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null)
            {
                lst[i].Price = buttons[i].Price;
                lst[i].NumberInParent = buttons[i].NumberInParent;
            }
        File.WriteAllText(path, JsonUtility.ToJson(savedData));
    }

    public void SaveTaskListData<T>(
        List<Task> lst, 
        Task[] buttons, 
        string path,
        T savedData)
    {
        for (int i = 0; i < lst.Count; i++)
            if (buttons[i] != null)
            {
                lst[i].Cost = buttons[i].Cost;
                lst[i].IsTaskBuy = buttons[i].IsTaskBuy;
                lst[i].PlaceInParent = buttons[i].PlaceInParent;
            }
        File.WriteAllText(path, JsonUtility.ToJson(savedData));
    }
}
