using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletPiercingUpgradeData", menuName = "SurvivorProto/Upgrade/Stats/BulletPiercing", order = 1)]
    public class BulletPiercingUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_bulletPiercing;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.Piercing += m_bulletPiercing;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{bulletPiercing}", m_bulletPiercing.ToString());
        }
    }
}