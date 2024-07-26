using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObstacleData obstacleData = (ObstacleData)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Obstacle Grid", EditorStyles.boldLabel);
        
        // start obstacle grid 
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Height(300));
        
        for (int y = 0; y < obstacleData.height; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < obstacleData.width; x++)
            {
                bool isObstacle = obstacleData.Obstacles[y, x];
                bool newValue = EditorGUILayout.Toggle(isObstacle, GUILayout.Width(20));
                
                if (newValue != isObstacle)
                {
                    obstacleData.Obstacles[y, x] = newValue;
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
