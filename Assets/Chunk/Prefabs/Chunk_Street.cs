using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Street : Chunk, IChunkObserver
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
        ID = "Street";
        forward.ID = "Street";
        right.ID = "Street";
        back.ID = "Street";
        left.ID = "Street";
        up.ID = "Street";
        down.ID = "Earth";
        UpdateModel("Street-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        bool f_Connected = forward.targetID == "Street";
        bool r_Connected = right.targetID == "Street";
        bool b_Connected = back.targetID == "Street";
        bool l_Connected = left.targetID == "Street";
        bool fr_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(0)) && ChunkManager.Instance.chunks[GetCornerCoord(0)].ID == "Street";
        bool rb_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(1)) && ChunkManager.Instance.chunks[GetCornerCoord(1)].ID == "Street";
        bool bl_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(2)) && ChunkManager.Instance.chunks[GetCornerCoord(2)].ID == "Street";
        bool lf_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(3)) && ChunkManager.Instance.chunks[GetCornerCoord(3)].ID == "Street";

        if (f_Connected && !r_Connected && !b_Connected && !l_Connected) { UpdateModel("Street-1"); }
        else if (!f_Connected && r_Connected && !b_Connected && !l_Connected) { UpdateModel("Street-2"); }
        else if (!f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Street-3"); }
        else if (!f_Connected && !r_Connected && !b_Connected && l_Connected) { UpdateModel("Street-4"); }
        else if (f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Street-5"); }
        else if (!f_Connected && r_Connected && !b_Connected && l_Connected) { UpdateModel("Street-6"); }
        else if (f_Connected && r_Connected && !b_Connected && !l_Connected) { if (fr_Connected) UpdateModel("Street-16"); else UpdateModel("Street-7"); }
        else if (!f_Connected && r_Connected && b_Connected && !l_Connected) { if (rb_Connected) UpdateModel("Street-17"); else UpdateModel("Street-8"); }
        else if (!f_Connected && !r_Connected && b_Connected && l_Connected) { if (bl_Connected) UpdateModel("Street-18"); else UpdateModel("Street-9"); }
        else if (f_Connected && !r_Connected && !b_Connected && l_Connected) { if (lf_Connected) UpdateModel("Street-19"); else UpdateModel("Street-10"); }
        else if (f_Connected && r_Connected && b_Connected && !l_Connected)
        {
            if (fr_Connected && rb_Connected) UpdateModel("Street-28");
            else if (fr_Connected && !rb_Connected) UpdateModel("Street-20");
            else if (!fr_Connected && rb_Connected) UpdateModel("Street-24");
            else UpdateModel("Street-11");
        }
        else if (!f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (rb_Connected && bl_Connected) UpdateModel("Street-29");
            else if (rb_Connected && !bl_Connected) UpdateModel("Street-21");
            else if (!rb_Connected && bl_Connected) UpdateModel("Street-25");
            else UpdateModel("Street-12");
        }
        else if (f_Connected && !r_Connected && b_Connected && l_Connected)
        {
            if (bl_Connected && lf_Connected) UpdateModel("Street-30");
            else if (bl_Connected && !lf_Connected) UpdateModel("Street-22");
            else if (!bl_Connected && lf_Connected) UpdateModel("Street-26");
            else UpdateModel("Street-13");
        }
        else if (f_Connected && r_Connected && !b_Connected && l_Connected)
        {
            if (lf_Connected && fr_Connected) UpdateModel("Street-31");
            else if (lf_Connected && !fr_Connected) UpdateModel("Street-23");
            else if (!lf_Connected && fr_Connected) UpdateModel("Street-27");
            else UpdateModel("Street-14");
        }
        else if (f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Street-46"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Street-32"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Street-33"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Street-34"); }
            else if (!fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Street-35"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Street-36"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Street-37"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Street-38"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Street-39"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Street-40"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Street-41"); }
            else if (fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Street-42"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Street-43"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Street-44"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Street-45"); }
            else { UpdateModel("Street-15"); }
        }
        else { UpdateModel("Street-0"); }
    }
}
