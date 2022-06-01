using UnityEditor;

namespace Bombsite 
{
    [CustomEditor(typeof(LevelAsset), true)]
    public class LevelAssetEditor : Editor
    {
        private SerializedProperty _scenePath;

        protected virtual void OnEnable() 
            => _scenePath = serializedObject.FindProperty("_scenePath");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

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
        }
    }
}
