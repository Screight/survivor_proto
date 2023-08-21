using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public abstract class EnemyAI
    {
        protected Enemy m_enemy;
        protected bool m_isEnabled = true;
        public abstract void HandleAI();
        public bool IsEnabled { get { return m_isEnabled; } set { m_isEnabled = value; } }
    }
}
