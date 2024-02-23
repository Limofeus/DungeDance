using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader
{
    public TextAsset csvFile;
    public char lineSplitter = '\n';
    public char surround = '"';
    public string[] fieldSplitter = { "\",\"" };
    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string[] lines = csvFile.text.Split(lineSplitter);
        int attributeIndex = -1;
        string[] headers = lines[0].Split(fieldSplitter, System.StringSplitOptions.None);
        for (int i = 0; i < headers.Length; i++)
        {
            //Debug.Log(headers[i]);
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
            }
        }
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");// <- This Regex is very colorful in my VS, but I still hate it.
        for(int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);
            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = fields[f].TrimStart(' ',surround);
                fields[f] = fields[f].TrimEnd();
                fields[f] = fields[f].TrimEnd(surround);
                //Debug.Log(fields[f]);
            }
            if(fields.Length > attributeIndex)
            {
                var key = fields[0];
                if (dictionary.ContainsKey(key)) { continue; }
                var value = fields[attributeIndex];
                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }
}
