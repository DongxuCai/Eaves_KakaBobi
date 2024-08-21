using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Embankment_OuterCorner : MonoBehaviour
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

        bool up_Self = manager.SpiritSoilCheck(coord + Vector3Int.up, "Stone_OuterCorner") && manager.spiritSoilDic[coord + Vector3Int.up].rotation == rotation;
        if (up_Self) { index = 1; }

        spiritSoil.UpdateModel(ID + '-' + index);
    }
}
