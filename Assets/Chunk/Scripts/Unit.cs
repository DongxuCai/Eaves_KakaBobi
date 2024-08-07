using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public string ID;
    public Vector3Int localCoord;
    public Vector3Int worldCoord;
    public int rotation;
    public void Initialize(UnitInfo info, Vector3Int chunkCoord)
    {
        localCoord = info.coord;
        worldCoord = chunkCoord + localCoord;
    }
    public void Rotate(Vector3Int targetChunkCoord)
    {
        localCoord = Tool.RotateCoord(localCoord);
        worldCoord = targetChunkCoord + localCoord;
    }
}
