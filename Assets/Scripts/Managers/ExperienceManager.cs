using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class ExperienceManager
    {
        ObjectPool m_expPool;
        
        public ExperienceManager(GameObject p_expPrefab)
        {
            m_expPool = new ObjectPool(50, 300, 50, p_expPrefab);
        }

        public ObjectPool ExperienceCollectiblePool { get { return m_expPool; } }
    }
}