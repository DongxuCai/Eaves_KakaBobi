using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_DoubleGallery : MonoBehaviour
{
    public SpiritSoil spiritSoil;

    public bool forward;
    public bool right;
    public bool back;
    public bool left;

    private void Update()
    {
        UpdateModel();
    }

    public bool ConnectionCheck(Vector3Int direction)
    {
        Vector3Int coord = spiritSoil.coord;
        int rotation = spiritSoil.rotation;
        SpiritSoilManager manager = SpiritSoilManager.Instance;
        Vector3Int neiCoord = coord + Tool.RotateDirection(direction, rotation);
        // 判断是否和自身（墙）相连 [只要相邻就会相连]
        bool selfConnected = manager.SpiritSoilCheck(neiCoord, "DoubleGallery");
        // 判断是否和门（特殊墙段）相连 [需要对应截面方向才会相连]
        bool specialConnected = false;
        if (manager.SpiritSoilTypeCheck(neiCoord, "DoublePavilion"))
        {
            int rotation_1 = rotation - 1;
            int rotation_2 = rotation + 1;
            if (direction == Vector3Int.right || direction == Vector3Int.left) { rotation_1 = rotation; rotation_2 = rotation + 2; }
            if (manager.spiritSoilDic[neiCoord].rotation == Tool.GetRotation(rotation_1) || manager.spiritSoilDic[neiCoord].rotation == Tool.GetRotation(rotation_2))
            { specialConnected = true; }
        }
        return selfConnected || specialConnected;
    }

    public void UpdateModel()
    {
        if (!spiritSoil.selected)
        {
            forward = ConnectionCheck(Vector3Int.forward);
            right = ConnectionCheck(Vector3Int.right);
            back = ConnectionCheck(Vector3Int.back);
            left = ConnectionCheck(Vector3Int.left);

            int index = 0;
            if (forward && !right && !back && !left)
            { index = 1; }
            else if (!forward && right && !back && !left)
            { index = 2; }
            else if (!forward && !right && back && !left)
            { index = 3; }
            else if (!forward && !right && !back && left)
            { index = 4; }
            else if (forward && !right && back && !left)
            { index = 5; }
            else if (!forward && right && !back && left)
            { index = 6; }
            else if (forward && right && !back && !left)
            { index = 7; }
            else if (!forward && right && back && !left)
            { index = 8; }
            else if (!forward && !right && back && left)
            { index = 9; }
            else if (forward && !right && !back && left)
            { index = 10; }
            else if (forward && right && back && !left)
            { index = 11; }
            else if (!forward && right && back && left)
            { index = 12; }
            else if (forward && !right && back && left)
            { index = 13; }
            else if (forward && right && !back && left)
            { index = 14; }
            else if (forward && right && back && left)
            { index = 15; }
            spiritSoil.UpdateModel(spiritSoil.ID + '-' + index);
        }
        else { spiritSoil.UpdateModel(spiritSoil.ID + '-' + 0); }
    }
}
