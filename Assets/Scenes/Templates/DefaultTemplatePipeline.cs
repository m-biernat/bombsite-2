using System;
using UnityEditor;
using UnityEditor.SceneTemplate;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bombsite
{
    public class DefaultTemplatePipeline : ISceneTemplatePipeline
    {
        public virtual bool IsValidTemplateForInstantiation(SceneTemplateAsset sceneTemplateAsset)
        {
            return true;
        }

        public virtual void BeforeTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, bool isAdditive, string sceneName)
        {
        
        }

        public virtual void AfterTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, Scene scene, bool isAdditive, string sceneName)
        {
            EditorApplication.Beep();
            
            if (EditorSceneManager.SaveScene(scene))
            {
                AddSceneToBuildIndex(scene);
                AddSceneToLevelManager(scene);
            }
            else
                Debug.LogWarning("Remember to add this scene to build index and level manager");
        }

        private void AddSceneToBuildIndex(Scene scene)
        {
            var scenes = EditorBuildSettings.scenes;
            
            Array.Resize<EditorBuildSettingsScene>(ref scenes, scenes.Length + 1);
            
            scenes.SetValue(new EditorBuildSettingsScene(scene.path, true), scenes.Length - 1);
            
            EditorBuildSettings.scenes = scenes;
        }

        private void AddSceneToLevelManager(Scene scene)
        {
            var asset = ScriptableObject.CreateInstance<LevelAsset>();

            AssetDatabase.CreateAsset(asset, $"Assets/Content/Levels/{scene.name}.asset");
            
            asset.Initialize(scene.path);
            var group = AssetDatabase.LoadAssetAtPath<LevelGroupAsset>("Assets/Content/Levels/Development.asset");
            group.Levels.Add(asset);
            
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
