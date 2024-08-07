using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Plaza : Chunk, IChunkObserver
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
        ID = "Plaza";
        forward.ID = "Plaza";
        right.ID = "Plaza";
        back.ID = "Plaza";
        left.ID = "Plaza";
        up.ID = "Plaza";
        down.ID = "Earth";
        UpdateModel("Plaza-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        if (
        forward.targetID == "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID != "Plaza"
        )
        { UpdateModel("Plaza-1"); }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID != "Plaza"
        )
        { UpdateModel("Plaza-2"); }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID != "Plaza"
        )
        { UpdateModel("Plaza-3"); }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID == "Plaza"
        )
        { UpdateModel("Plaza-4"); }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID != "Plaza"
        )
        { UpdateModel("Plaza-5"); }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID == "Plaza"
        )
        { UpdateModel("Plaza-6"); }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID != "Plaza"
        )
        {
            Vector3Int cornerCoord = GetCornerCoord(0);
            if (ChunkManager.Instance.chunks.ContainsKey(cornerCoord) && ChunkManager.Instance.chunks[cornerCoord].ID == "Plaza") { UpdateModel("Plaza-16"); }
            else { UpdateModel("Plaza-7"); }
        }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID != "Plaza"
        )
        {
            Vector3Int cornerCoord = GetCornerCoord(1);
            if (ChunkManager.Instance.chunks.ContainsKey(cornerCoord) && ChunkManager.Instance.chunks[cornerCoord].ID == "Plaza") { UpdateModel("Plaza-17"); }
            else { UpdateModel("Plaza-8"); }
        }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int cornerCoord = GetCornerCoord(2);
            if (ChunkManager.Instance.chunks.ContainsKey(cornerCoord) && ChunkManager.Instance.chunks[cornerCoord].ID == "Plaza") { UpdateModel("Plaza-18"); }
            else { UpdateModel("Plaza-9"); }
        }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int cornerCoord = GetCornerCoord(3);
            if (ChunkManager.Instance.chunks.ContainsKey(cornerCoord) && ChunkManager.Instance.chunks[cornerCoord].ID == "Plaza") { UpdateModel("Plaza-19"); }
            else { UpdateModel("Plaza-10"); }
        }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID != "Plaza"
        )
        {
            Vector3Int corner_0 = GetCornerCoord(0);
            Vector3Int corner_1 = GetCornerCoord(1);
            if (ChunkManager.Instance.chunks.ContainsKey(corner_0) && ChunkManager.Instance.chunks[corner_0].ID == "Plaza")
            {
                if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
                { UpdateModel("Plaza-28"); }
                else
                { UpdateModel("Plaza-20"); }
            }
            else if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
            { UpdateModel("Plaza-24"); }
            else { UpdateModel("Plaza-11"); }
        }
        else if (
        forward.targetID != "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int corner_0 = GetCornerCoord(1);
            Vector3Int corner_1 = GetCornerCoord(2);
            if (ChunkManager.Instance.chunks.ContainsKey(corner_0) && ChunkManager.Instance.chunks[corner_0].ID == "Plaza")
            {
                if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
                { UpdateModel("Plaza-29"); }
                else
                { UpdateModel("Plaza-21"); }
            }
            else if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
            { UpdateModel("Plaza-25"); }
            else { UpdateModel("Plaza-12"); }
        }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID != "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int corner_0 = GetCornerCoord(2);
            Vector3Int corner_1 = GetCornerCoord(3);
            if (ChunkManager.Instance.chunks.ContainsKey(corner_0) && ChunkManager.Instance.chunks[corner_0].ID == "Plaza")
            {
                if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
                { UpdateModel("Plaza-30"); }
                else
                { UpdateModel("Plaza-22"); }
            }
            else if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
            { UpdateModel("Plaza-26"); }
            else { UpdateModel("Plaza-13"); }
        }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID != "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int corner_0 = GetCornerCoord(3);
            Vector3Int corner_1 = GetCornerCoord(0);
            if (ChunkManager.Instance.chunks.ContainsKey(corner_0) && ChunkManager.Instance.chunks[corner_0].ID == "Plaza")
            {
                if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
                { UpdateModel("Plaza-31"); }
                else
                { UpdateModel("Plaza-23"); }
            }
            else if (ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza")
            { UpdateModel("Plaza-27"); }
            else { UpdateModel("Plaza-14"); }
        }
        else if (
        forward.targetID == "Plaza" &&
        right.targetID == "Plaza" &&
        back.targetID == "Plaza" &&
        left.targetID == "Plaza"
        )
        {
            Vector3Int corner_0 = GetCornerCoord(0);
            Vector3Int corner_1 = GetCornerCoord(1);
            Vector3Int corner_2 = GetCornerCoord(2);
            Vector3Int corner_3 = GetCornerCoord(3);
            bool corner_0_active = ChunkManager.Instance.chunks.ContainsKey(corner_0) && ChunkManager.Instance.chunks[corner_0].ID == "Plaza";
            bool corner_1_active = ChunkManager.Instance.chunks.ContainsKey(corner_1) && ChunkManager.Instance.chunks[corner_1].ID == "Plaza";
            bool corner_2_active = ChunkManager.Instance.chunks.ContainsKey(corner_2) && ChunkManager.Instance.chunks[corner_2].ID == "Plaza";
            bool corner_3_active = ChunkManager.Instance.chunks.ContainsKey(corner_3) && ChunkManager.Instance.chunks[corner_3].ID == "Plaza";
            if (corner_0_active && corner_1_active && corner_2_active && corner_3_active)
            { UpdateModel("Plaza-46"); }
            else if (corner_0_active && !corner_1_active && !corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-32"); }
            else if (!corner_0_active && corner_1_active && !corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-33"); }
            else if (!corner_0_active && !corner_1_active && corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-34"); }
            else if (!corner_0_active && !corner_1_active && !corner_2_active && corner_3_active)
            { UpdateModel("Plaza-35"); }
            else if (corner_0_active && corner_1_active && !corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-36"); }
            else if (!corner_0_active && corner_1_active && corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-37"); }
            else if (!corner_0_active && !corner_1_active && corner_2_active && corner_3_active)
            { UpdateModel("Plaza-38"); }
            else if (corner_0_active && !corner_1_active && !corner_2_active && corner_3_active)
            { UpdateModel("Plaza-39"); }
            else if (corner_0_active && !corner_1_active && corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-40"); }
            else if (!corner_0_active && corner_1_active && !corner_2_active && corner_3_active)
            { UpdateModel("Plaza-41"); }
            else if (corner_0_active && corner_1_active && corner_2_active && !corner_3_active)
            { UpdateModel("Plaza-42"); }
            else if (!corner_0_active && corner_1_active && corner_2_active && corner_3_active)
            { UpdateModel("Plaza-43"); }
            else if (corner_0_active && !corner_1_active && corner_2_active && corner_3_active)
            { UpdateModel("Plaza-44"); }
            else if (corner_0_active && corner_1_active && !corner_2_active && corner_3_active)
            { UpdateModel("Plaza-45"); }
            else { UpdateModel("Plaza-15"); }
        }
        else { UpdateModel("Plaza-0"); }
    }
}
