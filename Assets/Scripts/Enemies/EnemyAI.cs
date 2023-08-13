using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public abstract class EnemyAI
    {
        protected Enemy m_enemy;
        public abstract void HandleAI();
    }
}
