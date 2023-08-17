using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_BulletKnockBack", menuName = "SurvivorProto/Upgrade/Bullet/BulletKnockBack", order = 1)]
    public class BulletRecoilUpgradeData : UpgradeComponentData
    {
        [Range(0, 1)]
        [SerializeField] float m_recoilStrengthModifier;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.Recoil *= 1 + m_recoilStrengthModifier;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{recoilStrengthModifier}", (100 * m_recoilStrengthModifier).ToString());
        }
    }
}