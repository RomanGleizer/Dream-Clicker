using System.Collections.Generic;
using Crypto_Mechanics;
using UnityEngine;
using TMPro;
using Crypto_Mechanics.Serialization;
using System.IO;

public class Task : MonoBehaviour
{
    [SerializeField] public int PlaceInParent;
    [SerializeField] public List<string> Requirements;
    [SerializeField] public double Cost;
    [SerializeField] public double SingleBonus;
    [SerializeField] private TextMeshProUGUI _text;

    private bool _isPossibleToBuy = false;

    private void Start()
    {
        var json = File.ReadAllText("Assets/Resources/savedData.json");
        var newData = JsonUtility.FromJson<SerializablePlayerData>(json);

        if (PlaceInParent == newData.Tasks.Count) _text.text = $"Приобретено";
        else _text.text = $"{Cost} D";
    }

    public void Buy(PlayerData data)
    {
        for (int i = 0; i < Requirements.Count; i++)
        {
            for (int j = 0; j < data.UpgradableActiveItemList.Count; j++)
                if (Requirements[i] == data.UpgradableActiveItemList[i].Name)
                {
                    _isPossibleToBuy = true;
                    break;
                }

            for (int j = 0; j < data.UpgradablePassiveItemList.Count; j++)
                if (Requirements[i] == data.UpgradablePassiveItemList[i].Name)
                {
                    _isPossibleToBuy = true;
                    break;
                }
        }

        if (data.TotalCurrencyCnt >= Cost && _isPossibleToBuy && _text.text != "Приобретено")
        {
            data.TotalCurrencyCnt -= Cost;
            data.TotalCurrencyCnt += SingleBonus;
            data.Tasks.Add(this);
            _text.text = "Приобретено";
        }
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