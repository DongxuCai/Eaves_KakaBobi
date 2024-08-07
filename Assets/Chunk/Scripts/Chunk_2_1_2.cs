using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_2_1_2 : Chunk
{
      [Header("单元块")]
      public Unit unit_0_0_0;
      public Unit unit_1_0_0;
      [Header("接口")]
      public Socket F_0;
      public Socket F_1;
      public Socket R;
      public Socket B_0;
      public Socket B_1;
      public Socket L;
      public Socket U_0;
      public Socket U_1;
      public Socket D_0;
      public Socket D_1;

      protected override void Start()
      {
            base.Start();
            ChunkManager.Instance.units.Add(unit_0_0_0);
            ChunkManager.Instance.units.Add(unit_1_0_0);
            switch (ID)
            {
                  case "CW":
                        F_0.ID = "LD-T-E";
                        F_1.ID = "LD-T-E";
                        R.ID = "LD-T-S";
                        B_0.ID = "LD-T-E";
                        B_1.ID = "LD-T-E";
                        L.ID = "LD-T-S";
                        U_0.ID = "LD-T-U";
                        U_1.ID = "LD-T-U";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        break;
            }
      }

      public void UpdateModelBySockets()
      {
            switch (ID)
            {
                  case "CW": UpdateModel("CW-0"); break;
            }
      }
}
