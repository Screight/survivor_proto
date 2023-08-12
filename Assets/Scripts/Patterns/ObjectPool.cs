using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Stack<GameObject> m_unusedObjects = new Stack<GameObject>();

    int m_maxNumberOfObjects;
    int m_numberOfObjectsCreated;

    int m_numberOfObjectsToCreateWhenVoid;

    GameObject m_prefab;

    Transform m_parentTr;

    public ObjectPool(int p_numberOfInitialObjects, int p_maxNumberOfObjects, int p_numberOfObjectsToCreateWhenVoid, GameObject p_prefab)
    {
        m_numberOfObjectsCreated = 0;
        m_maxNumberOfObjects = p_maxNumberOfObjects;
        m_numberOfObjectsToCreateWhenVoid = p_numberOfObjectsToCreateWhenVoid;

        m_prefab = p_prefab;

        if (p_maxNumberOfObjects < p_numberOfInitialObjects)
        {
            m_maxNumberOfObjects = p_numberOfInitialObjects;
        }

        GameObject gO = new GameObject("(ObjectPool)" + $"{m_prefab.name}");
        m_parentTr = gO.transform;

        CreateNewObjects(p_numberOfInitialObjects);
    }

    void CreateNewObjects(int p_numberOfObjects)
    {
        for (int i = 0; i < p_numberOfObjects && m_numberOfObjectsCreated < m_maxNumberOfObjects; i++)
        {
            GameObject gO = MonoBehaviour.Instantiate(m_prefab);
            gO.SetActive(false);
            gO.transform.SetParent(m_parentTr);
            m_unusedObjects.Push(gO);
            m_numberOfObjectsCreated++;
        }
    }

    // There is no check of any kind so wrong gO could be inserted into the pool.
    public void AddObject(GameObject p_gO)
    {
        m_unusedObjects.Push(p_gO);
        p_gO.SetActive(false);
        p_gO.transform.SetParent(m_parentTr);
    }

    public GameObject GetObject() {

        if(m_unusedObjects.Count == 0) {
            if (m_numberOfObjectsCreated < m_maxNumberOfObjects)
            {
                CreateNewObjects(m_numberOfObjectsToCreateWhenVoid);
            }
        }

        if (m_unusedObjects.Count > 0) {
            GameObject gO = m_unusedObjects.Pop();
            gO.SetActive(true);
            return gO;
        }
        else return null;
    }

}
