using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyStats
    {
        float m_speed;
        float m_damage;
        float m_health;
        float m_maxHealth;

        public EnemyStats(EnemyData p_data)
        {
            SetUpStats(p_data);
        }

        public void SetUpStats(EnemyData p_data) {
            m_damage = p_data.Damage;
            m_speed = p_data.MovementSpeed;
            m_maxHealth = p_data.Health;
            m_health = m_maxHealth;
        }

        public float Speed { get { return m_speed; } set { m_speed = value; } }
        public float Damage { get { return m_damage; } set { m_damage = value; } }
        public float Health { get { return m_health; } set { m_health = value; } }
        public float MaxHealth { get { return m_maxHealth; } set { m_maxHealth = value; } }
    }
}