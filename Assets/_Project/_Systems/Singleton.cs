// http://wiki.unity3d.com/index.php/Singleton
// http://www.unitygeek.com/unity_c_singleton/

using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var instances = FindObjectsOfType<T>();
                int count = instances.Length;
                if (count == 0)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
                else
                {
                    for (int i = 1; i < instances.Length; i++)
                    {
                        Destroy(instances[i]);
                    }

                    instance = instances[0];
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Debug.LogWarning("Two Instance" + typeof(T).ToString());
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}