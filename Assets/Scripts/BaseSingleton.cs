using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Debug.LogError($"Instance great 1. {name}");
            Destroy(this);
        }
        else{
            Instance = this as T;
        }
        Initialize();
    }

    protected virtual void Initialize()
    {
        Debug.LogError(typeof(T).Name + " initialized");
    }
}