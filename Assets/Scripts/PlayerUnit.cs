using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerUnit : MonoBehaviour
{
    public ObstacleData obstacleData;
    public float moveSpeed = 5f;

    private Pathfinding pathfinding;
    private List<Vector2Int> currentPath;

    void Start()
    {
        pathfinding = new Pathfinding(obstacleData);
    }

    public void MoveTo(Vector2Int targetPos)
    {
        Vector2Int startPos = Vector2Int.RoundToInt(transform.position);
        currentPath = pathfinding.FindPathBetween(startPos, targetPos);

        if (currentPath != null)
        {
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 startPosition = transform.position;
        
        foreach (Vector2Int pos in currentPath)
        {
            Vector3 targetPosition = new Vector3(pos.x, transform.position.y, pos.y);
            float elapsedTime = 0;

            while (elapsedTime < 1f)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            transform.position = targetPosition;
            startPosition = transform.position;
        }
    }
}