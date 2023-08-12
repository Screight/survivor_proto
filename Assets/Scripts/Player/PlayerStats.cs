using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class PlayerStats
    {
        float m_movementSpeed;
        float m_health;
        float m_maxHealth;

        public PlayerStats(PlayerData p_data)
        {
            m_movementSpeed = p_data.MovementSpeed;
            m_maxHealth = p_data.Health;
            m_health = m_maxHealth;
        }

        public float MovementSpeed { get { return m_movementSpeed; } set { m_movementSpeed = value; } }
        public float Health { get { return m_health; } set { m_health = value; } }
    }
}
