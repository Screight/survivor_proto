using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T m_instance = null;

    static public bool IsNull { get { return m_instance == null; } }

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject gO = new GameObject($"{typeof(T).Name} (singleton)");
                m_instance = gO.AddComponent<T>();
            }
            return m_instance;
        }
        private set { }
    }

    protected virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
        }
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
    }

}
