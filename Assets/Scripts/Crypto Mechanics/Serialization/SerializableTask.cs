using System;
using UnityEngine;

[Serializable]
public class SerializableTask
{
    public double Cost;
    public bool IsTaskWasBuy;

    public SerializableTask(Task task)
    {
        Cost = task.Cost;
        IsTaskWasBuy = task.IsTaskBuy;
    }
}
