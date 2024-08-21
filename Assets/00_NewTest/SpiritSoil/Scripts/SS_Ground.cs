using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Ground : MonoBehaviour
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
        SpiritSoilManager manager = SpiritSoilManager.Instance;

        if (manager.SpiritSoilCheck(coord + Vector3Int.up, ID)) { index = 1; }

        spiritSoil.UpdateModel(ID + '-' + index);
    }
}
