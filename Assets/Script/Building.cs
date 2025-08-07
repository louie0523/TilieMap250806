using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    /// <summary>
    /// 건물의 크기
    /// </summary>
    public Vector2Int size = new Vector2Int(2, 2);

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(size.x, 1, size.y);
    }

    public void SetBuildingSize(int buildingSize)
    {
        size = new Vector2Int(buildingSize, buildingSize);
        this.gameObject.transform.localScale = new Vector3(buildingSize, 1, buildingSize);
    }

}
