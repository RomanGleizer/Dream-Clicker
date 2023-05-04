using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public readonly string Name;
    public readonly string Description;
    public readonly Image Image;
    public double Price { get; protected set; }

    public Item(string name, double price, Image image, string description)
    {
        Name = name;
        Price = price;
        Image = image;
        Description = description;
    }

    public abstract void BuyOrUpgrade(PlayerData playerData);
}
