using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSoilLibrary : Singleton<SpiritSoilLibrary>
{
    public TextAsset spiritSoilInfoTable;
    public static Dictionary<string, Transform> modelDic = new Dictionary<string, Transform>();
    public static Dictionary<string, string> spiritSoilInfo = new Dictionary<string, string>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        foreach (GameObject modelFile in Resources.LoadAll<GameObject>("Chunk"))
        {
            foreach (Transform model in modelFile.transform)
            { modelDic.Add(model.name, model); }
        }

        foreach (string[] info in CSVReader.ReadCSVFile(spiritSoilInfoTable))
        {
            spiritSoilInfo.Add(info[0], info[1]);
        }
    }
}
