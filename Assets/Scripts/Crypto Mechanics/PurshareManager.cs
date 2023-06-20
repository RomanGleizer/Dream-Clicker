using Crypto_Mechanics;
using System;
using System.IO;
using UnityEngine;

public class PurshareManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CryptoCurrencyScript currencyScript;
    [SerializeField] private DataSaver dataSaver;
    private string _activesDataPath = null;
    private string _passivesDataPath = null;
    private string _oneTimeItemsDataPath = null;
    private string _taskDataPath = null;

    public Predicate<SerializableUpItem> IsUpItemWasBought = 
        x => x.Level > 0;
    public Predicate<SerializableOneTimeUpgradableItem> IsOneTimeItemWasBought =
        x => x.IsItemWasBought == true;
    public Predicate<SerializableTask> IsTaskWasBought =
        x => x.IsTaskWasBuy == true;

    private void Awake()
    {
        _activesDataPath = Application.persistentDataPath + "/Actives.json";
        _passivesDataPath = Application.persistentDataPath + "/Passives.json";
        _oneTimeItemsDataPath = Application.persistentDataPath + "/OneTimeItems.json";
        _taskDataPath = Application.persistentDataPath + "/Tasks.json";
    }

    public void BuyUpgradableItem(UpgradableItem item)
    {
        if (playerData.TotalCurrencyCnt < item.Price || item.Level == 25) return;

        for (int i = 0; i < item.items.Length; i++)
        {
            if (item.items[i].Level > 0) item.IsPossibleToBuy = true;
            else
            {
                item.IsPossibleToBuy = false;
                break;
            }
        }

        var checkedText = item.Task.Text.text;
        if ((checkedText == "Приобретено" && item.IsPossibleToBuy)
            || ((checkedText == "Приобретено" || checkedText == "Пусто") && item.items.Length == 0))
        {
            var previousIncome = item.Income;
            item.Income = Math.Round(item.Income * item.UpgradeIncomeCoefficient, 1);
            var deltaIncome = item.Income - previousIncome;

            if (item.Level == 0) deltaIncome = item.Income;
            if (item.Type == IncomeType.Active) playerData.TotalIncomes.Active += deltaIncome;
            else if (item.Type == IncomeType.Passive) playerData.TotalIncomes.Passive += deltaIncome;
            playerData.TotalCurrencyCnt -= item.Price;

            currencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            currencyScript.TextCurrencyCntPerClick.text = $"{(playerData.TotalIncomes.Active > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Active.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Active).ToString().Length - 1}" : playerData.TotalIncomes.Active)} D";
            currencyScript.TextPassive.text = $"{(playerData.TotalIncomes.Passive > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Passive.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Passive).ToString().Length - 1}" : playerData.TotalIncomes.Passive)} D/s";

            item.Level++;
            item.Price = Math.Round(item.Price * item.UpgradeCostCoefficient, 1);
            item.SetTextes(item.Level, item.Income, item.Price);

            if (item.Type == IncomeType.Active)
            {
                dataSaver.SaveUpgradableItemListData(
                    playerData.UpgradableActiveItemList, 
                    dataSaver.ActiveButtons, 
                    _activesDataPath, 
                    new SavedActives(playerData));
            }
            else if (item.Type == IncomeType.Passive)
            {
                dataSaver.SaveUpgradableItemListData(
                    playerData.UpgradablePassiveItemList,
                    dataSaver.PassiveButtons,
                    _passivesDataPath,
                    new SavedPassives(playerData));
            }
        }
    }

    public void BuyOneTimeItem(OneTimeItem oneTimeItem)
    {
        for (int i = 0; i < oneTimeItem.items.Length; i++)
        {
            if (oneTimeItem.items[i].IsItemWasBought) oneTimeItem.IsPossibleToBuy = true;
            else
            {
                oneTimeItem.IsPossibleToBuy = false;
                break;
            }
        }

        if (playerData.TotalCurrencyCnt < oneTimeItem.Price)
            return;
        
        if (playerData.TotalCurrencyCnt >= oneTimeItem.Price
            && oneTimeItem.task.IsTaskBuy
            && (oneTimeItem.IsPossibleToBuy || oneTimeItem.items.Length == 0))
        {
            playerData.TotalCurrencyCnt -= oneTimeItem.Price;
            currencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            oneTimeItem.Text.text = "Приобретено";
            oneTimeItem.IsItemWasBought = true;
            dataSaver.SaveOneItemListData(
                playerData.OneTimeItems, 
                dataSaver.OneTimeButtons, 
                _oneTimeItemsDataPath, 
                new SavedOneTimeItems(playerData));
        }
    }

    public void BuyTask(Task task)
    {
        if (playerData.TotalCurrencyCnt < task.Cost) return;

        for (int i = 0; i < task.Items.Count; i++)
        {
            if (task.Items[i].Level > 0) task.IsPossibleToBuy = true;
            else
            {
                task.IsPossibleToBuy = false;
                break;
            }
        }
        for (int i = 0; i < task.OneTimeItems.Count; i++)
        {
            if (task.OneTimeItems[i].IsItemWasBought) task.IsPossibleToBuy = true;
            else
            {
                task.IsPossibleToBuy = false;
                break;
            }
        }

        if ((task.IsPossibleToBuy && task.Text.text != "Приобретено")
            || (task.PlaceInParent == 4 && task.Data.Tasks[2].Text.text == "Приобретено")
            || (task.PlaceInParent == 13 && task.Data.Tasks[11].Text.text == "Приобретено")
            )
        {
            task.Data.TotalCurrencyCnt -= task.Cost;
            task.Data.TotalCurrencyCnt += task.SingleBonus;
            task.Data.CurrencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            task.IsTaskBuy = true;
            task.Text.text = "Приобретено";
            dataSaver.SaveTaskListData(
                playerData.Tasks, 
                dataSaver.Tasks, 
                _taskDataPath, 
                new SavedTasks(playerData));
        }
    }
}
