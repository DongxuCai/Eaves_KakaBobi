using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_2_1_1 : Chunk
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
                  case "ST":
                        F_0.ID = "ST-E";
                        F_1.ID = "ST-E";
                        R.ID = "ST-S";
                        B_0.ID = "ST-E";
                        B_1.ID = "ST-E";
                        L.ID = "ST-S";
                        U_0.ID = "0";
                        U_1.ID = "0";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        break;
                  case "CW":
                        F_0.ID = "CW-B-0";
                        F_1.ID = "CW-B-1";
                        R.ID = "CW-L";
                        B_0.ID = "CW-F-0";
                        B_1.ID = "CW-F-1";
                        L.ID = "CW-R";
                        U_0.ID = "0";
                        U_1.ID = "0";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        break;
                  case "AG-F-A":
                        F_0.ID = "LD";
                        F_1.ID = "LD";
                        R.ID = "AG-F-A";
                        B_0.ID = "LD";
                        B_1.ID = "LD";
                        L.ID = "AG-F-A";
                        U_0.ID = "0";
                        U_1.ID = "0";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        break;
                  case "AG-F-B":
                        F_0.ID = "LD";
                        F_1.ID = "LD";
                        R.ID = "AG-F-B";
                        B_0.ID = "LD";
                        B_1.ID = "LD";
                        L.ID = "AG-F-B";
                        U_0.ID = "0";
                        U_1.ID = "0";
                        D_0.ID = "0";
                        D_1.ID = "0";
                        break;
            }
      }
      public void Update()
      {
            UpdateModelBySockets();
      }

      public void UpdateModelBySockets()
      {
            switch (ID)
            {
                  case "ST":
                        if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-1"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-2"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-3"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-4"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-5"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-6"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-7"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-8"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-9"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-10"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-11"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-12"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-13"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-14"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-15"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-16"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-17"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-18"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-19"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-20"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-21"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-22"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-23"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-24"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-25"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-26"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-27"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        L.targetID != "ST-S" && L.targetID != "ST-E"
                        )
                        { UpdateModel("ST-28"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        R.targetID != "ST-S" && R.targetID != "ST-E" &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-29"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-30"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-31"); }
                        else if
                        (
                        F_0.targetID == "ST-S" &&
                        F_1.targetID != "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID == "ST-S" &&
                        B_1.targetID != "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-32"); }
                        else if
                        (
                        F_0.targetID != "ST-S" &&
                        F_1.targetID == "ST-S" &&
                        (R.targetID == "ST-S" || R.targetID == "ST-E") &&
                        B_0.targetID != "ST-S" &&
                        B_1.targetID == "ST-S" &&
                        (L.targetID == "ST-S" || L.targetID == "ST-E")
                        )
                        { UpdateModel("ST-33"); }
                        else { UpdateModel("ST-0"); }
                        break;
                  case "CW":
                        if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-1"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-2"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-3"); }
                        else if
                        (
                              F_0.targetID == "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-4"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID == "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-5"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID == "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-6"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID == "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-7"); }
                        else if
                        (
                              F_0.targetID == "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-8"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID == "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-9"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID == "CW-R" &&
                              (L.targetID == "CW-L" || L.targetID == "CW-B-1" || L.targetID == "CW-F-1")
                        )
                        { UpdateModel("CW-10"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              (R.targetID == "CW-R" || R.targetID == "CW-B-0" || R.targetID == "CW-F-0") &&
                              B_0.targetID == "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-11"); }
                        else if
                        (
                              F_0.targetID == "CW-L" &&
                              F_1.targetID != "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID != "CW-L" &&
                              B_1.targetID == "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-12"); }
                        else if
                        (
                              F_0.targetID != "CW-L" &&
                              F_1.targetID == "CW-R" &&
                              R.targetID != "CW-R" && R.targetID != "CW-B-0" && R.targetID != "CW-F-0" &&
                              B_0.targetID == "CW-L" &&
                              B_1.targetID != "CW-R" &&
                              L.targetID != "CW-L" && L.targetID != "CW-B-1" && L.targetID != "CW-F-1"
                        )
                        { UpdateModel("CW-13"); }
                        else { UpdateModel("CW-0"); }
                        break;
                  case "AG-F-A":
                        if
                        (
                        R.targetID == "AG-F-A" &&
                        L.targetID != "AG-F-A"
                        )
                        { UpdateModel("AG-F-A-1"); }
                        else if
                        (
                        R.targetID == "AG-F-A" &&
                        L.targetID == "AG-F-A"
                        )
                        { UpdateModel("AG-F-A-2"); }
                        else if
                        (
                        R.targetID != "AG-F-A" &&
                        L.targetID == "AG-F-A"
                        )
                        { UpdateModel("AG-F-A-3"); }
                        else {UpdateModel("AG-F-A-0");}
                  break;
                  case "AG-F-B":
                        if
                        (
                        R.targetID == "AG-F-B" &&
                        L.targetID != "AG-F-B"
                        )
                        { UpdateModel("AG-F-B-1"); }
                        else if
                        (
                        R.targetID == "AG-F-B" &&
                        L.targetID == "AG-F-B"
                        )
                        { UpdateModel("AG-F-B-2"); }
                        else if
                        (
                        R.targetID != "AG-F-B" &&
                        L.targetID == "AG-F-B"
                        )
                        { UpdateModel("AG-F-B-3"); }
                        else {UpdateModel("AG-F-B-0");}
                  break;
            }
      }
}
