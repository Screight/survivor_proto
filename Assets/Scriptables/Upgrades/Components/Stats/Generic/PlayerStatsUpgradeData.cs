using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletBounceUpgradeData", menuName = "SurvivorProto/Upgrade/BaseStats/Player", order = 1)]
    public class PlayerStatsUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_health;

        [SerializeField] float m_healthRegen;

        [Range(0,1)]
        [SerializeField] float m_movementSpeed;

        [SerializeField] int m_physicalResistance;

        public override void ApplyUpgrade()
        {
            PlayerStats stats = PlayerController.Instance.Stats;
            stats.MaxHealth += m_health;
            stats.Health += m_health;
            stats.HealthRegen += m_healthRegen;
            stats.PhysicalResistance += m_physicalResistance;
            stats.MovementSpeed *= 1 + m_movementSpeed;
        }

        protected override string ParsedDescription(string p_description)
        {
            return p_description.Replace("{health}", m_health.ToString())
                                .Replace("{healthRegen}", m_healthRegen.ToString())
                                .Replace("{movementSpeed}", (100 * m_movementSpeed).ToString())
                                .Replace("{physicalResistance}", m_physicalResistance.ToString());
        }
    }
}