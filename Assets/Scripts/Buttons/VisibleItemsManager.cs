using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject ActiveItems;
    [SerializeField] private GameObject PassiveItems;
    [SerializeField] private GameObject Tasks;


    public void ShowActive()
    {
        ActiveItems.SetActive(true);
        PassiveItems.SetActive(false);
        Tasks.SetActive(false);
    }

    public void ShowPassive()
    {
        PassiveItems.SetActive(true);
        ActiveItems.SetActive(false);
        Tasks.SetActive(false);
    }

    public void ShowTasks()
    {
        Tasks.SetActive(true);
        PassiveItems.SetActive(false);
        ActiveItems.SetActive(false);
    }
    
    
}
