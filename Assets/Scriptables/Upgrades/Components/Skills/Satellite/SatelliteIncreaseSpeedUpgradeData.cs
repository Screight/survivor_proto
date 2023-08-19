using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteIncreaseSpeedUpgradeData", menuName = "SurvivorProto/Upgrade/Satellite/IncreaseSpeed", order = 1)]
    public class SatelliteIncreaseSpeedUpgradeData : UpgradeComponentData
    {
        [Tooltip("Duration of speed increase")]
        [SerializeField] float m_period;
        [SerializeField] float m_speedMultiplier;
        public override void ApplyUpgrade()
        {
            Timer timer = new Timer(m_period, false, true, OnTick, OnFinish, true);
        }

        void OnTick(float p_deltaTime)
        {
            SatelliteSkillController controller = LevelManager.Instance.SkillDictionary[typeof(SatelliteSkillController)] as SatelliteSkillController;

            float baseSpeed = controller.Data.RotationalSpeed;
            float acceleration = (m_speedMultiplier - 1) * baseSpeed / m_period;
            controller.RotationalSpeed += p_deltaTime * acceleration;
        }

        void OnFinish()
        {
            SatelliteSkillController controller = LevelManager.Instance.SkillDictionary[typeof(SatelliteSkillController)] as SatelliteSkillController;
            controller.RotationalSpeed = controller.Data.RotationalSpeed;
        }

        public override string ParsedDescription() { 
            return m_description.Replace("{period}", m_period.ToString())
                                .Replace("{speedMultiplier}", m_speedMultiplier.ToString()); ; }
    }
}