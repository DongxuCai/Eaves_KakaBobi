using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    public static List<string[]> ReadCSVFile(TextAsset csvFile)
    {
        List<string[]> result = new List<string[]>();
        string[] data = csvFile.text.Split('\n');
        for (int i = 1; i < data.Length - 1; i++)
        {
            string info = data[i].Substring(0, data[i].Length - 1);
            result.Add(info.Split(','));
        }
        return result;
    }
}
