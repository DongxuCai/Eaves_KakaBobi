using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Embankment_Edge : MonoBehaviour
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

        bool up_Self = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_Edge") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == rotation;
        bool up_Corner_0 = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_OuterCorner") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == SpiritSoilManager.RotateRotation(rotation, 3);
        bool up_Corner_1 = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_OuterCorner") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == rotation;

        if (up_Self) { index = 1; }
        else if (up_Corner_0) { index = 2; }
        else if (up_Corner_1) { index = 3; }

        spiritSoil.UpdateModel(ID + '-' + index);
    }
}
