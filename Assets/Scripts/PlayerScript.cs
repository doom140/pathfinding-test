using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private Vector2Int initialPosition = new Vector2Int(0, 0);
    [SerializeField] private float movespeed = 0.2f;

    private Vector2Int currentPosition;
    private Pathfinding pathfinding;
    private Node[,] nodeArray;
    private bool receiveInput = true;

    private void Start()
    {
        pathfinding = new Pathfinding(obstacleData);
        pathfinding.PopulateTilesFromGrid(gridGenerator.GetTileGrid());
        nodeArray = pathfinding.GetNodeArray();

        GameObject startingTile = nodeArray[initialPosition.x, initialPosition.y].NodeTile;
        transform.position = startingTile.transform.position;
        currentPosition = initialPosition;
    }

    public void MoveToTile(Vector2Int destination)
    {
        if (nodeArray[destination.x, destination.y].HasObstacle)
        {
            // Destination has obstacle
            Debug.Log("Cannot move to tile with obstacle");
            return;
        }

        if (!receiveInput)
        {
            Debug.Log("Input disabled while player is moving");
            return;
        }

        ClearTiles();
        
        List<Vector2Int> path = pathfinding.FindPathBetween(initialPosition, destination);
        StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(List<Vector2Int> path)
    {
        receiveInput = false;

        for (int i = 0; i < path.Count; i++)
        {
            StartCoroutine(TakeStep(path[i]));

            while (path[i] != currentPosition)
            {
                yield return null;
            }
        }
        initialPosition = currentPosition;

        receiveInput = true;
    }

    private IEnumerator TakeStep(Vector2Int nextTilePos)
    {
        Vector3 currentTile = nodeArray[currentPosition.x, currentPosition.y].NodeTile.transform.position;
        Vector3 nextTile = nodeArray[nextTilePos.x, nextTilePos.y].NodeTile.transform.position;
        float threshold = 0.05f;

        while (currentPosition != nextTilePos)
        {
            if (Vector3.Distance(transform.position, nextTile) > threshold)
            {
                transform.position = Vector3.Lerp(transform.position, nextTile, movespeed);
                yield return null;
            }
            else
            {
                currentPosition = nextTilePos;
            }
        }
    }

    private void ClearTiles()
    {
        foreach (Node node in nodeArray)
        {
            node.NodeTile.GetComponent<TileData>().SetInsideColor(Color.white);
        }
    }
}
