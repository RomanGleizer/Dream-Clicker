using System;

namespace Crypto_Mechanics.Items
{
    [Serializable]
    public class SerializableUpPassiveItem : SerializableUpItem
    {
        public SerializableUpPassiveItem(UpgradableItem upItem) : base(upItem) { }
    }
}