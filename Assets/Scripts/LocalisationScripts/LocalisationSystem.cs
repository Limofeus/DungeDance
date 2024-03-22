using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

using System;
using System.IO;
public class LocalisationSystem
{
    public enum Language
    {
        English,
        Russian,
        Japanise
    }
    public static Language language = Language.Russian;
    public static Dictionary<string, string> localizedDictionary;
    public static bool isInit = false;

    static readonly ProfilerMarker localizationInitMarker = new ProfilerMarker("Localisation.CustomMarker.Init");

    public static void Init()
    {
        Debug.Log("LocalisationInitialized");

        using (localizationInitMarker.Auto())
        {
            //var stopWatch = System.Diagnostics.Stopwatch.StartNew();

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

            //stopWatch.Stop();
            //var elapsedMs = stopWatch.ElapsedMilliseconds;
            //Debug.Log(elapsedMs);
        }
        ExportLocaleToFile();
        isInit = true;
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
    public static string GetLocalizedValue(string key)
    {
        if (!isInit) Init();
        string value;
        if (localizedDictionary.ContainsKey(key))
        {
            localizedDictionary.TryGetValue(key, out value);
            return value;
        }
        else
            return key;
        //Debug.Log("Key exists?: " + localizedDictionary.ContainsKey(key) + " Returning: " + value);
    }
}
