using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Building_RetailBasic : Chunk
{
      [Header("接口")]
      public Socket F;
      public Socket R;
      public Socket B;
      public Socket L;
      public Socket U;
      public Socket D;

      protected override void Start()
      {
            base.Start();
            F.ID = "B-RT-B";
            R.ID = "B-RT-L";
            B.ID = "B-RT-F";
            L.ID = "B-RT-R";
            U.ID = "B-RT-U";
            D.ID = "0";
      }

      public void UpdateModelBySockets()
      {
            if (
                  F.targetID != "B-RT-L" && F.targetID != "B-RT-R" &&
                  R.targetID == "B-RT-R" &&
                  // B.targetID != "B-RT-L" && B.targetID != "B-RT-R" &&
                  L.targetID != "B-RT-L"
            )
            { UpdateModel("B-RT-1"); }
            else if (
                  F.targetID != "B-RT-L" && F.targetID != "B-RT-R" &&
                  R.targetID == "B-RT-R" &&
                  // B.targetID != "B-RT-L" && B.targetID != "B-RT-R" &&
                  L.targetID == "B-RT-L"
            )
            { UpdateModel("B-RT-2"); }
            else if (
                  F.targetID != "B-RT-L" && F.targetID != "B-RT-R" &&
                  R.targetID != "B-RT-R" &&
                  // B.targetID != "B-RT-L" && B.targetID != "B-RT-R" &&
                  L.targetID == "B-RT-L"
            )
            { UpdateModel("B-RT-3"); }
            else if (
                  F.targetID == "B-RT-L" &&
                  R.targetID == "B-RT-R" &&
                  // B.targetID != "B-RT-L" && B.targetID != "B-RT-R" &&
                  L.targetID != "B-RT-L"
            )
            { UpdateModel("B-RT-4"); }
            else if (
                  F.targetID == "B-RT-R" &&
                  R.targetID != "B-RT-R" &&
                  // B.targetID != "B-RT-L" && B.targetID != "B-RT-R" &&
                  L.targetID == "B-RT-L"
            )
            { UpdateModel("B-RT-5"); }
            else
            { UpdateModel("B-RT-0"); }
      }
}
