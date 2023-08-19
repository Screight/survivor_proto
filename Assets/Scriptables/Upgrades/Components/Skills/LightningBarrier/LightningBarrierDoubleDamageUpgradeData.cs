using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_LightningBarrierDoubleDamageUpgradeData", menuName = "SurvivorProto/Upgrade/LightningBarrier/DoubleDamage", order = 1)]
    public class LightningBarrierDoubleDamageUpgradeData : UpgradeComponentData
    {
        [SerializeField] float m_damageMultiplier;
        [Tooltip("Damage of next tick will be increased every X ticks")]
        [SerializeField] uint m_numberOfTicks;
        public override void ApplyUpgrade()
        {
            LightningBarrierSkillController controller = LevelManager.Instance.SkillDictionary[typeof(LightningBarrierSkillController)] as LightningBarrierSkillController;

            controller.OnLightningBarrierTickEvent += OnTick;
            controller.NumberOfTicks = uint.MaxValue;
        }

        public void OnTick(uint p_ticks)
        {
            LightningBarrierSkillController controller = LevelManager.Instance.SkillDictionary[typeof(LightningBarrierSkillController)] as LightningBarrierSkillController;

            uint rest = p_ticks % m_numberOfTicks;
            // Increase damage
            if (rest == 0) { controller.Damage *= (1 + m_damageMultiplier); }
            // Reduce damage by the same amount it was increased in last tick
            else if(rest == 1) { controller.Damage /= (1 + m_damageMultiplier); }
        }

        public override string ParsedDescription() {
            return m_description.
                Replace("{damageMultiplier}", (1 + m_damageMultiplier).ToString()).
                Replace("{numberOfTicks}", m_numberOfTicks.ToString());
        }
    }
}