using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Water : Chunk, IChunkObserver
{
    [Header("接口")]
    public Socket forward;
    public Socket right;
    public Socket back;
    public Socket left;
    public Socket up;
    public Socket down;
    protected override void Start()
    {
        base.Start();
        ID = "Water";
        forward.ID = "Water";
        right.ID = "Water";
        back.ID = "Water";
        left.ID = "Water";
        up.ID = "Water";
        down.ID = "Water";
        UpdateModel("Water-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        bool f_Connected = forward.targetID == "Earth" || forward.targetID == "Plaza";
        bool r_Connected = right.targetID == "Earth" || right.targetID == "Plaza";
        bool b_Connected = back.targetID == "Earth" || back.targetID == "Plaza";
        bool l_Connected = left.targetID == "Earth" || left.targetID == "Plaza";
        if (f_Connected && !r_Connected && !b_Connected && !l_Connected)
        { UpdateModel("Water-1"); }
        else if (!f_Connected && r_Connected && !b_Connected && !l_Connected)
        { UpdateModel("Water-2"); }
        else if (!f_Connected && !r_Connected && b_Connected && !l_Connected)
        { UpdateModel("Water-3"); }
        else if (!f_Connected && !r_Connected && !b_Connected && l_Connected)
        { UpdateModel("Water-4"); }
        else if (f_Connected && r_Connected && !b_Connected && !l_Connected)
        { UpdateModel("Water-5"); }
        else if (!f_Connected && r_Connected && b_Connected && !l_Connected)
        { UpdateModel("Water-6"); }
        else if (!f_Connected && !r_Connected && b_Connected && l_Connected)
        { UpdateModel("Water-7"); }
        else if (f_Connected && !r_Connected && !b_Connected && l_Connected)
        { UpdateModel("Water-8"); }
        else if (f_Connected && !r_Connected && b_Connected && !l_Connected)
        { UpdateModel("Water-9"); }
        else if (!f_Connected && r_Connected && !b_Connected && l_Connected)
        { UpdateModel("Water-10"); }
        else if (f_Connected && r_Connected && b_Connected && !l_Connected)
        { UpdateModel("Water-11"); }
        else if (!f_Connected && r_Connected && b_Connected && l_Connected)
        { UpdateModel("Water-12"); }
        else if (f_Connected && !r_Connected && b_Connected && l_Connected)
        { UpdateModel("Water-13"); }
        else if (f_Connected && r_Connected && !b_Connected && l_Connected)
        { UpdateModel("Water-14"); }
        else if (f_Connected && r_Connected && b_Connected && l_Connected)
        { UpdateModel("Water-15"); }
        else { UpdateModel("Water-0"); }
    }
}
