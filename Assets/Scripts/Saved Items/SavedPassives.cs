using Crypto_Mechanics;
using Crypto_Mechanics.Items;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedPassives
{
    public List<SerializableUpPassiveItem> SerializableUpPassiveItems;
    public SavedPassives(PlayerData data)
    {
        SerializableUpPassiveItems = data.UpgradablePassiveItemList
            .Select(item => new SerializableUpPassiveItem(item))
            .ToList();
    }
}
