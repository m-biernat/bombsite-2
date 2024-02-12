using UnityEngine;
using UnityEditor;

namespace Bombsite
{
    [CustomEditor(typeof(TileCanvas))]
    public class TileCanvasEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Update Tiles",
                                 GUILayout.Width(260),
                                 GUILayout.Height(30)))
                UpdateTiles();
            
            if (GUILayout.Button("Add Tile",
                                 GUILayout.Width(260),
                                 GUILayout.Height(30)))
                AddTile();

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void UpdateTiles() 
        {
            var tileCanvas = target as TileCanvas; 

            var transform = tileCanvas.TileContainer.transform;

            if (transform.childCount == 0)
            {
                Debug.LogWarning("There are no tiles in the canvas", tileCanvas);
                return;
            }

            if (!tileCanvas.TileContainer)
            {
                Debug.LogWarning("There is no Tile Container assigned", tileCanvas);
                return;
            }

            foreach (Transform child in transform)
                RenameTile(child);
        }

        private void RenameTile(Transform tileTransform) 
        {
            var pos = tileTransform.localPosition;
            tileTransform.name = $"Tile {pos.x:0.00} {pos.y:0.00}";
        }

        private void AddTile() 
        {
            var tileCanvas = target as TileCanvas; 

            var prefab = tileCanvas.TilePrefab;

            if (prefab)
                Instantiate(prefab, tileCanvas.TileContainer.transform);
            else
                Debug.LogError("There is no Tile Prefab assigned", tileCanvas);
        }
    }
}
