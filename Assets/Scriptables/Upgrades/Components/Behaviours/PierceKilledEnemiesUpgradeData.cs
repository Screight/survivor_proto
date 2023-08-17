using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_PierceKilledEnemiesUpgradeData", menuName = "SurvivorProto/Upgrade/Bullet/PierceKilledEnemies", order = 1)]
    public class PierceKilledEnemiesUpgradeData : UpgradeComponentData
    {
        public override void ApplyUpgrade()
        {
            Bullet.OnBulletHitDamagable += PierceKilledEnemies;
        }

        void PierceKilledEnemies(IDamagable p_damagable, Bullet p_bullet)
        {
            Enemy enemy = p_damagable as Enemy;
            if(enemy == null) { return; }
            p_bullet.ObjectsPierced--;
        }

        public override string ParsedDescription() { return m_description; }
    }
}