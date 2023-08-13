using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_EnemyData", menuName = "SurvivorProto/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] float m_movementSpeed;
        [SerializeField] int m_health;
        [SerializeField] GameObject m_prefab;
        [SerializeField] int m_experience;

        public float MovementSpeed { get { return m_movementSpeed; } }
        public int Health { get { return m_health; } }
        public GameObject Prefab { get { return m_prefab; } }
        public int Experience { get { return m_experience; } }
    }
}
