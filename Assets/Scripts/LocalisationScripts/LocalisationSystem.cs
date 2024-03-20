using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static void Init()
    {
        Debug.Log("LocalizationInitialized");
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
        isInit = true;
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
