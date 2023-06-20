using System;

[Serializable]
public class SerializableOneTimeUpgradableItem
{
    public double Price;
    public string Text;
    public bool IsItemWasBought;

    public SerializableOneTimeUpgradableItem(OneTimeItem oneTimeItem)
    {
        Price = oneTimeItem.Price;
        Text = oneTimeItem.Text.text;
        IsItemWasBought = oneTimeItem.IsItemWasBought;
    }
}
