using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletDamageUpgradeData", menuName = "SurvivorProto/Upgrade/Stats/BulletDamage", order = 1)]
    public class BulletDamageUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_bulletDamage;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.BaseDamage *= 1 + m_bulletDamage;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{bulletDamage}", (100 * m_bulletDamage).ToString());
        }
    }
}