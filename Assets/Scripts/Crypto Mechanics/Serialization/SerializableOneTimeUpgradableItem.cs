using System;

[Serializable]
public class SerializableOneTimeUpgradableItem
{
    public double Price;
    public string Name;

    public SerializableOneTimeUpgradableItem(OneTimeItem oneTimeItem)
    {
        Price = oneTimeItem.Price;
        Name = oneTimeItem.Name;
    }
}
