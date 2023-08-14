using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class PlayerStats
    {
        float m_movementSpeed;
        int m_health;
        int m_maxHealth;

        int m_level;
        float m_curExperience;
        float m_maxExperience;

        float m_collectionRange;

        public PlayerStats(PlayerData p_data)
        {
            m_movementSpeed = p_data.MovementSpeed;
            m_maxHealth = p_data.Health;
            m_health = m_maxHealth;
            m_collectionRange = p_data.CollectionRange;

            m_level = 1;
            m_curExperience = 0;
            // SHOULD NOT BE HARDCODED
            m_maxExperience = 10;
        }

        public float CollectionRange { get { return m_collectionRange; } }
        public float MovementSpeed { get { return m_movementSpeed; } set { m_movementSpeed = value; } }
        public int Health { get { return m_health; } set { m_health = value; } }
        public int MaxHealth { get { return m_maxHealth; } set { m_maxHealth = value; } }
        public int Level {
            get { return m_level; }
            set { m_level = value; }
        }
        public float Experience { get { return m_curExperience; } set { m_curExperience = value; } }
        public void GainExperience(float p_exp)
        {
            m_curExperience += p_exp;
            GUIManager.Instance.SetExpFillTo(m_curExperience / m_maxExperience);
            CheckIfLevelUp();
        }

        public bool CheckIfLevelUp()
        {
            if (m_curExperience >= m_maxExperience) { LevelUp(); return true; }
            return false;
        }

        public void LevelUp()
        {
            m_level++;
            m_curExperience = m_curExperience - m_maxExperience;
            GUIManager.Instance.SetLevelTo(m_level);
            GUIManager.Instance.SetExpFillTo(m_curExperience / m_maxExperience);
            // SHOULD NOT BE HARDCODED
            m_maxExperience += 10;
            
            LevelManager.Instance.HandleLevelUp();
        }

    }
}
