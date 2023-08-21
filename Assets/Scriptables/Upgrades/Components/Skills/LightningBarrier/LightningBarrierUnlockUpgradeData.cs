using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_LightningBarrierUnlockUpgradeData", menuName = "SurvivorProto/Upgrade/LightningBarrier/Unlock", order = 1)]
    public class LightningBarrierUnlockUpgradeData : UpgradeComponentData
    {
        [SerializeField] LightningBarrierSkillData m_skillData;
        [SerializeField] GameObject m_lightningBarrierPrefab;
        public override void ApplyUpgrade()
        {
            LightningBarrierSkillController controller = MonoBehaviour.Instantiate(m_lightningBarrierPrefab, PlayerController.Instance.transform.parent).GetComponent<LightningBarrierSkillController>();
            LevelManager.Instance.SkillDictionary.Add(typeof(LightningBarrierSkillController), controller);
        }

        

        public override string ParsedDescription() { return m_description.Replace("{lightningBarrierDamage}", m_skillData.Damage.ToString()); ; }
    }
}