using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class MapLocation
{
    public int x;
    public int z;


}

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

    public int width = 10;
    public int height = 10;
    public bool[,] occupied;
    public bool isBuilding = false;

    public int[,] map;

    public List<MapLocation> directions = new List<MapLocation>();
    System.Random rng = new System.Random();


    private void Awake()
    {
        Instance = this;
        occupied = new bool[width, height];
        map = new int[width, height];
        MapLegnth();

        SetMapLocation();

        Generate(5, 5);

        TilemapGenerator.Instance.DrawMap();
    }

    void MapLegnth()
    {
       for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                map[i, j] = 1;
                occupied[i, j] = true;
            }
        }
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

    public void DestroyArea(Vector2Int start, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {

                occupied[start.x + x, start.y + y] = false;

            }
        }
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

    void Generate(int x, int z)
    {
        Debug.Log($"{map.GetLength(0)} : {map.GetLength(1)} ");
        Stack<Vector2Int> mapDatas = new Stack<Vector2Int>();
        int startX = x;
        int StartZ = z;
        map[startX, StartZ] = 0;
        mapDatas.Push(new Vector2Int(startX, StartZ));

        while (mapDatas.Count > 0)
        {
            Vector2Int current = mapDatas.Peek();
            Shuffle(directions);
            //이동이 가능한 곳인지 확인
            bool moved = false;

            foreach (MapLocation dir in directions)
            {
                int changeX = current.x + dir.x;
                int changeZ = current.y + dir.z;

                if (!(CountSquareNeighbours(changeX, changeZ) >= 2 || map[changeX, changeZ] == 0))
                {
                    map[changeX, changeZ] = 0;
                    occupied[changeX, changeZ] = false;
                    mapDatas.Push(new Vector2Int(changeX, changeZ));
                    moved = true;
                    break;
                } 
            }
            if (!moved)
            {
                mapDatas.Pop();
            }
        }

    }

    void SetMapLocation()
    {
        //오
        MapLocation location0 = new MapLocation();
        location0.x = 1;
        location0.z = 0;
        //클래스 내 함수로 값 등록
        //location0.SetMapLocation(1,0);
        directions.Add(location0);
        //위
        MapLocation location1 = new MapLocation();
        location1.x = 0;
        location1.z = 1;
        //클래스 내 함수로 값 등록
        //location0.SetMapLocation(0,1);
        directions.Add(location1);
        //왼
        MapLocation location2 = new MapLocation();
        location2.x = -1;
        location2.z = 0;
        //클래스 내 함수로 값 등록
        //location0.SetMapLocation(-1,0);
        directions.Add(location2);
        //아래
        MapLocation location3 = new MapLocation();
        location3.x = 0;
        location3.z = -1;
        directions.Add(location3);
        //클래스 내 함수로 값 등록
        ////↗
        //MapLocation location4 = new MapLocation();
        //location4.x = 1;
        //location4.z = 1;
        //directions.Add(location4);
        ////↖
        //MapLocation location5 = new MapLocation();
        //location5.x = -1;
        //location5.z = 1;
        //directions.Add(location5);
        ////↘
        //MapLocation location6 = new MapLocation();
        //location6.x = 1;
        //location6.z = -1;
        //directions.Add(location6);
        ////↙
        //MapLocation location7 = new MapLocation();
        //location6.x = -1;
        //location6.z = -1;
        //directions.Add(location7);


    }


    public void Shuffle(List<MapLocation> mapLocations)
    {
        int n = mapLocations.Count;
        //리스트의 크기만큼 반복
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            MapLocation value = mapLocations[k];
            mapLocations[k] = mapLocations[n];
            mapLocations[n] = value;
        }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= height - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        return count;
    }

    public int CountSleepSqaureNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= height - 1) return 5;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        return count;
    }

}
