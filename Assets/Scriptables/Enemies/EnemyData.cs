using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_EnemyData", menuName = "SurvivorProto/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] ENEMY_TYPE m_type;
        [SerializeField] Color m_color;

        [SerializeField] float m_movementSpeed;
        [SerializeField] int m_health;
        [SerializeField] int m_experience;

        [SerializeField] RuntimeAnimatorController m_animatorController;

        [SerializeField] int m_damage = 1;

        public ENEMY_TYPE Type { get { return m_type; } }
        public float MovementSpeed { get { return m_movementSpeed; } }
        public int Health { get { return m_health; } }
        public RuntimeAnimatorController AnimatorController { get { return m_animatorController; } }
        public int Experience { get { return m_experience; } }
        public int Damage { get { return m_damage; } }
        public Color Color { get { return m_color; } }
    }
}
