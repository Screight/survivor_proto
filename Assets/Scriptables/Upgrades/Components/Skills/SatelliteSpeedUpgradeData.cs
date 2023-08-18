using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteSpeedUpgradeData", menuName = "SurvivorProto/Upgrade/Satellite/Speed", order = 1)]
    public class SatelliteSpeedUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_speedMultiplier;
        public override void ApplyUpgrade()
        {
            (LevelManager.Instance.SkillDictionary[typeof(SatelliteSkillController)] as SatelliteSkillController).RotationalSpeed *= 1 + m_speedMultiplier;
        }

        public override string ParsedDescription() { return m_description.Replace("{speedMultiplier}", (100 * m_speedMultiplier).ToString()); ; }
    }
}