using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static SceneName targetSceneIndex;
    public enum SceneName
    {
        MainMenuScene,
        GameScene,
        LoadingScene,

    }

    public static void Load(SceneName targetSceneName)
    {
        Loader.targetSceneIndex = targetSceneName;
        SceneManager.LoadScene(Loader.SceneName.LoadingScene.ToString());

        
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetSceneIndex.ToString());
    }
}
