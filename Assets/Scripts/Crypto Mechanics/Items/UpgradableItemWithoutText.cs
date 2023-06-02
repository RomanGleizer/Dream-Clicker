using Crypto_Mechanics;
using UnityEngine;

public class UpgradableItemWithoutText : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public PlayerData data;
    [SerializeField] public int Level;
    [SerializeField] public IncomeType Type;
    [SerializeField] public double Income;
    [SerializeField] public double price;
}
