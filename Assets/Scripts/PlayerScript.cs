using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MobileUnitBase
{
    [SerializeField] private ObstacleData obstacleData;
    [SerializeField] private GridGenerator gridGenerator;

    private void Start()
    {
        ReceiveInput = true;

        GameObject startingTile = GameManager.Instance.nodeArray[initialPosition.x, initialPosition.y].NodeTile;
        transform.position = startingTile.transform.position;
    }
    

    public void MoveToTile(Vector2Int destination)
    {
        GameManager.Instance.UpdateObstacles();
        Node[,] nodeArray = GameManager.Instance.nodeArray;
        
        if (nodeArray[destination.x, destination.y].HasObstacle)
        {
            // Destination has obstacle
            Debug.Log("Cannot move to tile with obstacle");
            return;
        }

        if (!ReceiveInput)
        {
            Debug.Log("Cannot move until next player turn");
            return;
        }
        
        Pathfinding.ClearTiles();
        
        List<Vector2Int> path = GameManager.Instance.Pathfinding.FindPathBetween(initialPosition, destination);
        
        if (path != null) StartCoroutine(FollowPath(path));
        else Debug.Log("Path not available");
    }

    public void SetInputReception(bool input)
    {
        ReceiveInput = input;
    }
}
