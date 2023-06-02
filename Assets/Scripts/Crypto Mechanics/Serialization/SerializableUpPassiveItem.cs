using System;

namespace Crypto_Mechanics.Items
{
    [Serializable]
    public class SerializableUpPassiveItem
    {
        public string Name;
        public int Level;
        public double Income;
        public double Price;
        public IncomeType Type;

        public SerializableUpPassiveItem(UpgradableItem upItem)
        {
            if (upItem != null)
            {
                Name = upItem.name;
                Level = upItem.Level;
                Income = upItem.Income;
                Price = upItem.Price;
                Type = upItem.Type;
            }
        }
    }
}