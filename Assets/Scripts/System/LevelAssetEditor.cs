using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Bombsite 
{
    [CustomEditor(typeof(LevelAsset), true)]
    public class LevelAssetEditor : Editor
    {
        private SerializedProperty _scenePath;
        
        private LevelManagerAsset _manager;

        private string[] _options;

        private int _selected;

        protected virtual void OnEnable() {
            _scenePath = serializedObject.FindProperty("_scenePath");
            
            var path = AssetDatabase.GUIDToAssetPath("96b543fbd4c01314eaf9c8730cac199a");
            _manager = AssetDatabase.LoadAssetAtPath<LevelManagerAsset>(path);

            var level = target as LevelAsset;
            _selected = -1;
            _options = new string[_manager.LevelGroups.Count];
            for (int i = 0; i < _manager.LevelGroups.Count; i++)
            {
                _options[i] = _manager.LevelGroups[i].name;
                if (_manager.LevelGroups[i].Levels.Contains(level))
                    _selected = i;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            var level = target as LevelAsset;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(level.ScenePath);

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                _scenePath.stringValue = newPath;
            }
            serializedObject.ApplyModifiedProperties();
            
            EditorGUI.BeginChangeCheck();
            var selected = EditorGUILayout.Popup("Group", _selected, _options); 
            if (EditorGUI.EndChangeCheck())
            {
                if (selected != _selected)
                {
                    _manager.SwapGroups(level, _selected, selected);
                    _selected = selected;
                }
            }

            EditorGUILayout.Space(20);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Open Level",
                                 GUILayout.Width(160),
                                 GUILayout.Height(40)))
                EditorSceneManager.OpenScene(level.ScenePath, OpenSceneMode.Single);
            
            if (GUILayout.Button("Select Scene",
                                 GUILayout.Width(160),
                                 GUILayout.Height(40)))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = newScene;
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}
