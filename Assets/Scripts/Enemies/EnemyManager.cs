using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        #region Events
        event OnEnemyDeathDelegate m_onEnemyGeneralDeathEvent;
        public OnEnemyDeathDelegate OnEnemyGeneralDeathEvent { get { return m_onEnemyGeneralDeathEvent; } set { m_onEnemyGeneralDeathEvent = value; } }
        event OnEnemyDamagedDelegate m_onEnemyDamagedEvent;
        public OnEnemyDamagedDelegate OnEnemyDamagedEvent { get { return m_onEnemyDamagedEvent; } set { m_onEnemyDamagedEvent = value; } }
        #endregion

        List<Enemy> m_enemyList;

        protected override void Awake()
        {
            base.Awake();
            m_enemyList = new List<Enemy>();
        }

        public void AddEnemy(Enemy p_enemy) { if(!m_enemyList.Contains(p_enemy)) { m_enemyList.Add(p_enemy); } }
        public void RemoveEnemy(Enemy p_enemy) { m_enemyList.Remove(p_enemy); }
        public List<Enemy> EnemyList { get { return m_enemyList; } }
    }
}