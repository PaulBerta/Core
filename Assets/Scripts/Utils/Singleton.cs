using UnityEngine;

public abstract class StaticInstance<T> : MonoBehaviour where T : StaticInstance<T>
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;
    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

}

public abstract class Singleton<T> : StaticInstance<T> where T : Singleton<T>
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        };
        base.Awake();
    }
}

public abstract class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T> 
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
