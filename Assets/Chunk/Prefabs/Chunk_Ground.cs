using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Ground : Chunk, IChunkObserver
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
        UpdateModel("Ground");
        ChunkManager.Instance.chunkObservers.Add(this);
    }
    public void UpdateChunk()
    {
        
    }
}
