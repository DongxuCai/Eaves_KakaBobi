using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Pavilion : MonoBehaviour
{
    public SpiritSoil spiritSoil;
    private void Update()
    {
        UpdateModel();
    }

    public bool ConnectionCheck(Vector3Int side)
    {
        Vector3Int coord = spiritSoil.coord;
        int rotation = spiritSoil.rotation;
        SpiritSoilManager manager = SpiritSoilManager.Instance;
        Vector3Int neiCoord = coord + Tool.RotateDirection(side, rotation);
        bool wallConnected = manager.SpiritSoilTypeCheck(neiCoord, "Gallery");
        bool specialConnected = false;
        if (manager.SpiritSoilTypeCheck(neiCoord, "Pavilion"))
        {
            if (manager.spiritSoilDic[neiCoord].rotation == rotation || manager.spiritSoilDic[neiCoord].rotation == Tool.GetRotation(rotation + 2))
            { specialConnected = true; }
        }
        return wallConnected || specialConnected;
    }

    public void UpdateModel()
    {
        string ID = spiritSoil.ID;
        if (!spiritSoil.selected)
        {
            bool left = ConnectionCheck(Vector3Int.left);
            bool right = ConnectionCheck(Vector3Int.right);

            if (!left && right) { spiritSoil.UpdateModel(ID + '-' + 1); }
            else if (left && !right) { spiritSoil.UpdateModel(ID + '-' + 2); }
            else if (left && right) { spiritSoil.UpdateModel(ID + '-' + 3); }
            else { spiritSoil.UpdateModel(ID + '-' + 0); }
        }
        else { spiritSoil.UpdateModel(ID + '-' + 0); }
    }
}
