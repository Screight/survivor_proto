using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        event OnEnemyDeathDelegate m_onEnemyGeneralDeathEvent;
        public OnEnemyDeathDelegate OnEnemyGeneralDeathEvent { get { return m_onEnemyGeneralDeathEvent; } set { m_onEnemyGeneralDeathEvent = value; } }

        public void OnEnemyGeneralDeath(Enemy p_enemy)
        {
            OnEnemyGeneralDeathEvent?.Invoke(p_enemy);
        }

    }
}