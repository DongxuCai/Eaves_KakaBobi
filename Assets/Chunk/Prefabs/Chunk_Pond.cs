using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Pond : Chunk, IChunkObserver
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
        ID = "Pond";
        forward.ID = "Pond";
        right.ID = "Pond";
        back.ID = "Pond";
        left.ID = "Pond";
        up.ID = "Pond";
        down.ID = "Earth";
        UpdateModel("Pond-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        bool f_Connected = forward.targetID == "Pond";
        bool r_Connected = right.targetID == "Pond";
        bool b_Connected = back.targetID == "Pond";
        bool l_Connected = left.targetID == "Pond";
        bool fr_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(0)) && ChunkManager.Instance.chunks[GetCornerCoord(0)].ID == "Pond";
        bool rb_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(1)) && ChunkManager.Instance.chunks[GetCornerCoord(1)].ID == "Pond";
        bool bl_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(2)) && ChunkManager.Instance.chunks[GetCornerCoord(2)].ID == "Pond";
        bool lf_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(3)) && ChunkManager.Instance.chunks[GetCornerCoord(3)].ID == "Pond";

        if (f_Connected && !r_Connected && !b_Connected && !l_Connected) { UpdateModel("Pond-1"); }
        else if (!f_Connected && r_Connected && !b_Connected && !l_Connected) { UpdateModel("Pond-2"); }
        else if (!f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Pond-3"); }
        else if (!f_Connected && !r_Connected && !b_Connected && l_Connected) { UpdateModel("Pond-4"); }
        else if (f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Pond-5"); }
        else if (!f_Connected && r_Connected && !b_Connected && l_Connected) { UpdateModel("Pond-6"); }
        else if (f_Connected && r_Connected && !b_Connected && !l_Connected) { if (fr_Connected) UpdateModel("Pond-16"); else UpdateModel("Pond-7"); }
        else if (!f_Connected && r_Connected && b_Connected && !l_Connected) { if (rb_Connected) UpdateModel("Pond-17"); else UpdateModel("Pond-8"); }
        else if (!f_Connected && !r_Connected && b_Connected && l_Connected) { if (bl_Connected) UpdateModel("Pond-18"); else UpdateModel("Pond-9"); }
        else if (f_Connected && !r_Connected && !b_Connected && l_Connected) { if (lf_Connected) UpdateModel("Pond-19"); else UpdateModel("Pond-10"); }
        else if (f_Connected && r_Connected && b_Connected && !l_Connected)
        {
            if (fr_Connected && rb_Connected) UpdateModel("Pond-28");
            else if (fr_Connected && !rb_Connected) UpdateModel("Pond-20");
            else if (!fr_Connected && rb_Connected) UpdateModel("Pond-24");
            else UpdateModel("Pond-11");
        }
        else if (!f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (rb_Connected && bl_Connected) UpdateModel("Pond-29");
            else if (rb_Connected && !bl_Connected) UpdateModel("Pond-21");
            else if (!rb_Connected && bl_Connected) UpdateModel("Pond-25");
            else UpdateModel("Pond-12");
        }
        else if (f_Connected && !r_Connected && b_Connected && l_Connected)
        {
            if (bl_Connected && lf_Connected) UpdateModel("Pond-30");
            else if (bl_Connected && !lf_Connected) UpdateModel("Pond-22");
            else if (!bl_Connected && lf_Connected) UpdateModel("Pond-26");
            else UpdateModel("Pond-13");
        }
        else if (f_Connected && r_Connected && !b_Connected && l_Connected)
        {
            if (lf_Connected && fr_Connected) UpdateModel("Pond-31");
            else if (lf_Connected && !fr_Connected) UpdateModel("Pond-23");
            else if (!lf_Connected && fr_Connected) UpdateModel("Pond-27");
            else UpdateModel("Pond-14");
        }
        else if (f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Pond-46"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Pond-32"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Pond-33"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Pond-34"); }
            else if (!fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Pond-35"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Pond-36"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Pond-37"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Pond-38"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Pond-39"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Pond-40"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Pond-41"); }
            else if (fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Pond-42"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Pond-43"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Pond-44"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Pond-45"); }
            else { UpdateModel("Pond-15"); }
        }
        else { UpdateModel("Pond-0"); }
    }
}
