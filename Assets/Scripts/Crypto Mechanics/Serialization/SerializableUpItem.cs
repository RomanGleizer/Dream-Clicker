using System;

namespace Crypto_Mechanics.Items
{
    [Serializable]
    public class SerializableUpItem
    {
        public string name;
        public int level;
        public double income;
        public double price;
        public IncomeType type;

        public SerializableUpItem(UpgradableItem upItem)
        {
            name = upItem.Name;
            level = upItem.Level;
            income = upItem.Income;
            price = upItem.Price;
            type = upItem.Type;
        }
    }
}