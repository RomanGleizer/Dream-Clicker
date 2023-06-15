using Crypto_Mechanics;
using Crypto_Mechanics.Items;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedActives
{
    public List<SerializableUpActiveItem> SerializableUpActiveItems;

	public SavedActives(PlayerData data)
	{
        SerializableUpActiveItems = data.UpgradableActiveItemList
            .Select(item => new SerializableUpActiveItem(item))
            .ToList();
    }
}
