using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Grass : Chunk, IChunkObserver
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
        ID = "Grass";
        forward.ID = "Grass";
        right.ID = "Grass";
        back.ID = "Grass";
        left.ID = "Grass";
        up.ID = "Grass";
        down.ID = "Earth";
        UpdateModel("Grass-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        bool f_Connected = forward.targetID == "Grass";
        bool r_Connected = right.targetID == "Grass";
        bool b_Connected = back.targetID == "Grass";
        bool l_Connected = left.targetID == "Grass";
        bool fr_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(0)) && ChunkManager.Instance.chunks[GetCornerCoord(0)].ID == "Grass";
        bool rb_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(1)) && ChunkManager.Instance.chunks[GetCornerCoord(1)].ID == "Grass";
        bool bl_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(2)) && ChunkManager.Instance.chunks[GetCornerCoord(2)].ID == "Grass";
        bool lf_Connected = ChunkManager.Instance.chunks.ContainsKey(GetCornerCoord(3)) && ChunkManager.Instance.chunks[GetCornerCoord(3)].ID == "Grass";

        if (f_Connected && !r_Connected && !b_Connected && !l_Connected) { UpdateModel("Grass-1"); }
        else if (!f_Connected && r_Connected && !b_Connected && !l_Connected) { UpdateModel("Grass-2"); }
        else if (!f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Grass-3"); }
        else if (!f_Connected && !r_Connected && !b_Connected && l_Connected) { UpdateModel("Grass-4"); }
        else if (f_Connected && !r_Connected && b_Connected && !l_Connected) { UpdateModel("Grass-5"); }
        else if (!f_Connected && r_Connected && !b_Connected && l_Connected) { UpdateModel("Grass-6"); }
        else if (f_Connected && r_Connected && !b_Connected && !l_Connected) { if (fr_Connected) UpdateModel("Grass-16"); else UpdateModel("Grass-7"); }
        else if (!f_Connected && r_Connected && b_Connected && !l_Connected) { if (rb_Connected) UpdateModel("Grass-17"); else UpdateModel("Grass-8"); }
        else if (!f_Connected && !r_Connected && b_Connected && l_Connected) { if (bl_Connected) UpdateModel("Grass-18"); else UpdateModel("Grass-9"); }
        else if (f_Connected && !r_Connected && !b_Connected && l_Connected) { if (lf_Connected) UpdateModel("Grass-19"); else UpdateModel("Grass-10"); }
        else if (f_Connected && r_Connected && b_Connected && !l_Connected)
        {
            if (fr_Connected && rb_Connected) UpdateModel("Grass-28");
            else if (fr_Connected && !rb_Connected) UpdateModel("Grass-20");
            else if (!fr_Connected && rb_Connected) UpdateModel("Grass-24");
            else UpdateModel("Grass-11");
        }
        else if (!f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (rb_Connected && bl_Connected) UpdateModel("Grass-29");
            else if (rb_Connected && !bl_Connected) UpdateModel("Grass-21");
            else if (!rb_Connected && bl_Connected) UpdateModel("Grass-25");
            else UpdateModel("Grass-12");
        }
        else if (f_Connected && !r_Connected && b_Connected && l_Connected)
        {
            if (bl_Connected && lf_Connected) UpdateModel("Grass-30");
            else if (bl_Connected && !lf_Connected) UpdateModel("Grass-22");
            else if (!bl_Connected && lf_Connected) UpdateModel("Grass-26");
            else UpdateModel("Grass-13");
        }
        else if (f_Connected && r_Connected && !b_Connected && l_Connected)
        {
            if (lf_Connected && fr_Connected) UpdateModel("Grass-31");
            else if (lf_Connected && !fr_Connected) UpdateModel("Grass-23");
            else if (!lf_Connected && fr_Connected) UpdateModel("Grass-27");
            else UpdateModel("Grass-14");
        }
        else if (f_Connected && r_Connected && b_Connected && l_Connected)
        {
            if (fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Grass-46"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Grass-32"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Grass-33"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Grass-34"); }
            else if (!fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Grass-35"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && !lf_Connected)
            { UpdateModel("Grass-36"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Grass-37"); }
            else if (!fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Grass-38"); }
            else if (fr_Connected && !rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Grass-39"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Grass-40"); }
            else if (!fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Grass-41"); }
            else if (fr_Connected && rb_Connected && bl_Connected && !lf_Connected)
            { UpdateModel("Grass-42"); }
            else if (!fr_Connected && rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Grass-43"); }
            else if (fr_Connected && !rb_Connected && bl_Connected && lf_Connected)
            { UpdateModel("Grass-44"); }
            else if (fr_Connected && rb_Connected && !bl_Connected && lf_Connected)
            { UpdateModel("Grass-45"); }
            else { UpdateModel("Grass-15"); }
        }
        else { UpdateModel("Grass-0"); }
    }
}
