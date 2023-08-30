using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteUnlockUpgradeData", menuName = "SurvivorProto/Upgrade/Satellite/Unlock", order = 1)]
    public class SatelliteUnlockUpgradeData : UpgradeComponentData
    {
        [SerializeField] SatelliteSkillData m_skillData;
        [SerializeField] GameObject m_satellitePrefab;
        public override void ApplyUpgrade()
        {
            SatelliteSkillController controller = MonoBehaviour.Instantiate(m_satellitePrefab, PlayerController.Instance.transform.parent).GetComponent<SatelliteSkillController>();
            LevelManager.Instance.SkillDictionary.Add(typeof(SatelliteSkillController), controller);
        }
        protected override string ParsedDescription(string p_description) { return p_description.Replace("{satelliteDamage}", m_skillData.Damage.ToString()); ; }
    }
}