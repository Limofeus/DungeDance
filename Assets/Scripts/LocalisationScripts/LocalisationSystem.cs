using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

using System.Data;
using Mono.Data.Sqlite;

using System;
using System.IO;
public class LocalisationSystem
{
    public enum Language
    {
        English = 1,
        Russian = 2,
        Japanise = 3
    }
    public static Language language = Language.Russian;
    public static Dictionary<string, string> localizedDictionary;
    public static bool isInit = false;

    private const bool useSqlite = true;
    private const bool sqliteNoPreload = true;
    private const string dbTableName = "Locales";
    private static IDbConnection dbConnection;

    static readonly ProfilerMarker localizationInitMarker = new ProfilerMarker("Localisation.CustomMarker.Init");

    public static void Init()
    {
        Debug.Log("LocalisationInitialized");

        if (!useSqlite)
        {
            InitWithDictsCSV();
        }
        else
        {
            InitSQLite();
            if (!sqliteNoPreload)
            {
                PreloadSqlite();
            }
        }

        //ExportLocaleToFile();
        isInit = true;
    }
    private static void InitWithDictsCSV()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        switch (language)
        {
            case Language.English:
                //Debug.Log("eng switch");
                localizedDictionary = csvLoader.GetDictionaryValues("en");
                break;
            case Language.Russian:
                localizedDictionary = csvLoader.GetDictionaryValues("ru");
                break;
            case Language.Japanise:
                localizedDictionary = csvLoader.GetDictionaryValues("jp");
                break;
        }
    }
    private static void InitSQLite()
    {
        dbConnection = CreateAndOpenDatabase();
    }
    private static void PreloadSqlite()
    {
        localizedDictionary = new Dictionary<string, string>();

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM " + dbTableName;
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read()) // 18
        {
            localizedDictionary.Add(dataReader.GetString(0), dataReader.GetString((int)language));
        }
    }
    public static string GetLocalizedValue(string key)
    {
        if (!isInit) Init();
        if(useSqlite && sqliteNoPreload)
        {
            return GetlocalizedValueFromDB(key);
        }
        else
        {
            return GetLocalizedValueFromDicts(key);
        }
    }
    private static string GetLocalizedValueFromDicts(string key)
    {
        string value;
        if (localizedDictionary.ContainsKey(key))
        {
            localizedDictionary.TryGetValue(key, out value);
            return value;
        }
        else
            return key;
    }
    private static string GetlocalizedValueFromDB(string key)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + dbTableName + " WHERE \"key\" = '" + key + "'";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        return dataReader.GetString((int)language);
    }
    private static string GetDatabasePath()
    {
        return Path.Combine(Application.streamingAssetsPath, "Locales.db");
    }
    private static IDbConnection CreateAndOpenDatabase() // 3
    {

        string dbPath = GetDatabasePath();
        IDbConnection dbConnection = new SqliteConnection("Data Source=" + dbPath);
        Debug.Log("Connecting to SQLiteDB");
        dbConnection.Open();

        return dbConnection;
    }
    public static void ExportLocaleToFile()
    {
        var fileName = "C:\\Users\\Limofeus\\Desktop\\LocaleCSV2.txt";
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        Dictionary<string, string> enDict = csvLoader.GetDictionaryValues("en");
        csvLoader.LoadCSV();
        Dictionary<string, string> ruDict = csvLoader.GetDictionaryValues("ru");
        string endString = "";
        foreach(KeyValuePair<string, string> entry in enDict)
        {
            string dictKey = entry.Key;
            endString += dictKey + "\t" + enDict[dictKey] + "\t" + ruDict[dictKey] + "\n";
        }
        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        sr.WriteLine(endString);
        sr.Close();
    }
    ~LocalisationSystem()
    {
        if(dbConnection.State != ConnectionState.Closed)
        {
            Debug.Log("Closing SQLite connection");
            dbConnection.Close();
        }
    }
}
