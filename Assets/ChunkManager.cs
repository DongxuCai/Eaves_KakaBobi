using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChunkManager : Singleton<ChunkManager>
{
    [Header("全局变量")]
    public static Vector3 scale = new Vector3(5f, 2.5f, 5f);
    public static List<Vector3Int> cornerVectors = new List<Vector3Int>()
    { Vector3Int.forward + Vector3Int.right, Vector3Int.right + Vector3Int.back, Vector3Int.back + Vector3Int.left, Vector3Int.left + Vector3Int.forward };
    public static Vector3 GetCoordWorldPosition(Vector3Int coord)
    { return new Vector3(coord.x * scale.x, coord.y * scale.y, coord.z * scale.z); }
    public static Vector3Int GetWorldPositionCoord(Vector3 coord)
    { return new Vector3Int(Mathf.RoundToInt(coord.x / scale.x), Mathf.RoundToInt(coord.y / scale.y), Mathf.RoundToInt(coord.z / scale.z)); }
    [Header("放置平面")]
    public int planeHeight;
    public Plane plane;
    [Header("移动速度")]
    public static float dragSpeed = 0.2f;
    public static float rotateSpeed = 0.2f;
    public static float scaleSpeed = 0.2f;
    [Header("获取拼图块")]
    public Transform chunkContainer;
    public GameObject chunk_Prefab;
    [Header("拼图块数据")]
    public List<IChunkObserver> chunkObservers = new List<IChunkObserver>();
    public Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
    public List<Unit> units = new List<Unit>();
    public Dictionary<Vector3Int, Unit> chunkUnits = new Dictionary<Vector3Int, Unit>();
    [Header("鼠标选择")]
    public LayerMask layerMask;
    public RaycastHit raycastHit;
    [HideInInspector] public Chunk selectedChunk;
    [Header("拖拽拼图")]
    public float dragTriggerDistance;
    [HideInInspector] public Vector3 mouseStartPosition;
    public bool isDragging;
    [Header("能量")]
    public int energy;
    public TextMeshProUGUI energyText;
    public Transform popupContainer;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO 自动找到附近可放置的位置？OR 放进背包
            GameObject chunk_Instance = Instantiate(chunk_Prefab, chunkContainer);
            Chunk chunk = chunk_Instance.GetComponent<Chunk>();
            int index = Random.Range(0, ChunkLibrary.chunkInfoDic.Keys.Count);
            string ID = ChunkLibrary.chunkInfoDic.Keys.ToList()[index];
            // chunk.Initialize(ID);
        }
        // 旋转拼图块
        if (Physics.Raycast(PlayerController.Instance.ray, out raycastHit, Mathf.Infinity, layerMask) && Input.GetMouseButtonDown(1))
        { raycastHit.collider.GetComponentInParent<Chunk>().Rotate(); }
        // 左键选中拼图块
        if (selectedChunk == null)
        {
            if (Physics.Raycast(PlayerController.Instance.ray, out raycastHit, Mathf.Infinity, layerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectedChunk = raycastHit.collider.GetComponentInParent<Chunk>();
                    mouseStartPosition = Input.mousePosition;
                }
            }
        }
        else
        {
            // 左键单击改变拼图块形态
            if (!isDragging)
            {
                if (Vector3.Distance(mouseStartPosition, Input.mousePosition) > dragTriggerDistance)
                {
                    plane = new Plane(Vector3.up, selectedChunk.transform.position);
                    selectedChunk.Drag();
                    UpdateChunks();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("改变形态!");
                    selectedChunk.Click();
                    selectedChunk = null;
                }
            }
            // 左键拖拽移动拼图块
            else
            {
                if (Input.GetKeyUp(KeyCode.W)) { plane = new Plane(Vector3.up, selectedChunk.transform.position + Vector3.up * 2.5f); }
                if (Input.GetKeyUp(KeyCode.S)) { plane = new Plane(Vector3.up, selectedChunk.transform.position - Vector3.up * 2.5f); }
                if (plane.Raycast(PlayerController.Instance.ray, out float enter))
                {
                    Vector3Int targetCoord = GetWorldPositionCoord(Tool.AttachToGrid(PlayerController.Instance.ray.GetPoint(enter), scale));
                    if (targetCoord != selectedChunk.coord && !chunks.ContainsKey(targetCoord)) { selectedChunk.Move(targetCoord); }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    selectedChunk.Drop();
                    UpdateChunks();
                }
            }
        }
    }
    public void UpdateEnergy(int update)
    {
        energy += update;
        energyText.text = energy.ToString();
    }
    public void UpdateChunks()
    {
        Debug.Log("更新拼图!");
        foreach (var chunk in chunkObservers) { chunk.UpdateChunk(); }
    }
}
