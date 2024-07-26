using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class Pathfinding 
{
    private ObstacleData obstacleData;
    private Node[,] nodeArray;

    public Pathfinding(ObstacleData obstacles)
    {
        obstacleData = obstacles;
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        nodeArray = new Node[obstacleData.width, obstacleData.height];
        for (int x = 0; x < obstacleData.width; x++)
        {
            for (int y = 0; y < obstacleData.height; y++)
            {
                nodeArray[x, y] = new Node(new Vector2Int(x, y), obstacleData.HasObstacle(x, y));
            }
        }
    }

    public void PopulateTilesFromGrid(GameObject[,] tiles)
    {
        for (int x = 0; x < obstacleData.width; x++)
        {
            for (int y = 0; y < obstacleData.height; y++)
            {
                nodeArray[x, y].NodeTile = tiles[x, y];
            }
        }
    }

    public Node[,] GetNodeArray()
    {
        return nodeArray;
    }

    public List<Vector2Int> FindPathBetween(Vector2Int start, Vector2Int end)
    {
        Node startNode = nodeArray[start.x, start.y];
        Node endNode = nodeArray[end.x, end.y];
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        
        openList.Add(startNode);
        startNode.NodeTile.GetComponent<TileData>().SetInsideColor(Color.green);
        
        

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            
            for (int x = 0; x < openList.Count; x++)
            {
                if (openList[x].FCost < currentNode.FCost) currentNode = openList[x];
            }
            closedList.Add(currentNode);
            openList.Remove(currentNode);
            currentNode.NodeTile.GetComponent<TileData>().SetInsideColor(Color.red);

            if (currentNode == endNode)
            {
                // path found
                return GetFinalRoute(startNode, endNode);
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (neighbour.HasObstacle || closedList.Contains(neighbour)) continue;

                if (!openList.Contains(neighbour))
                {
                    neighbour.GCost = currentNode.GCost + GetDistance(currentNode, neighbour);
                    neighbour.HCost = GetDistance(endNode, neighbour);
                    openList.Add(neighbour);
                    neighbour.NodeTile.GetComponent<TileData>().SetInsideColor(Color.green);
                    
                    neighbour.Parent = currentNode;
                }
                else if (neighbour.GCost > currentNode.GCost + GetDistance(currentNode, neighbour))
                {
                    neighbour.GCost = currentNode.GCost + GetDistance(currentNode, neighbour);
                }
            }
        }
        return null; // path not found
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
        int distanceY = Mathf.Abs(nodeA.Position.y - nodeB.Position.y);
        
        // assuming tiles have equal height and width
        int lateralStep = 10;
        int diagonalStep = 14;

        if (distanceX > distanceY)
        {
            return distanceY * diagonalStep + ((distanceX - distanceY) * lateralStep);
        }
        else
        {
            return distanceX * diagonalStep + ((distanceY - distanceX) * lateralStep);
        }
    }

    private List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                
                // checking neighbour validity
                int testX = node.Position.x + x;
                if (testX >= 0 && testX < obstacleData.width)
                {
                    int testY = node.Position.y + y;
                    if (testY >= 0 && testY < obstacleData.height)
                    {
                        neighbours.Add(nodeArray[testX, testY]);
                    }
                }
            }   
        }

        return neighbours;
    }

    private List<Vector2Int> GetFinalRoute(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            currentNode.NodeTile.GetComponent<TileData>().SetInsideColor(new Color(0.06f, 0.69f, 1f));
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }
}
