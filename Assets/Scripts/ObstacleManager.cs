using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;
    [SerializeField] private GameObject obstacle;
    

    private void Start()
    {
        PlaceObstacles();
    }

    private void PlaceObstacles()
    {
        float tileSize = GetComponent<GridGenerator>().tileSize;
        
        for (int x = 0; x < obstacleData.width; x++)
        {
            for (int y = 0; y < obstacleData.height; y++)
            {
                if (obstacleData.HasObstacle(x, y))
                {
                    Instantiate(obstacle, new Vector3(x + tileSize / 2, 0f, y + tileSize / 2), obstacle.transform.rotation);
                }
            }
        }
    }
}
