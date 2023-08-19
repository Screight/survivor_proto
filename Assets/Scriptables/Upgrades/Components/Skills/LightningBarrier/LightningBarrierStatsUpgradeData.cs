using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_LightningBarrierStatsUpgradeData", menuName = "SurvivorProto/Upgrade/LightningBarrier/Stats", order = 1)]
    public class LightningBarrierStatsUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_damageMultiplier;
        [Range(0, 1)]
        [SerializeField] float m_sizeMultiplier;
        public override void ApplyUpgrade()
        {
            LightningBarrierSkillController controller = (LevelManager.Instance.SkillDictionary[typeof(LightningBarrierSkillController)] as LightningBarrierSkillController);

            if (m_damageMultiplier > 0) { controller.Damage *= 1 + m_damageMultiplier; }
            if(m_sizeMultiplier > 0) { controller.Size *= 1 + m_sizeMultiplier; }
        }

        public override string ParsedDescription() {
            return m_description.
                Replace("{damageMultiplier}", (100 * m_damageMultiplier).ToString()).
                Replace("{sizeMultiplier}", (100 * m_sizeMultiplier).ToString()); ;
        }
    }
}