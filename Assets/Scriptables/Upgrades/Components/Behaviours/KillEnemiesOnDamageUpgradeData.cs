using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_KillEnemiesOnDamageUpgradeData", menuName = "SurvivorProto/Upgrade/Behaviour/KillEnemiesOnDamage", order = 1)]
    public class KillEnemiesOnDamageUpgradeData : UpgradeComponentData
    {
        [Tooltip("Enemies are killed when HP percentage is lower than this variable")]
        [Range(0,1)]
        [SerializeField] float m_healthPercentage;
        public override void ApplyUpgrade()
        {
            EnemyManager.Instance.OnEnemyDamagedEvent += TryKillingEnemies;
        }

        void TryKillingEnemies(Enemy p_enemy)
        {
            if(p_enemy.Health <= 0) { return; }
            if(p_enemy.Health / p_enemy.Stats.MaxHealth > m_healthPercentage) { return; }
            p_enemy.OnDeath();
        }

        public override string ParsedDescription() { return m_description.Replace("{healthPercentage}", (100 * m_healthPercentage).ToString()); }
    }
}