
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IEnemyAi
{
    
    public Vector2Int GetTargetTile(Node[,] nodeArray, Vector2Int currentTile, Vector2Int playerTile)
    {
        
        return Vector2Int.zero;
    }

    public List<Node> GetPlayerAdjacentTiles(Node[,] nodeArray, Node playerTile)
    {
        List<Node> adjacentTiles = new List<Node>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // skip centre tile 
                if (x == 0 && y == 0) continue; 
                // skip diagonal neighbours 
                if (Mathf.Abs(x) - Mathf.Abs(y) == 0) continue;
                
                adjacentTiles.Add(nodeArray[x, y]);
            }
        }

        return adjacentTiles;
    }
}
