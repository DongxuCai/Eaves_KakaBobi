using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SpiritSoilPlaceInfo
{
    public string ID;
    public Vector3Int coord;
    public int rotation;
    public SpiritSoilPlaceInfo(string ID, Vector3Int coord, int rotation)
    {
        this.ID = ID;
        this.coord = coord;
        this.rotation = rotation;
    }
}

public class SpiritSoilManager : Singleton<SpiritSoilManager>
{
    public static Vector3 scale = new Vector3(5f, 2.5f, 5f);
    public static Vector3Int GetWorldPositionCoord(Vector3 coord)
    { return new Vector3Int(Mathf.RoundToInt(coord.x / scale.x), Mathf.RoundToInt(coord.y / scale.y), Mathf.RoundToInt(coord.z / scale.z)); }
    public static int RotateRotation(int rotation, int degree) { rotation = rotation + degree > 2 ? rotation + degree - 4 : rotation + degree; return rotation; }

    public Dictionary<Vector3Int, SpiritSoilPlaceInfo> spiritSoilDic = new Dictionary<Vector3Int, SpiritSoilPlaceInfo>();
    public List<Vector3Int> gridOccupation = new List<Vector3Int>();
    [Header("鼠标选择")]
    public LayerMask layerMask;
    public RaycastHit raycastHit;

    [Header("菜单")]
    public GameObject defaultMenu;
    public GameObject spiritSoilMenu;
    public GameObject selectedSpiritSoilBasicInfoPanel;
    public SpiritSoilAdjustmentMenu spiritSoilAdjustmentMenu;

    public GameObject grid;
    public GameObject cursor;
    public GameObject spiritSoil_Prefab;

    [Header("引例存放位置")]
    public Transform earthSpiritContainer;
    public Transform elementContainer;

    [Header("基准平面")]
    public int level;
    public Plane levelPlane;
    public SpiritSoil selectedSpiritSoil;
    public bool spiritSoilSelected;
    private void Start()
    {
        defaultMenu.SetActive(true);
        spiritSoilMenu.SetActive(false);
        selectedSpiritSoilBasicInfoPanel.SetActive(false);
        spiritSoilAdjustmentMenu.Close();
        grid.SetActive(false);
        cursor.SetActive(false);
        selectedSpiritSoil = null;
    }
    private void Update()
    {
        if (spiritSoilSelected)
        {
            if (levelPlane.Raycast(PlayerController.Instance.ray, out float enter) && !EventSystem.current.IsPointerOverGameObject())
            {
                cursor.transform.position = Tool.AttachToGrid(PlayerController.Instance.ray.GetPoint(enter), scale);
                cursor.SetActive(true);
            }
            else { cursor.SetActive(false); }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3Int cursorCoord = GetWorldPositionCoord(cursor.transform.position);
                if (spiritSoilDic.ContainsKey(cursorCoord)) { }
                else
                {
                    if (selectedSpiritSoil == null)
                    {
                        GameObject spiritSoil_Instance = Instantiate(spiritSoil_Prefab, transform);
                        selectedSpiritSoil = spiritSoil_Instance.GetComponent<SpiritSoil>();
                        selectedSpiritSoil.Select();
                    }
                    selectedSpiritSoil.transform.position = cursor.transform.position;
                    selectedSpiritSoil.coord = GetWorldPositionCoord(cursor.transform.position);
                }
            }
            if (Input.mouseScrollDelta.y > 0) { UpdateLevelPlane(level + 1); }
            if (Input.mouseScrollDelta.y < 0) { UpdateLevelPlane(level - 1); }
        }
        if (Physics.Raycast(PlayerController.Instance.ray, out raycastHit, Mathf.Infinity, layerMask) && selectedSpiritSoil == null && !spiritSoilSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (raycastHit.collider.GetComponentInParent<SpiritSoil>()) { SelectSpiritSoil(); }
                else { raycastHit.collider.GetComponentInParent<EarthSpirit>().GetElement(); }
            }
        }
    }
    #region 息壤菜单
    public void OpenSpiritSoilMenu()
    {
        spiritSoilMenu.SetActive(true);
        defaultMenu.SetActive(false);
    }
    public void CloseSpiritSoilMenu()
    {
        defaultMenu.SetActive(true);
        spiritSoilMenu.SetActive(false);
    }
    public void SelectSpiritSoilFromMenu()
    {
        spiritSoilMenu.SetActive(false);
        spiritSoilAdjustmentMenu.Open(true, true, "ID");
        grid.SetActive(true);
        cursor.SetActive(true);
        spiritSoilSelected = true;

        UpdateLevelPlane(level);
    }
    #endregion
    #region 选择场景中的息壤
    public void SelectSpiritSoil()
    {
        // 选择息壤
        selectedSpiritSoil = raycastHit.collider.GetComponentInParent<SpiritSoil>();
        selectedSpiritSoil.Select();
        spiritSoilDic.Remove(selectedSpiritSoil.coord);

        // 更新UI(菜单/基准平面和位置光标)
        defaultMenu.SetActive(false);
        spiritSoilAdjustmentMenu.Open(selectedSpiritSoil.shiftable, selectedSpiritSoil.rotatable, selectedSpiritSoil.ID);
        grid.SetActive(true);
        cursor.SetActive(true);
        UpdateLevelPlane(selectedSpiritSoil.coord.y);

        // 设置为选中息壤状态
        spiritSoilSelected = true;
    }
    public void RecycleSpiritSoil()
    {
        spiritSoilAdjustmentMenu.Close();

        defaultMenu.SetActive(true);
        Destroy(selectedSpiritSoil.gameObject);
        selectedSpiritSoil = null;
        spiritSoilSelected = false;
        grid.SetActive(false);
        cursor.SetActive(false);
    }
    public void DeselectSpiritSoil()
    {
        defaultMenu.SetActive(true);
        selectedSpiritSoil.Deselect();
        selectedSpiritSoil = null;
    }
    #endregion
    #region 息壤移动菜单
    public void Shift()
    {
        selectedSpiritSoil.Shift();
    }
    public void Rotate()
    {
        selectedSpiritSoil.Rotate();
    }
    public void Confirm()
    {
        // UI调整
        spiritSoilAdjustmentMenu.Close();
        grid.SetActive(false);
        cursor.SetActive(false);
        defaultMenu.SetActive(true);

        // 更新数据
        spiritSoilDic.Add(selectedSpiritSoil.coord, new SpiritSoilPlaceInfo(selectedSpiritSoil.ID, selectedSpiritSoil.coord, selectedSpiritSoil.rotation));

        // 取消选择
        selectedSpiritSoil.Deselect();
        selectedSpiritSoil = null;
        spiritSoilSelected = false;
    }
    public void UpdateLevelPlane(int level)
    {
        this.level = level;
        float height = level * scale.y;
        levelPlane = new Plane(Vector3.up, height * Vector3.up);
        grid.transform.position = height * Vector3.up;
    }
    #endregion

    public bool SpiritSoilCheck(Vector3Int coord, string ID)
    {
        return spiritSoilDic.ContainsKey(coord) && spiritSoilDic[coord].ID == ID;
    }
}
