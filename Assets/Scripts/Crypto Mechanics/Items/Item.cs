using Crypto_Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] Image Image;

    public abstract void BuyOrUpgrade(PlayerData playerData);
}
