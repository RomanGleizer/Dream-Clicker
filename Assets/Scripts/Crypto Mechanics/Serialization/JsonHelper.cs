using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Crypto_Mechanics.Serialization
{
    public class JsonHelper
    {
        public string Serialize(SerializablePlayerData data, bool prettyPrint)
        {
            var upItemsJson = SerializeList(data.upgradableItemList, prettyPrint);
            var tasksJson = SerializeList(data.tasks, prettyPrint);

            if (prettyPrint)
                return $"{{\n\"Name\": \"{data.name}\",\n\"TotalCurrencyCnt\": {data.totalCurrencyCnt},\n\"UpgradableItemList\": [\n{upItemsJson}\n],\n\"Tasks\": [{tasksJson}]\n}}";
            return $"{{\"Name\": \"{data.name}\",\"TotalCurrencyCnt\": {data.totalCurrencyCnt},\"UpgradableItemList\": [{upItemsJson}],\"Tasks\": [{tasksJson}]}}";
        }
        

        private string SerializeList<T>(IReadOnlyList<T> list, bool prettyPrint)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i + 1 != list.Count)
                    stringBuilder.Append(JsonUtility.ToJson(list[i], prettyPrint) + ",");
                else
                    stringBuilder.Append(JsonUtility.ToJson(list[i], prettyPrint));
                if (prettyPrint) stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}