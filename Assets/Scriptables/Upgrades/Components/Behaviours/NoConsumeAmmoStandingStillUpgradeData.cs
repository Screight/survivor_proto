using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_NoConsumeAmmoStandingStillUpgradeData", menuName = "SurvivorProto/Upgrade/Weapon/NoConsumeAmmoStandingStillUpgradeData", order = 1)]
    public class NoConsumeAmmoStandingStillUpgradeData : UpgradeComponentData
    {
        [Range(0, 1)]
        [Tooltip("The probability of triggering the effect")]
        [SerializeField] float m_probability;
        public override void ApplyUpgrade()
        {
            PlayerController.Instance.WeaponController.OnFireBulletEvent += TriggerEffect;
        }

        void TriggerEffect(Weapon p_weapon)
        {
            Vector2 movement = PlayerController.Instance.Movement;
            bool isPlayerMoving = movement.x != 0 || movement.y != 0;
            if (isPlayerMoving) { return; }

            float randomNum = Random.Range(0, m_probability);
            if(m_probability > randomNum) { return; }

            p_weapon.Ammo++;
        }

        protected override string ParsedDescription(string p_description) { return p_description; }
    }
}