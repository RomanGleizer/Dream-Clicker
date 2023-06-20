using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public static class TimeHandler 
{
    private static string _currentTimeDataPath = null;

    static TimeHandler()
    {
        _currentTimeDataPath = Application.persistentDataPath + "/Last Visit Data.json";
    }

    public static void SaveTime()
    {
        File.WriteAllText(
            _currentTimeDataPath,
            DateTime.Now.ToString(CultureInfo.CurrentCulture));
    }
}