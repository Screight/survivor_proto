using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteAddUpgradeData", menuName = "SurvivorProto/Upgrade/Satellite/AddSatellites", order = 1)]
    public class SatelliteAmountUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_numberOfSatellites;
        public override void ApplyUpgrade()
        {
            (LevelManager.Instance.SkillDictionary[typeof(SatelliteSkillController)] as SatelliteSkillController).AddSatellites(m_numberOfSatellites);
        }

        public override string ParsedDescription() { return m_description.Replace("{numberOfSatellites}", m_numberOfSatellites.ToString()); ; }
    }
}