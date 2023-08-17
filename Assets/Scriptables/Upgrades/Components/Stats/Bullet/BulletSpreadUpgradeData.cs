using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WeaponSpreadUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/Spread", order = 1)]
    public class WeaponSpreadUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [Tooltip("How much bullets are spread when weapon is fired")]
        [SerializeField] float m_spread;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.Spread += m_spread;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{spread}", m_spread.ToString());
        }
    }
}