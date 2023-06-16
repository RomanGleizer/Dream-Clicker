using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    private void Update()
    {
        File.WriteAllText(
            Application.dataPath + "/Last Visit Data.json",
            DateTime.Now.ToString(CultureInfo.CurrentCulture));
    }
}