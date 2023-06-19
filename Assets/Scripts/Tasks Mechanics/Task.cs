using System.Collections.Generic;
using Crypto_Mechanics;
using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{
    public PlayerData Data;
    public List<UpgradableItem> Items;
    public List<OneTimeItem> OneTimeItems;
    public int PlaceInParent;
    public double Cost;
    public double SingleBonus;
    public TextMeshProUGUI Text;
    public bool IsTaskBuy = false;
    public bool IsPossibleToBuy = false;

    public void InitializeTextes(SavedTasks tasks)
    {
        if (PlaceInParent == 0) return;
        IsTaskBuy = tasks.Tasks[PlaceInParent - 1].IsTaskWasBuy;

        if (tasks.Tasks[PlaceInParent - 1].IsTaskWasBuy) Text.text = $"Приобретено";
        else Text.text = $"{Cost} D";
    }
}

#region
//Зачатки вывода задач

//public string[] taskTitles;
//public Sprite[] taskSprites;
//public GameObject button;
//public GameObject content;

//private List<GameObject> tasks = new List<GameObject>();

//private void RemoveList()
//{
//    foreach (var elem in tasks)
//    {
//        Destroy(elem);
//    }
//    tasks.Clear();
//}

//void setTasks()
//{
//    RectTransform rectT = content.GetComponents<RectTransform>()[0];
//    rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
//    RemoveList();
//    if (taskTitles.Length > 0)
//    {
//        var pr1 = Instantiate(button, transform);
//        var h = pr1.GetComponent<RectTransform>().rect.height;
//        var tr = GetComponent<RectTransform>();
//        tr.sizeDelta = new Vector2(tr.rect.width, h * taskTitles.Length);
//        Destroy(pr1);
//        for (int i = 0; i < taskTitles.Length; i++)
//        {
//            var pr = Instantiate(button, transform);
//            pr.GetComponentInChildren<Text>().text = taskTitles[i];
//            pr.GetComponentInChildren<Image>().sprite = taskSprites[i];
//            var i1 = i;
//            tasks.Add(pr);
//        }
//    }
//}

//public int monet;
//public int total_money;
//[SerializeField] bool isFirst;
//private VerticalLayoutGroup _group;

//private void Start()
//{
//    money = PlayerPrefs.GetInt("m");
//    total_money = PlayerPrefs.GetInt("t");
//    isFirst = PlayerPrefs.GetInt("i") == 1 ? true : false;

//    RectTransform rectT = content.GetComponents<RectTransform>()[0];
//    rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
//    _group = GetComponent<VerticalLayoutGroup>();

//    if (isFirst)
//    {
//        StartCoroutine(IdleFarm());
//    }
//}
#endregion