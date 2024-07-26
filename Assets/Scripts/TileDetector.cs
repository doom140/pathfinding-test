using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetector : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private LayerMask gridLayer;

    private Camera mainCamera;
    private Vector2Int hoveringTilePosition;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit mouseHit, Mathf.Infinity, gridLayer))
        {
            hoveringTilePosition = mouseHit.transform.GetComponent<TileData>().GetTilePosition();
        }
        if (Input.GetMouseButtonDown(0))
        {
            player.MoveToTile(hoveringTilePosition);
        }
    }

    public Vector2 GetHoverTilePos()
    {
        return hoveringTilePosition;
    }
}
