using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Stone_Edge : MonoBehaviour
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
        bool up_Corner_0 = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_OuterCorner") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == SpiritSoilManager.RotateRotation(rotation, 3);
        bool up_Corner_1 = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_OuterCorner") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == rotation;
        bool down_Self = (manager.SpiritSoilCheck(coord + Vector3Int.down, ID) || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_Edge"))
                        && manager.spiritSoilDic[coord + Vector3Int.down].rotation == rotation;
        bool down_Ground = manager.SpiritSoilCheck(coord + Vector3Int.down, "Ground");
        bool down_Corner_0 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_InnerCorner") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_InnerCorner"))
                            && manager.spiritSoilDic[coord + Vector3Int.down].rotation == SpiritSoilManager.RotateRotation(rotation, 3);
        bool down_Corner_1 = (manager.SpiritSoilCheck(coord + Vector3Int.down, "Stone_InnerCorner") || manager.SpiritSoilCheck(coord + Vector3Int.down, "Embankment_InnerCorner"))
                            && manager.spiritSoilDic[coord + Vector3Int.down].rotation == rotation;

        if (up_Isolated && down_Ground) { index = 1; }
        else if (up_Self && down_Ground) { index = 2; }
        else if (up_Self && down_Self) { index = 3; }
        else if (up_Isolated && down_Self) { index = 4; }

        else if (up_Isolated && down_Corner_0) { index = 5; }
        else if (up_Isolated && down_Corner_1) { index = 6; }
        else if (up_Self && down_Corner_0) { index = 7; }
        else if (up_Self && down_Corner_0) { index = 8; }

        else if (up_Corner_0 && down_Self) { index = 9; }
        else if (up_Corner_1 && down_Self) { index = 10; }
        else if (up_Corner_0 && down_Ground) { index = 11; }
        else if (up_Corner_1 && down_Ground) { index = 12; }

        else if (up_Corner_0 && down_Corner_1) { index = 13; }
        else if (up_Corner_1 && down_Corner_0) { index = 14; }
        else if (up_Corner_0 && down_Corner_0) { index = 15; }
        else if (up_Corner_1 && down_Corner_1) { index = 16; }

        spiritSoil.UpdateModel(ID + '-' + index);
    }
}
