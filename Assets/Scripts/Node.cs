using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public GameObject NodeTile;
    
    public Vector2Int Position;
    public bool HasObstacle;
    public Node Parent;
    public int GCost; // distance from start node 
    public int HCost; // distance from end node 
    public int FCost => GCost + HCost;

    public Node(Vector2Int position, bool hasObstacle)
    {
        Position = position;
        HasObstacle = hasObstacle;
    } 

}
