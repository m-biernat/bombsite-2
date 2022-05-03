using UnityEngine;
using UnityEditor;

namespace Bombsite
{
    [CustomEditor(typeof(TileManager))]
    public class TileManagerEditor : Editor
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
            var tileManager = target as TileManager; 

            var tiles = GameObject.FindGameObjectsWithTag("Tile");

            if (tiles.Length == 0)
            {
                Debug.LogWarning("There are no tiles in the scene", tileManager);
                return;
            }

            if (!tileManager.TileContainer)
            {
                Debug.LogWarning("There is no Tile Container assigned", tileManager);
                return;
            }

            var transform = tileManager.TileContainer.transform;

            foreach (var tile in tiles)
            {
                tile.transform.SetParent(transform, true);
                RenameTile(tile);
            }
        }

        private void RenameTile(GameObject tile) 
        {
            var pos = tile.transform.localPosition;
            tile.name = $"Tile {pos.x:0.00} {pos.y:0.00} {pos.z:0.00}";
        }

        private void AddTile() 
        {
            var tileManager = target as TileManager; 

            var prefab = tileManager.TilePrefab;

            if (prefab)
                Instantiate(prefab);
            else
                Debug.LogError("There is no Tile Prefab assigned", tileManager);
        }
    }
}
