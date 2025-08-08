using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{

    /// <summary>
    /// 마우스를 따라다닐 건물 프리팹
    /// </summary>
    public GameObject buildingPrefab;

    /// <summary>
    /// 마우스를 따라 다닐 건물 오브젝트
    /// </summary>
    private GameObject previewObj;

    /// <summary>
    /// 생성될 건물의 데이터
    /// </summary>
    private Building builidngData;

    /// <summary>
    /// 마우스가 움직일때 생성 조건에 따라 변화되는 색깔 관리 스크립트
    /// </summary>
    public BuildingPreview previewScript;

    private int BuildingSize = 0;

    public void StartBuilding(int buildingsize)
    {
        previewObj = Instantiate(buildingPrefab);
        builidngData = previewObj.GetComponent<Building>();
        builidngData.SetBuildingSize(buildingsize);
        previewScript = previewObj.AddComponent<BuildingPreview>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.isBuilding = !GameManager.Instance.isBuilding;
            if(previewObj == null && GameManager.Instance.isBuilding)
            {
                BuildingSize = 1;
                StartBuilding(BuildingSize);
            } else if(previewObj != null && !GameManager.Instance.isBuilding)
            {
                Destroy(previewObj);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.isBuilding = !GameManager.Instance.isBuilding;
            if (previewObj == null && GameManager.Instance.isBuilding)
            {
                BuildingSize = 2;
                StartBuilding(BuildingSize);
            }
            else if (previewObj != null && !GameManager.Instance.isBuilding)
            {
                Destroy(previewObj);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.isBuilding = !GameManager.Instance.isBuilding;
            if (previewObj == null && GameManager.Instance.isBuilding)
            {
                BuildingSize = 3;
                StartBuilding(BuildingSize);
            }
            else if (previewObj != null && !GameManager.Instance.isBuilding)
            {
                Destroy(previewObj);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.Instance.isBuilding = !GameManager.Instance.isBuilding;
            if (previewObj == null && GameManager.Instance.isBuilding)
            {
                BuildingSize = 4;
                StartBuilding(BuildingSize);
            }
            else if (previewObj != null && !GameManager.Instance.isBuilding)
            {
                Destroy(previewObj);
            }
        }

        if (GameManager.Instance.isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Floor")))
            {
                Vector3 hitPoint = hit.point;
                Vector2Int gridPos = new Vector2Int(Mathf.FloorToInt(hitPoint.x), Mathf.FloorToInt(hitPoint.z));
                Vector3 displaypos = new Vector3(gridPos.x + builidngData.size.x - BuildingSize, 1,  gridPos.y + builidngData.size.y  -BuildingSize);
                previewObj.transform.position = displaypos;

                bool canPlace = GameManager.Instance.IsAreaFree(gridPos, builidngData.size);
                previewScript.SetColor(canPlace ? Color.green : Color.red);

                if(Input.GetMouseButtonDown(0) && canPlace)
                {
                    PlaceBuilding(gridPos);
                }

            }
        }

        if (!GameManager.Instance.isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Build")))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider != null)
                {
                    Building building = hit.collider.gameObject.GetComponent<Building>();
                    

                    if (Input.GetMouseButtonDown(0))
                    {
                            GameManager.Instance.DestroyArea(building.CreatePos, building.size);
                            Destroy(hit.collider.gameObject);
                        
                    }
                }



            }
        }


        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameObject[] deleteObj = GameObject.FindGameObjectsWithTag("Build");
            foreach(GameObject obj in deleteObj)
            {
                Destroy(obj);
            }

            for(int x = 0; x < GameManager.Instance.width; x++)
            {
                for(int z = 0; z < GameManager.Instance.height; z++)
                {
                    GameManager.Instance.map[x, z] = 0;
                }
            }
        }


    }

    void PlaceBuilding(Vector2Int gridPos)
    {
        Vector3 spawnPos = new Vector3(gridPos.x + builidngData.size.x - BuildingSize, 1, gridPos.y + builidngData.size.y - BuildingSize);
        GameObject creatBuilding = Instantiate(buildingPrefab, spawnPos, Quaternion.identity);
        Building building = creatBuilding.GetComponent<Building>();
        building.CreatePos = gridPos;
        creatBuilding.transform.name = "CreateBuilding";
        creatBuilding.GetComponent<Building>().SetBuildingSize(builidngData.size.x);
        GameManager.Instance.OccupyArea(gridPos, builidngData.size);
    }


}
