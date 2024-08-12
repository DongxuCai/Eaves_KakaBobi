using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SpiritSoil : MonoBehaviour
{
    [Header("基础数据")]
    public string ID;
    public Vector3Int coord;
    [Header("切换形态")]
    public bool shiftable;
    public int index;
    public int varieties;
    [Header("旋转调整")]
    public bool rotatable;
    public int rotation;

    [Header("状态列表")]
    public bool selected;
    public bool toppest;
    public bool earthSpiritOccupied;

    [Header("地灵系统")]
    public GameObject earthSpirit_prefab;
    public Transform spawnPoint;

    [Header("游戏对象")]
    public GameObject modelContainer;
    public Animator animator;

    private void Start()
    {
        coord = SpiritSoilManager.GetWorldPositionCoord(transform.position);
        rotation = 0;
        SpiritSoilPlaceInfo info = new SpiritSoilPlaceInfo(ID, coord, rotation);
        SpiritSoilManager.Instance.spiritSoilDic.Add(coord, info);

        StartCoroutine(GenerateEarthSpirit());
    }
    public IEnumerator GenerateEarthSpirit()
    {
        float random = 0;
        while (random < 99.9f || SpiritSoilManager.Instance.spiritSoilSelected || SpiritSoilManager.Instance.earthSpiritContainer.childCount > 5)
        {
            random = Random.Range(0f, 100f);
            yield return new WaitForSeconds(0.5f);
        }
        GameObject earthSpirit_Instance = Instantiate(earthSpirit_prefab, SpiritSoilManager.Instance.earthSpiritContainer);
        earthSpirit_Instance.transform.position = spawnPoint.position;
        earthSpiritOccupied = true;
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

    public void Shift()
    {
        index = index == varieties - 1 ? 0 : index + 1;
        UpdateModel(ID + '-' + index);
    }
}
