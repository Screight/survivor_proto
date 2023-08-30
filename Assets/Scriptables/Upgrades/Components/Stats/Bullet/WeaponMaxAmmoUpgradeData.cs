using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WeaponMaxAmmoUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/MaxAmmo", order = 1)]
    public class BulletMaxAmmoUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_maxAmmo;

        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.MaxAmmo += m_maxAmmo; ;
        }

        protected override string ParsedDescription(string p_description)
        {
            return p_description.Replace("{maxAmmo}", m_maxAmmo.ToString());
        }
    }
}