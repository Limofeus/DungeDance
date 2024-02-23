using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string dataPath = Application.persistentDataPath + "/DungeDanceData.aboba"; //<- TMP - TEMPERORY (NoMore) !!! (change later) IN TWO SCRIPTS!!! (1/3)
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        SaveData data = saveData;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }
    public static SaveData Load()
    {
        string dataPath = Application.persistentDataPath + "/DungeDanceData.aboba"; //<- TMP - TEMPERORY (NoMore) !!! (change later) IN TWO SCRIPTS!!! (2/3)
        if (File.Exists(dataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            SaveData data = binaryFormatter.Deserialize(fileStream) as SaveData;
            fileStream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning("No .aboba has been found in " + dataPath);
            return null;
        }
    }
}
