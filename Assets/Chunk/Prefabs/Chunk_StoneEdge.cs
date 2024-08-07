using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_StoneEdge : Chunk, IChunkObserver
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
        ID = "Earth";
        forward.ID = "Earth";
        right.ID = "Earth";
        back.ID = "Earth";
        left.ID = "Earth";
        up.ID = "Earth";
        down.ID = "Earth";
        varieties = 3;
        index = 0;
        UpdateModel("StoneEdge-0");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    private void Update()
    {
        if (index == 0) { UpdateModel("StoneEdge-0"); }
        else if (index == 1) { UpdateModel("StoneEdge-1"); }
        else { UpdateModel("StoneEdge-2"); }
    }
    public void UpdateChunk()
    {

    }
}
