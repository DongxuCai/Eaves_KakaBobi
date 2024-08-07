using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SpiritSoil : MonoBehaviour
{
    [Header("基础数据")]
    public string ID;
    public Vector3Int coord;
    public int rotation;
    public bool selected;
    public GameObject modelContainer;
    public Animator animator;

    private void Start()
    {
        coord = SpiritSoilManager.GetWorldPositionCoord(transform.position);
        rotation = 0;
        SpiritSoilPlaceInfo info = new SpiritSoilPlaceInfo(ID, coord, rotation);
        SpiritSoilManager.Instance.spiritSoilDic.Add(coord, info);
    }
    public void UpdateModel(string modelID)
    {
        modelContainer.GetComponent<MeshFilter>().mesh = SpiritSoilLibrary.modelDic[modelID].GetComponent<MeshFilter>().sharedMesh;
        modelContainer.GetComponent<MeshRenderer>().materials = SpiritSoilLibrary.modelDic[modelID].GetComponent<MeshRenderer>().sharedMaterials;
        modelContainer.GetComponent<MeshCollider>().sharedMesh = SpiritSoilLibrary.modelDic[modelID].GetComponent<MeshFilter>().sharedMesh;
    }

    public void Select()
    {
        selected = true;
        animator.SetBool("selected", true);
    }
    public void Deselect()
    {
        selected = false;
        animator.SetBool("selected", false);
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
        while (Mathf.Abs(modelContainer.transform.rotation.y - rotation * 90f) > 0.01f)
        {
            modelContainer.transform.rotation = Quaternion.Slerp(modelContainer.transform.rotation, Quaternion.Euler(new Vector3(0, rotation * 90f, 0)), ChunkManager.rotateSpeed);
            yield return new WaitForEndOfFrame();
        }
        modelContainer.transform.rotation = Quaternion.Euler(new Vector3(0, rotation * 90f, 0));
    }
}
