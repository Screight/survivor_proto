using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletBounceUpgradeData", menuName = "SurvivorProto/Upgrade/Bullet/Bounce", order = 1)]
    public class BulletBounceUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_bounce;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.Bounce += m_bounce;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{bounce}", m_bounce.ToString());
        }
    }
}