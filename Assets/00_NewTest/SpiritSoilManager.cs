using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiritSoilManager : Singleton<SpiritSoilManager>
{
    public static Vector3 scale = new Vector3(5f, 2.5f, 5f);
    public static Vector3Int GetWorldPositionCoord(Vector3 coord)
    { return new Vector3Int(Mathf.RoundToInt(coord.x / scale.x), Mathf.RoundToInt(coord.y / scale.y), Mathf.RoundToInt(coord.z / scale.z)); }
    [Header("鼠标选择")]
    public LayerMask layerMask;
    public RaycastHit raycastHit;
    
    [Header("菜单")]
    public GameObject defaultMenu;
    public GameObject spiritSoilMenu;
    public GameObject selectedSpiritSoilBasicInfoPanel;
    public GameObject spiritSoilAdjustmentMenu;

    public GameObject grid;
    public GameObject cursor;
    public GameObject spiritSoil_Prefab;
    [Header("基准平面")]
    public int level;
    public Plane levelPlane;
    public SpiritSoil selectedSpiritSoil;
    public bool isMoving;
    private void Start()
    {
        defaultMenu.SetActive(true);
        spiritSoilMenu.SetActive(false);
        selectedSpiritSoilBasicInfoPanel.SetActive(false);
        spiritSoilAdjustmentMenu.SetActive(false);
        grid.SetActive(false);
        cursor.SetActive(false);
        selectedSpiritSoil = null;
    }
    private void Update()
    {
        if (isMoving)
        {
            if (levelPlane.Raycast(PlayerController.Instance.ray, out float enter) && !EventSystem.current.IsPointerOverGameObject())
            {
                cursor.transform.position = Tool.AttachToGrid(PlayerController.Instance.ray.GetPoint(enter), scale);
                cursor.SetActive(true);
            }
            else { cursor.SetActive(false); }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(selectedSpiritSoil == null) 
                { 
                    GameObject spiritSoil_Instance = Instantiate(spiritSoil_Prefab, transform); 
                    selectedSpiritSoil = spiritSoil_Instance.GetComponent<SpiritSoil>();
                    selectedSpiritSoil.Select();
                }
                selectedSpiritSoil.transform.position = cursor.transform.position;
                selectedSpiritSoil.coord = GetWorldPositionCoord(cursor.transform.position);
            }
        }
        if (Physics.Raycast(PlayerController.Instance.ray, out raycastHit, Mathf.Infinity, layerMask) && selectedSpiritSoil == null && !isMoving)
        {
            if (Input.GetMouseButtonDown(0)) { SelectSpiritSoil(); }
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
        spiritSoilAdjustmentMenu.SetActive(true);
        grid.SetActive(true);
        cursor.SetActive(true);
        isMoving = true;

        UpdateLevelPlane(level);
    }
    #endregion
    #region 选择场景中的息壤
    public void SelectSpiritSoil()
    {
        defaultMenu.SetActive(false);

        selectedSpiritSoil = raycastHit.collider.GetComponentInParent<SpiritSoil>();
        selectedSpiritSoil.Select();
        selectedSpiritSoilBasicInfoPanel.SetActive(true);
        spiritSoilAdjustmentMenu.SetActive(true);
        grid.SetActive(true);
        cursor.SetActive(true);
        isMoving = true;

        UpdateLevelPlane(selectedSpiritSoil.coord.y);
    }
    public void EnterAdjustMode()
    {
        spiritSoilAdjustmentMenu.SetActive(true);
        grid.SetActive(true);
        cursor.SetActive(true);
        isMoving = true;

        UpdateLevelPlane(selectedSpiritSoil.coord.y);
    }
    public void RecycleSpiritSoil()
    {
        selectedSpiritSoilBasicInfoPanel.SetActive(false);
        spiritSoilAdjustmentMenu.SetActive(false);

        defaultMenu.SetActive(true);
        Destroy(selectedSpiritSoil.gameObject);
        selectedSpiritSoil = null;
        isMoving = false;
        grid.SetActive(false);
        cursor.SetActive(false);
    }
    public void DeselectSpiritSoil()
    {
        selectedSpiritSoilBasicInfoPanel.SetActive(false);
        defaultMenu.SetActive(true);
        selectedSpiritSoil.Deselect();
        selectedSpiritSoil = null;
    }
    #endregion
    #region 息壤移动菜单
    public void Shift()
    {

    }
    public void Rotate()
    {
        selectedSpiritSoil.Rotate();
    }
    public void Confirm()
    {
        selectedSpiritSoilBasicInfoPanel.SetActive(false);
        spiritSoilAdjustmentMenu.SetActive(false);
        grid.SetActive(false);
        cursor.SetActive(false);
        defaultMenu.SetActive(true);
        selectedSpiritSoil.Deselect();
        selectedSpiritSoil = null;
        isMoving = false;
    }
    public void LevelPlaneUp()
    {
        UpdateLevelPlane(level + 1);
    }
    public void LevelPlaneDown()
    {
        UpdateLevelPlane(level - 1);
    }
    public void UpdateLevelPlane(int level)
    {
        this.level = level;
        float height = level * scale.y;
        levelPlane = new Plane(Vector3.up, height * Vector3.up);
        grid.transform.position = height * Vector3.up;
    }
    #endregion
}
