using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLibrary : Singleton<ChunkLibrary>
{
    public TextAsset chunkConft_csv;
    public TextAsset unitConft_csv;
    public TextAsset chunkSocket_csv;
    public static Dictionary<string, ChunkInfo> chunkInfoDic = new Dictionary<string, ChunkInfo>();
    public static Dictionary<string, Transform> chunkModelDic = new Dictionary<string, Transform>();
    public static Dictionary<string, List<UnitInfo>> unitInfoDic = new Dictionary<string, List<UnitInfo>>();
    public static Dictionary<string, SocketInfo> socketInfoDic = new Dictionary<string, SocketInfo>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        foreach (GameObject modelFile in Resources.LoadAll<GameObject>("Chunk"))
        {
            foreach (Transform model in modelFile.transform)
            { chunkModelDic.Add(model.name, model); }
        }
        // 1. 配置拼图块字典
        foreach (string[] info in CSVReader.ReadCSVFile(chunkConft_csv))
        {
            ChunkInfo chunkInfo = new ChunkInfo(info);
            chunkInfoDic.Add(chunkInfo.ID, chunkInfo);
        }
        // 2. 配置单元块字典
        foreach (string[] info in CSVReader.ReadCSVFile(unitConft_csv))
        {
            UnitInfo unitInfo = new UnitInfo(info);
            if (unitInfoDic.ContainsKey(unitInfo.chunkID)) { unitInfoDic[unitInfo.chunkID].Add(unitInfo); }
            else { unitInfoDic.Add(unitInfo.chunkID, new List<UnitInfo>() { unitInfo }); }
        }
        // 3. 配置接口信息字典
        foreach (string[] info in CSVReader.ReadCSVFile(chunkSocket_csv))
        {
            SocketInfo socketInfo = new SocketInfo(info);
            socketInfoDic.Add(socketInfo.ID, socketInfo);
        }
    }
}

public class ChunkModelInfo
{
    [HideInInspector] public Mesh chunkMesh;
    [HideInInspector] public Material[] chunkMaterials;
}

public class ChunkInfo
{
    public string ID;
    public int unit;
    public string defaultModelID;
    public ChunkInfo(string[] info)
    {
        ID = info[0];
        unit = int.Parse(info[1]);
        defaultModelID = info[2];
    }
}

public class UnitInfo
{
    public string chunkID;
    public Vector3Int coord;
    public List<string> sockets = new List<string>();
    public UnitInfo(string[] info)
    {
        chunkID = info[0];
        // coord = new Vector3Int(int.Parse(info[1]), int.Parse(info[3]), int.Parse(info[2]));
        // for (int i = 4; i < 10; i++) { sockets.Add(info[i]); }
    }
}

public class SocketInfo
{
    public string ID;
    public string name_CN;
    public int energy;
    public SocketInfo(string[] info)
    {
        ID = info[0];
        name_CN = info[1];
        energy = int.Parse(info[2]);
    }
}

