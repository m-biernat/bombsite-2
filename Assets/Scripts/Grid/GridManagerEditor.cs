using UnityEngine;
using UnityEditor;

namespace Bombsite
{
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Update Grid",
                                 GUILayout.Width(260),
                                 GUILayout.Height(30)))
                UpdateGrid();
            
            if (GUILayout.Button("Add Tile",
                                 GUILayout.Width(260),
                                 GUILayout.Height(30)))
                AddTile();

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void UpdateGrid() 
        {
            var gridManager = target as GridManager; 

            var tiles = GameObject.FindGameObjectsWithTag("Tile");

            if (tiles.Length == 0)
                Debug.LogWarning(
                    "There are no tiles in the scene",
                    gridManager.gameObject);

            foreach (var tile in tiles)
            {
                var gridTile = tile?.GetComponent<Tile>();
                
                if (gridTile)
                {
                    var isListed = gridManager.IsTileListed(gridTile);
                    var inContainer = gridManager.IsTileInContainer(gridTile);
                    
                    if (isListed && inContainer)
                        RenameTile(tile);
                    else if (!isListed && !inContainer)
                    {
                        gridManager.AddTile(gridTile);
                        RenameTile(tile);
                    }
                    else
                    {
                        Debug.LogWarning(
                            "Incorrect object registered to Grid Manager",
                            tile.gameObject);
                        gridManager.RemoveTile(gridTile);
                    }
                }
                else
                    Debug.LogWarning(
                        $"There is incorrectly tagged object {tile.name}",
                        tile);
            }

            gridManager.RemoveUnavailableTiles();
        }

        private void RenameTile(GameObject tile) 
        {
            var pos = tile.transform.localPosition;
            tile.name = $"Tile {pos.x:0.00} {pos.y:0.00} {pos.z:0.00}";
        }

        private void AddTile() 
        {
            var gridManager = target as GridManager; 

            var prefab = gridManager.TilePrefab;

            if (!prefab)
            {
                Debug.LogError(
                    "There is no Tile prefab assigned",
                    gridManager);
                return;
            }

            var go = Instantiate(prefab);
            var tile = go?.GetComponent<Tile>();

            if (tile)
            {
                gridManager.AddTile(tile);
                RenameTile(go);
            }
            else 
            {
                Debug.LogError(
                    "Tile prefab has no Grid Tile component",
                    prefab);
                DestroyImmediate(go);
            }
        }
    }
}
