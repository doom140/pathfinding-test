using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobileUnitBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Vector2Int initialPosition;

    protected Vector2Int CurrentPosition;
    protected bool ReceiveInput;

    private void Awake()
    {
        CurrentPosition = initialPosition;
    }

    public IEnumerator FollowPath(List<Vector2Int> path)
    {
        ReceiveInput = false;

        for (int i = 0; i < path.Count; i++)
        {
            StartCoroutine(TakeStep(path[i]));

            while (path[i] != CurrentPosition)
            {
                yield return null;
            }
        }
        initialPosition = CurrentPosition;

        if (GameManager.Instance.gameState == GameManager.GameState.PlayerTurn)
        {
            GameManager.Instance.UpdateState(GameManager.GameState.EnemyTurn);
        }
        else GameManager.Instance.UpdateState(GameManager.GameState.PlayerTurn);
    }

    private IEnumerator TakeStep(Vector2Int nextTilePos)
    {
        Node[,] nodeArray = GameManager.Instance.nodeArray;
        
        Vector3 currentTile = nodeArray[CurrentPosition.x, CurrentPosition.y].NodeTile.transform.position;
        Vector3 nextTile = nodeArray[nextTilePos.x, nextTilePos.y].NodeTile.transform.position;
        float threshold = 0.05f;

        while (CurrentPosition != nextTilePos)
        {
            if (Vector3.Distance(transform.position, nextTile) > threshold)
            {
                transform.position = Vector3.Lerp(transform.position, nextTile, moveSpeed);
                yield return null;
            }
            else
            {
                CurrentPosition = nextTilePos;
            }
        }
    }

    public Vector2Int GetCurrentPos()
    {
        return CurrentPosition;
    }
}