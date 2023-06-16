using Crypto_Mechanics;
using System;
using UnityEngine;

public class PurshareManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] protected CryptoCurrencyScript currencyScript;
    
    public Predicate<SerializableUpItem> IsUpItemWasBought = 
        x => x.Level > 0;
    public Predicate<SerializableOneTimeUpgradableItem> IsOneTimeItemWasBought =
        x => x.Text == "Приобретено";
    public Predicate<SerializableTask> IsTaskWasBought =
        x => x.IsTaskWasBuy == true;

    public void BuyUpgradableItem(UpgradableItem item)
    {
        if (playerData.TotalCurrencyCnt < item.Price || item.Level == 25) return;

        for (int i = 0; i < item.items.Length; i++)
        {
            if (item.items[i].Level > 0) item.isPossibleToBuy = true;
            else
            {
                item.isPossibleToBuy = false;
                break;
            }
        }
        if ((item.task.Text.text == "Приобретено" && item.isPossibleToBuy)
            || ((item.task.Text.text == "Приобретено" || item.task.Text.text == "Пусто") 
            && item.items.Length == 0))
        {
            var deltaIncome = item.Income;
            var previousIncome = item.Income;

            item.Income = Math.Round(item.Income * 1.3, 1);
            deltaIncome = item.Income - previousIncome;

            if (item.Level == 0) deltaIncome = item.Income;
            if (item.Type == IncomeType.Active)
                playerData.TotalIncomes.Active += deltaIncome;
            else if (item.Type == IncomeType.Passive)
                playerData.TotalIncomes.Passive += deltaIncome;
            playerData.TotalCurrencyCnt -= item.Price;

            currencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            currencyScript.textCurrencyCntPerClick.text = $"{(playerData.TotalIncomes.Active > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Active.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Active).ToString().Length - 1}" : playerData.TotalIncomes.Active)} D";
            currencyScript.textPassive.text = $"{(playerData.TotalIncomes.Passive > 10000 ? $"{Math.Round(double.Parse(playerData.TotalIncomes.Passive.ToString().Substring(0, 3)) / 100, 2)}Е{Math.Round(playerData.TotalIncomes.Passive).ToString().Length - 1}" : playerData.TotalIncomes.Passive)} D/s";

            item.Level++;
            item.Price = Math.Round(item.Price * 1.3, 1);
            item.SetTextes(item.Level, item.Income, item.Price);
        }
    }

    public void BuyOneTimeItem(OneTimeItem oneTimeItem)
    {
        for (int i = 0; i < oneTimeItem.items.Length; i++)
        {
            if (oneTimeItem.items[i].Text.text == "Приобретено") oneTimeItem.isPossibleToBuy = true;
            else
            {
                oneTimeItem.isPossibleToBuy = false;
                break;
            }
        }

        if (playerData.TotalCurrencyCnt < oneTimeItem.Price && oneTimeItem.Text.text == "Приобретено")
            return;
        else if (playerData.TotalCurrencyCnt >= oneTimeItem.Price
            && oneTimeItem.task.Text.text == "Приобретено"
            && (oneTimeItem.isPossibleToBuy || oneTimeItem.items.Length == 0))
        {
            playerData.TotalCurrencyCnt -= oneTimeItem.Price;
            currencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            oneTimeItem.Text.text = "Приобретено";
        }
    }

    public void BuyTask(Task task)
    {
        for (int i = 0; i < task.items.Count; i++)
        {
            if (task.items[i].Level > 0) task.isPossibleToBuy = true;
            else
            {
                task.isPossibleToBuy = false;
                break;
            }
        }

        for (int i = 0; i < task.oneTimeItems.Count; i++)
        {
            if (task.oneTimeItems[i].Text.text == "Приобретено") task.isPossibleToBuy = true;
            else
            {
                task.isPossibleToBuy = false;
                break;
            }
        }

        if ((task.data.TotalCurrencyCnt >= task.Cost && task.isPossibleToBuy && task.Text.text != "Приобретено")
            || (task.PlaceInParent == 4 && task.data.Tasks.Count > 0 && task.data.Tasks[2].Text.text == "Приобретено")
            || (task.PlaceInParent == 13 && task.data.Tasks.Count > 0 && task.data.Tasks[11].Text.text == "Приобретено")
            )
        {
            task.data.TotalCurrencyCnt -= task.Cost;
            task.data.TotalCurrencyCnt += task.SingleBonus;
            task.data.CurrencyScript.TextTotalCurrencyCnt.text = $"{(playerData.TotalCurrencyCnt > 10000000 ? $"{Math.Round(double.Parse(playerData.TotalCurrencyCnt.ToString().Substring(0, 4)) / 1000, 3)}Е{Math.Round(playerData.TotalCurrencyCnt).ToString().Length - 1}" : Math.Round(playerData.TotalCurrencyCnt, 1))} D";
            task.IsTaskBuy = true;
            task.Text.text = "Приобретено";
        }
    }
}
