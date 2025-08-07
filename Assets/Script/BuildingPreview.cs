using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    private Renderer[] renderers;

    public bool isDestory = false;
    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetColor(Color color)
    {
        foreach(var r in renderers)
        {
            if(r.material.HasProperty("_Color"))
                r.material.color = color;
        }

        //if(color == Color.yellow)
        //{
        //    StartCoroutine(ColorBack());
        //}
    }

    IEnumerator ColorBack()
    {
        yield return new WaitWhile(() => isDestory);

        SetColor(Color.white);
    }

    
}
