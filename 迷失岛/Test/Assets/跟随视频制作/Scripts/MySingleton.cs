using UnityEngine;

public class MySingleton<T> : MonoBehaviour where T : MySingleton<T>
{
    protected static T instance;

    public static T Instance { get => instance; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        else Destroy(gameObject);
    }

    public static bool IsInitialized()
    {
        return instance != null;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}