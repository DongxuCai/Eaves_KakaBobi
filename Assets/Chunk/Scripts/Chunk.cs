using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Chunk : MonoBehaviour
{
    [Header("基础数据")]
    public string ID;
    public Vector3Int coord;
    public int rotation;
    public int varieties;
    public int index;
    public List<Unit> units = new List<Unit>();
    [Header("模型模块")]
    public GameObject modelContainer;
    public GameObject model;
    public GameObject chunkCollider;
    [HideInInspector] public Animator animator;
    protected virtual void Start()
    {
        coord = ChunkManager.GetWorldPositionCoord(transform.position);
        rotation = 0;
        animator = GetComponent<Animator>();
        ChunkManager.Instance.chunks.Add(coord, this);
    }
    #region 鼠标互动操作
    public void Move(Vector3Int targetCoord) { StopAllCoroutines(); StartCoroutine(IE_Move(targetCoord)); }
    public IEnumerator IE_Move(Vector3Int targetCoord)
    {
        // 1. 计算原位置，目标坐标和实际位置
        Vector3 previousPosition = modelContainer.transform.position;
        coord = targetCoord;
        Vector3 targetPosition = ChunkManager.GetCoordWorldPosition(coord);
        // 2. 将整体移动至目标位置
        transform.position = targetPosition;
        // 3. 将模型跟随至目标位置
        modelContainer.transform.position = previousPosition;
        while (Vector3.Distance(modelContainer.transform.position, targetPosition) > 0.1f)
        {
            modelContainer.transform.position = Vector3.Lerp(modelContainer.transform.position, targetPosition, ChunkManager.dragSpeed);
            yield return new WaitForEndOfFrame();
        }
        modelContainer.transform.position = targetPosition;
    }
    public void Rotate() { StopAllCoroutines(); StartCoroutine(IE_Rotate()); }
    public IEnumerator IE_Rotate()
    {
        // 1. 计算原旋转，目标旋转
        Quaternion previousRotation = modelContainer.transform.rotation;
        rotation = rotation == 2 ? -1 : rotation + 1;
        // 2.将整体旋转至目标旋转
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation * 90f, 0));
        // 3. 将模型跟随旋转至目标位置
        modelContainer.transform.rotation = previousRotation;
        ChunkManager.Instance.UpdateChunks();
        while (Mathf.Abs(modelContainer.transform.rotation.y - rotation * 90f) > 0.01f)
        {
            modelContainer.transform.rotation = Quaternion.Slerp(modelContainer.transform.rotation, Quaternion.Euler(new Vector3(0, rotation * 90f, 0)), ChunkManager.rotateSpeed);
            yield return new WaitForEndOfFrame();
        }
        modelContainer.transform.rotation = Quaternion.Euler(new Vector3(0, rotation * 90f, 0));
    }
    public void Click() { StopAllCoroutines(); StartCoroutine(IE_Click()); }
    public IEnumerator IE_Click()
    {
        modelContainer.transform.localScale = Vector3.one * 0.9f;
        index = index == varieties - 1 ? 0 : index + 1;
        while (Vector3.Distance(modelContainer.transform.localScale, Vector3.one) > 0.01f)
        {
            modelContainer.transform.localScale = Vector3.Lerp(modelContainer.transform.localScale, Vector3.one, ChunkManager.scaleSpeed);
            yield return new WaitForEndOfFrame();
        }
        modelContainer.transform.localScale = Vector3.one;
    }
    #endregion

    public Vector3Int GetCornerCoord(int index)
    {
        int rotatedIndex = index + rotation;
        if (rotatedIndex > 3) { rotatedIndex -= 4; }
        else if (rotatedIndex < 0) { rotatedIndex += 4; }
        return coord + ChunkManager.cornerVectors[rotatedIndex];
    }
    public void UpdateModel(string modelID)
    {
        model.GetComponent<MeshFilter>().mesh = ChunkLibrary.chunkModelDic[modelID].GetComponent<MeshFilter>().sharedMesh;
        model.GetComponent<MeshRenderer>().materials = ChunkLibrary.chunkModelDic[modelID].GetComponent<MeshRenderer>().sharedMaterials;
        model.GetComponent<MeshCollider>().sharedMesh = ChunkLibrary.chunkModelDic[modelID].GetComponent<MeshFilter>().sharedMesh;
    }
    public void Drag()
    {
        ChunkManager.Instance.isDragging = true;
        animator.SetBool("isDragging", true);

        ChunkManager.Instance.chunks.Remove(coord);
    }
    public void Drop()
    {
        ChunkManager.Instance.isDragging = false;
        ChunkManager.Instance.selectedChunk = null;
        animator.SetBool("isDragging", false);

        ChunkManager.Instance.chunks.Add(coord, this);
    }
}
