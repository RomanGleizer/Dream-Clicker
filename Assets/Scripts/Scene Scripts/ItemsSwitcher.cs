using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    public void SwitchItems(int index)
    {
        switch (index)
        {
            case 0:
                ChangeItemsStatus(0, 1, 2);
                break;
            case 1:
                ChangeItemsStatus(1, 0, 2);
                break;
            case 2:
                ChangeItemsStatus(2, 0, 1);
                break;
            default:
                break;
        }
    }

    public void ChangeItemsStatus(int turnOnItemIndex, params int[] turnOffItemsIndexes)
    {
        items[turnOnItemIndex].SetActive(true);
        items[turnOffItemsIndexes[0]].SetActive(false);
        items[turnOffItemsIndexes[1]].SetActive(false);
    }
}
