using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletSpeedUpgradeData", menuName = "SurvivorProto/Upgrade/Bullet/BulletSpeed", order = 1)]
    public class BulletSpeedUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_bulletSpeed;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.BulletSpeed *= 1 + m_bulletSpeed;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{bulletSpeed}", (100 * m_bulletSpeed).ToString());
        }
    }
}