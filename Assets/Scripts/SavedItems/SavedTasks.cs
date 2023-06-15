using Crypto_Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedTasks
{
    public List<SerializableTask> Tasks;

    public SavedTasks(PlayerData data)
    {
        Tasks = data.Tasks.Select(item => new SerializableTask(item)).ToList();
    }
}