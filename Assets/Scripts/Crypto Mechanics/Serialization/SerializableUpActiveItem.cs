using System;

namespace Crypto_Mechanics.Items
{
    [Serializable]
    public class SerializableUpActiveItem : SerializableUpItem
    {
        public SerializableUpActiveItem(UpgradableItem upItem) : base(upItem) { }
    }
}