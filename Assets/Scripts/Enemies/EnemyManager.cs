using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        event OnEnemyDeathDelegate m_onEnemyGeneralDeathEvent;
        public OnEnemyDeathDelegate OnEnemyGeneralDeathEvent { get { return m_onEnemyGeneralDeathEvent; } set { m_onEnemyGeneralDeathEvent = value; } }

        List<Enemy> m_enemyList;

        protected override void Awake()
        {
            base.Awake();
            m_enemyList = new List<Enemy>();
        }

        public void AddEnemy(Enemy p_enemy) { if(!m_enemyList.Contains(p_enemy)) { m_enemyList.Add(p_enemy); } }
        public void RemoveEnemy(Enemy p_enemy) { m_enemyList.Remove(p_enemy); }
        public List<Enemy> EnemyList { get { return m_enemyList; } }

        public void OnEnemyGeneralDeath(Enemy p_enemy)
        {
            OnEnemyGeneralDeathEvent?.Invoke(p_enemy);
        }

    }
}