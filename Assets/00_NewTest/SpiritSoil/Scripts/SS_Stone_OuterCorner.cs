using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Stone_OuterCorner : MonoBehaviour
{
    public SpiritSoil spiritSoil;

    private void Update()
    {
        UpdateModel();
    }

    public void UpdateModel()
    {
        int index = 0;

        string ID = spiritSoil.ID;
        Vector3Int coord = spiritSoil.coord;
        int rotation = spiritSoil.rotation;
        SpiritSoilManager manager = SpiritSoilManager.Instance;

        bool up_Isolated = !manager.spiritSoilDic.ContainsKey(coord + Vector3Int.up);
        bool up_Self = manager.SpiritSoilCheck(coord + Vector3Int.up, ID) && manager.spiritSoilDic[coord + Vector3Int.up].rotation == rotation;
        bool down_Self = (manager.SpiritSoilCheck(coord + Vector3Int.down, ID) || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_OuterCorner"))
                            && manager.spiritSoilDic[coord + Vector3Int.down].rotation == rotation;
        bool down_Ground = manager.SpiritSoilCheck(coord + Vector3Int.down, "Ground");
        bool down_Corner_0 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_InnerCorner") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_InnerCorner"))
                                && manager.spiritSoilDic[coord + Vector3Int.down].rotation == SpiritSoilManager.RotateRotation(rotation, 3);
        bool down_Corner_1 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_InnerCorner") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_InnerCorner"))
                                && manager.spiritSoilDic[coord + Vector3Int.down].rotation == rotation;
        bool down_Corner_2 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_InnerCorner") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_InnerCorner"))
                                && manager.spiritSoilDic[coord + Vector3Int.down].rotation == SpiritSoilManager.RotateRotation(rotation, 1);
        bool down_Edge_0 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_Edge") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_Edge"))
                            && manager.spiritSoilDic[coord + Vector3Int.down].rotation == rotation;
        bool down_Edge_1 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_Edge") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_Edge"))
                            && manager.spiritSoilDic[coord + Vector3Int.down].rotation == SpiritSoilManager.RotateRotation(rotation, 1);

        if (up_Isolated && (down_Ground || down_Corner_1)) { index = 1; }
        else if (up_Self && (down_Ground || down_Corner_1)) { index = 2; }
        else if (up_Self && down_Self) { index = 3; }
        else if (up_Isolated && down_Self) { index = 4; }
        else if (up_Self && (down_Edge_0 || down_Corner_0)) { index = 5; }
        else if (up_Self && (down_Edge_1 || down_Corner_2)) { index = 6; }
        else if (up_Isolated && (down_Edge_0 || down_Corner_0)) { index = 7; }
        else if (up_Isolated && (down_Edge_1 || down_Corner_2)) { index = 8; }


        spiritSoil.UpdateModel(ID + '-' + index);
    }
}
