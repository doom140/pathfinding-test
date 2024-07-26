using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerUnit playerUnit;
    public ObstacleData gridData;
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.z));

                if (gridPos.x >= 0 && gridPos.x < gridData.width && gridPos.y >= 0 && gridPos.y < gridData.height)
                {
                    playerUnit.MoveTo(gridPos);
                }
            }
        }
    }
}