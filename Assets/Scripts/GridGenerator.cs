using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tileCube;
    [SerializeField] private Transform gridContainer;
    public float tileSize;

    private GameObject[,] tiles;

    private void Awake()
    {
        GenerateGrid(10, 10, tileCube);
    }

    private void Start()
    {
        
    }
    
    private void GenerateGrid(int width, int height, GameObject tile)
    {
        tiles = new GameObject[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject currentTile = Instantiate(tile, new Vector3(x + tileSize / 2, 0f, y + tileSize / 2), tile.transform.rotation, gridContainer);
                
                // set tile position and layer mask
                currentTile.GetComponent<TileData>().SetTilePosition(x, y);
                currentTile.layer = LayerMask.NameToLayer("Grid");
                
                // store tile reference in array
                tiles[x, y] = currentTile;
            }
        }
    }

    public GameObject[,] GetTileGrid()
    {
        return tiles;
    }
}
