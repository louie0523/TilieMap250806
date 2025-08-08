using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TilemapGenerator : MonoBehaviour
{
    public static TilemapGenerator Instance;
    public Transform Map;
    public GameObject floorTile;
    public GameObject wallTile;

    public int[,] map;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }


    public void DrawMap()
    {
        map = GameManager.Instance.map;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector3Int floorTilePos = new Vector3Int(x, 0, y);
                if (map[x, y] == 1)
                {
                    Instantiate(floorTile, floorTilePos, floorTile.transform.rotation, Map);
                    Instantiate(wallTile, floorTilePos + new Vector3Int(0, 1, 0), floorTile.transform.rotation, Map);
                    Building building = wallTile.GetComponent<Building>();
                    building.CreatePos = new Vector2Int(x, y);
                }
                else
                {
                    Instantiate(floorTile, floorTilePos, floorTile.transform.rotation, Map);
                }
                Debug.Log(map[x, y]);
            }
        }
    }

  





}
