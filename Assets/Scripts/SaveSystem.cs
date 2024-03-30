using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UltraLiteDB;
using System;
using System.Collections.Generic;

public static class SaveSystem
{
    public const bool useUltraLiteDB = true;
    public static string dataPath = Application.persistentDataPath + "/DungeDanceData.uldboba"; //<- TMP - TEMPERORY (NoMore) !!! (change later) IN TWO SCRIPTS!!! (1/3)
    public static void Save(SaveData saveData)
    {
        if (useUltraLiteDB)
        {
            SaveULDB(saveData);
        }
        else
        {
            SaveBinary(saveData);
        }
    }
    private static void SaveBinary(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //string dataPath = Application.persistentDataPath + "/DungeDanceData.aboba";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        SaveData data = saveData;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    private static void SaveULDB(SaveData saveData)
    {
        UltraLiteDatabase ulDB = new UltraLiteDatabase(dataPath);
        UltraLiteCollection<BsonDocument> saveCollection = ulDB.GetCollection("SaveDatas");
        BsonDocument saveDataWrapperBson = new BsonDocument();
        BsonMapper bsonMapper = new BsonMapper();
        bsonMapper.UseLowerCaseDelimiter('_');
        bsonMapper.IncludeFields = true;

        SaveData ulSaveData = saveData;

        BsonDocument saveDataBson = bsonMapper.ToDocument<SaveData>(ulSaveData);
        saveDataWrapperBson["SaveData"] = saveDataBson;

        saveCollection.Upsert(saveDataWrapperBson);

        ulDB.Dispose();
    }
    public static SaveData Load()
    {
        if (useUltraLiteDB) 
        { 
            return LoadULDB();
        }
        else
        {
            return LoadBinary();
        }
    }

    private static SaveData LoadBinary()
    {
        //string dataPath = Application.persistentDataPath + "/DungeDanceData.aboba"; //<- TMP - TEMPERORY (NoMore) !!! (change later) IN TWO SCRIPTS!!! (2/3)
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

    private static SaveData LoadULDB()
    {
        if (File.Exists(dataPath))
        {
            SaveData saveData = null;

            UltraLiteDatabase ulDB = new UltraLiteDatabase(dataPath);

            BsonMapper bsonMapper = new BsonMapper();
            bsonMapper.UseLowerCaseDelimiter('_');
            bsonMapper.IncludeFields = true;

            UltraLiteCollection<BsonDocument> saveDataCollection = ulDB.GetCollection("SaveDatas");
            List<BsonDocument> collectionSaveDatas = new List<BsonDocument>(saveDataCollection.FindAll());

            foreach (BsonDocument saveDataWrapperBson in collectionSaveDatas)
            {
                saveData = bsonMapper.ToObject<SaveData>(saveDataWrapperBson["SaveData"].AsDocument);
            }

            ulDB.Dispose();

            return saveData;

        }
        else
        {
            return null;
        }
    }
}
