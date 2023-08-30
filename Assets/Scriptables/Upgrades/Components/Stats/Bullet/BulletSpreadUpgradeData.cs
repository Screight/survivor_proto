using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WeaponSpreadUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/Spread", order = 1)]
    public class WeaponSpreadUpgradeData : UpgradeComponentData
    {
        [Tooltip("How much bullets are spread when weapon is fired")]
        [SerializeField] int m_spread;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.Spread += m_spread;
        }

        protected override string ParsedDescription(string p_description)
        {
            return p_description.Replace("{spread}", m_spread.ToString());
        }
    }
}