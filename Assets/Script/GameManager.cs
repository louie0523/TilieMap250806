using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

    public int width = 10;
    public int height = 10;
    public bool[,] occupied;
    public bool isBuilding = false;

    public int[,] map = new int[,]
    {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},


    };


    private void Awake()
    {
        Instance = this;
        width = map.GetLength(0);
        height = map.GetLength(1);
        occupied = new bool[width, height];
    }

    public bool IsAreaFree(Vector2Int start, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {

                int checkX = start.x + x;
                int checkY = start.y + y;   
                if(checkX < 0 || checkY < 0 || checkX >= width || checkY >= height)
                    return false;
                if (occupied[checkX, checkY])
                    return false;

            }
        }
        return true;
    }

    public void OccupyArea(Vector2Int start, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {

                occupied[start.x + x, start.y + y] = true;

            }
        }
    }
}
