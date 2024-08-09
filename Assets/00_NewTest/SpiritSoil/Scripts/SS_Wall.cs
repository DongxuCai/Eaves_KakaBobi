using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Wall : MonoBehaviour
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

    public void UpdateModel()
    {
        if (!spiritSoil.selected)
        {
            string ID = spiritSoil.ID;
            Vector3Int coord = spiritSoil.coord;
            forward = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.forward, ID);
            right = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.right, ID);
            back = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.back, ID);
            left = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.left, ID);

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
