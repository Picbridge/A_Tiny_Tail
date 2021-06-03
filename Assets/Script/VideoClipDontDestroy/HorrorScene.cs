using UnityEngine;
using UnityEngine.SceneManagement;

public class HorrorScene : Singleton<HorrorScene>
{
    public SpriteRenderer spriteRenderer;

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /* if it's not LevelSelectionMenu */
        if (scene.buildIndex != 15)
            spriteRenderer.enabled = false;
        else
            spriteRenderer.enabled = true;
    }
}
