using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletSizeUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/BulletSize", order = 1)]
    public class BulletSizeUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_bulletSizeMultiplier;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.BulletSize *= 1 + m_bulletSizeMultiplier;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{bulletSizeMultiplier}", (100 * m_bulletSizeMultiplier).ToString());
        }
    }
}