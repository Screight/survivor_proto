using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WeaponFireRateUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/FireRate", order = 1)]
    public class WeaponFireRateUpgradeData : UpgradeComponentData
    {
        [Range(0,1)]
        [SerializeField] float m_fireRateMultiplier;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.FireRate += m_fireRateMultiplier; ;
        }

        public override string ParsedDescription()
        {
            return m_description.Replace("{fireRateMultiplier}", (100 * m_fireRateMultiplier).ToString());
        }
    }
}