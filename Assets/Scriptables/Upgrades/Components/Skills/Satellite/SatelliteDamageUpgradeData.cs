using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteDamageUpgradeData", menuName = "SurvivorProto/Upgrade/Satellite/Damage", order = 1)]
    public class SatelliteDamageUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_damageMultiplier;
        public override void ApplyUpgrade()
        {
            (LevelManager.Instance.SkillDictionary[typeof(SatelliteSkillController)] as SatelliteSkillController).Damage *= 1 + m_damageMultiplier;
        }

        protected override string ParsedDescription(string p_description) { return p_description.Replace("{damageMultiplier}", (100 * m_damageMultiplier).ToString()); ; }
    }
}