using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public int width = 10;
    public int height = 10;
    public bool[,] Obstacles;

    public void OnValidate()
    {
        Obstacles = new bool[width, height];
    }

    public bool HasObstacle(int x, int y)
    {
        return Obstacles[x, y];
    }

    public void SetObstacle(int x, int y, bool isObstacle)
    {
        Obstacles[x, y] = isObstacle;
    }
}
