using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_EnemyBulletExplosionUpgradeData", menuName = "SurvivorProto/Upgrade/Behaviours/EnemyBulletExplosion", order = 1)]
    public class EnemyBulletExplosionUpgradeData : UpgradeComponentData
    {
        [SerializeField] int m_numberOfBullets;
        [Range(0, 2)]
        [SerializeField] float m_bulletPercentageDamage;

        public override void ApplyUpgrade()
        {
            EnemyManager.Instance.OnEnemyGeneralDeathEvent += ExplodeIntoBullets;
        }

        void ExplodeIntoBullets(Enemy p_enemy)
        {
            Weapon weaponController = PlayerController.Instance.WeaponController;
            Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.right;
            float spread = 360 / m_numberOfBullets;

            for (int i = 0; i < m_numberOfBullets; i++)
            {
                GameObject gO = weaponController.BulletPool.GetObject();
                if (gO == null) { return; }
                if(i > 0) { direction = Quaternion.AngleAxis(spread, Vector3.forward) * direction; }

                Bullet bullet = gO.GetComponent<Bullet>();
                bullet.Initialize(direction, weaponController.BulletSpeed, weaponController.BaseDamage * m_bulletPercentageDamage);
                bullet.transform.position = p_enemy.transform.position;
            }
        }

        protected override string ParsedDescription(string p_description)
        {

            return p_description.
                                Replace("{numberOfBullets}", m_numberOfBullets.ToString()).
                                Replace("{bulletPercentageDamage}", (100 * m_bulletPercentageDamage).ToString());
        }
    }
}