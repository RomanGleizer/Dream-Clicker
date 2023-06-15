using Crypto_Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedOneTimeItems
{
    public List<SerializableOneTimeUpgradableItem> SerializableOneTimeItems;

    public SavedOneTimeItems(PlayerData data)
    {
        SerializableOneTimeItems = data.OneTimeItems
            .Select(item => new SerializableOneTimeUpgradableItem(item))
            .ToList();
    }
}
