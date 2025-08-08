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


  





}
