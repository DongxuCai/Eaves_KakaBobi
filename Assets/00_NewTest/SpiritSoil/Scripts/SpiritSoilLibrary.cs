using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSoilLibrary : Singleton<SpiritSoilLibrary>
{
    public static Dictionary<string, Transform> modelDic = new Dictionary<string, Transform>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        foreach (GameObject modelFile in Resources.LoadAll<GameObject>("Chunk"))
        {
            foreach (Transform model in modelFile.transform)
            { modelDic.Add(model.name, model); }
        }
    }
}
