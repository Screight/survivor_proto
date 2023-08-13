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

        public float MovementSpeed { get { return m_movementSpeed; } }
        public int Health { get { return m_health; } }
    }
}
