using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBuildingKit : Equip
{
    public GameObject buildingWindow;
    private BuildingRecipe curRecipe;
    private BuildingPreview curBuildingPreview;

    public float placementUpdateRate = 0.03f;
    private float lastPlacementUpdateTime;
    public float placementMaxDistance = 4.0f;

    public LayerMask placementLayerMask;

    public Vector3 placementPosition;
    private bool canPlace;
    private float curYRot;

    public float rotateSpeed = 180f;

    private Camera cam;
    public static EquipBuildingKit instanceBuildingKit;

    private void Awake()
    {
        instanceBuildingKit = this;
        cam = Camera.main;
    }

    void Start()
    {
        buildingWindow = FindObjectOfType<BuildingWindow>(true).gameObject;
    }

    public override void OnAttackInput()
    {

    }

    public override void OnAltAttackInput()
    {
        buildingWindow.SetActive(true);
        PlayerController.instancePlayerController.ToggleCursor(true);
    }

    public void SetNewBuildingRecipe(BuildingRecipe buildingRecipe)
    {
        curRecipe = buildingRecipe;
        buildingWindow.SetActive(false);
        PlayerController.instancePlayerController.ToggleCursor(false);

        curBuildingPreview = Instantiate(buildingRecipe.previewPrefab).GetComponent<BuildingPreview>();
    }

    private void Update()
    {
        if(curRecipe != null && curBuildingPreview != null && Time.time - lastPlacementUpdateTime > placementUpdateRate)
        {
            lastPlacementUpdateTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, placementMaxDistance, placementLayerMask))
            {
                curBuildingPreview.transform.position = hit.point;
                curBuildingPreview.transform.up = hit.normal;
            }
        }
    }

    private void OnDestroy()
    {
        
    }
}
