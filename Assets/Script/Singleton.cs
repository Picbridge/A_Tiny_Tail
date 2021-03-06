using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (IsNull)
            {
                GameObject obj = new GameObject();
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>() as T;
            }
            return _instance;
        }
    }
    public static bool IsNull
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();

            return _instance == null;
        }
    }

    protected virtual void Awake()
    {
        if (!transform.parent)
        {
            DontDestroyOnLoad(this);
            //SceneManager.activeSceneChanged += DestroyOnSceneLoaded;
        }

        if (this != instance)
        {
            GameObject obj = this.gameObject;
            Destroy(this);
            Destroy(obj);
            return;
        }        
    }

    //void DestroyOnSceneLoaded(Scene current, Scene next)
    //{
    //}
}