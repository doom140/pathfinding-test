using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private TileDetector tileDetector;
    [SerializeField] private Text hoverTileText;

    private void Update()
    {
        hoverTileText.text = tileDetector.GetHoverTilePos().ToString();
    }
}
