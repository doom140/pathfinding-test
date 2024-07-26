using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MobileUnitBase
{
    private void Start()
    {
        GameObject startingTile = GameManager.Instance.nodeArray[initialPosition.x, initialPosition.y].NodeTile;
        transform.position = startingTile.transform.position;
    }

    public void ReachPlayer()
    {
        GameManager gameManager = GameManager.Instance;
        
        List<Vector2Int> path =
        GetShortestPath(GetPlayerAdjacentTiles(gameManager.nodeArray, gameManager.player.GetCurrentPos()));
        if (path == null)
        {
            Debug.Log("Player unreachable");
            gameManager.UpdateState(GameManager.GameState.PlayerTurn);
            return;
        }
        else
        {
            // recolor shortest path
            Pathfinding.ClearTiles();
            gameManager.Pathfinding.FindPathBetween(GetCurrentPos(), path[^1]);
            
            GameManager.Instance.UpdateObstacles();
            
            StartCoroutine(FollowPath(path));
        }
    }
    
    private List<Node> GetPlayerAdjacentTiles(Node[,] nodeArray, Vector2Int playerTile)
    {
        List<Node> adjacentTiles = new List<Node>();
        Vector2Int playerPos = playerTile;
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // skip centre tile 
                if (x == 0 && y == 0) continue; 
                // skip diagonal neighbours 
                if (Mathf.Abs(x) - Mathf.Abs(y) == 0) continue;
                
                // checking validity
                int testX = playerTile.x + x;
                if (testX >= 0 && testX < GameManager.Instance.obstacleData.width)
                {
                    int testY = playerTile.y + y;
                    if (testY >= 0 && testY < GameManager.Instance.obstacleData.height)
                    {
                        if (!nodeArray[testX, testY].HasObstacle)
                        {
                            adjacentTiles.Add(nodeArray[testX, testY]);
                        }
                    }
                }
            }
        }

        return adjacentTiles;
    }

    private List<Vector2Int> GetShortestPath(List<Node> targetTiles)
    {
        GameManager.Instance.UpdateObstacles();
        List<Vector2Int> shortestPath = null;
        foreach (Node node in targetTiles)
        {
            List<Vector2Int> thisPath = GameManager.Instance.Pathfinding.FindPathBetween(CurrentPosition, node.Position);
            if (thisPath != null)
            {
                if (shortestPath == null) shortestPath = thisPath;
                else if (thisPath.Count < shortestPath.Count) shortestPath = thisPath;
            }
        }
        return shortestPath;
    }
    
}
