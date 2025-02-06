using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    #region Singleton
    public static SceneStateManager Instance;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    #endregion   

    public string CurrentScene { get; private set; }

    public Action<string> OnSceneLoadAction;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = scene.name;
    }
    
    public void OnCurrentSceneChangeMethod(string sceneName)
    {
        CurrentScene = sceneName;
        OnSceneLoadAction?.Invoke(CurrentScene);
    }
}
