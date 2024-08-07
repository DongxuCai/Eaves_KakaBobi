using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_3_1_1 : Chunk
{
      [Header("单元块")]
      public Unit unit_0_0_0;
      public Unit unit_1_0_0;
      public Unit unit_2_0_0;
      [Header("接口")]
      public Socket F_0;
      public Socket F_1;
      public Socket F_2;
      public Socket R;
      public Socket B_0;
      public Socket B_1;
      public Socket B_2;
      public Socket L;
      public Socket U_0;
      public Socket U_1;
      public Socket U_2;
      public Socket D_0;
      public Socket D_1;
      public Socket D_2;

      protected override void Start()
      {
            base.Start();
            ChunkManager.Instance.units.Add(unit_0_0_0);
            ChunkManager.Instance.units.Add(unit_1_0_0);
            switch (ID)
            {
                  case "CW-SG":
                        F_0.ID = "LD";
                        F_1.ID = "ST-S";
                        F_2.ID = "LD";
                        R.ID = "CW-L";
                        B_0.ID = "LD";
                        B_1.ID = "ST-S";
                        B_2.ID = "LD";
                        L.ID = "CW-R";
                        U_0.ID = "CW-U-0";
                        U_1.ID = "CW-U-1";
                        U_2.ID = "CW-U-2";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        D_2.ID = "0";
                        break;
            }
      }

      public void UpdateModelBySockets()
      {
            switch (ID)
            {
                  case "CW-SG":
                        if
                        (
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") && L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-SG-1"); }
                        else if
                        (
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") && (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-SG-2"); }
                        else if
                        (
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" && (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-SG-3"); }
                        else { UpdateModel("CW-SG-0"); }
                        break;
            }
      }
}
