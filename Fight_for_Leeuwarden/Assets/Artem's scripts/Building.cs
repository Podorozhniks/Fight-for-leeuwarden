using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRender;
    public Vector3Int Size = Vector3Int.one;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            MainRender.material.color = Color.green;
        }
        else
        {
            MainRender.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        MainRender.material.color = Color.white;
    }
    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(center: transform.position + new Vector3(x, y: 0, z: y), size: new Vector3(x: 1, y: .1F, z: 1));
            }

        }
    }
}
