using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] private MeshRenderer insideRenderer;
    private Material cubeInside;
    private Vector2Int tilePosition;

    private void Awake()
    {
        cubeInside = insideRenderer.material;
    }

    public void SetTilePosition(int x, int y)
    {
        tilePosition = new Vector2Int(x, y);
    }

    public Vector2Int GetTilePosition()
    {
        return tilePosition;
    }

    public void SetInsideColor(Color color)
    {
        cubeInside.color = color;
    }
}


