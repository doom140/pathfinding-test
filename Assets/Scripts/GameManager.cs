using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public PlayerScript player;
    [SerializeField] private EnemyAI enemy;

    private GridGenerator gridGenerator;
    public Pathfinding Pathfinding;
    
    public static GameManager Instance;
    public Node[,] nodeArray { get; private set; }

    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }
    public GameState gameState;
    
    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
        
        Pathfinding = new Pathfinding(obstacleData);
        Pathfinding.PopulateTilesFromGrid(gridGenerator.GetTileGrid());
        nodeArray = Pathfinding.GetNodeArray();

        Instance = this;
    }

    private void Start()
    {
        UpdateState(GameState.PlayerTurn);
    }


    public void UpdateState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.PlayerTurn:
                player.SetInputReception(true);
                break;
            case GameState.EnemyTurn:
                enemy.ReachPlayer();
                break;
        }
    }

    private Node previousObstacle;
    public void UpdateObstacles()
    {
        // based on game state, set enemy or player as obstacle

        if (previousObstacle != null)
        {
            previousObstacle.HasObstacle = false;
        }
        switch (gameState)
        {
            case GameState.PlayerTurn: // set enemy as obstacle
                Node enemyNode = nodeArray[enemy.GetCurrentPos().x, enemy.GetCurrentPos().y];
                enemyNode.HasObstacle = true;
                previousObstacle = enemyNode;
                break;
            case GameState.EnemyTurn: // set player as obstacle 
                Node playerNode = nodeArray[player.GetCurrentPos().x, player.GetCurrentPos().y];
                playerNode.HasObstacle = true;
                previousObstacle = playerNode;
                break;
        }

    }
}
