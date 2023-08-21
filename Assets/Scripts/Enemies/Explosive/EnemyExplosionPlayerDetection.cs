using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyExplosionPlayerDetection : MonoBehaviour
    {
        [SerializeField] EnemyExplosion m_enemy;

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            if (m_enemy.Health <= 0) { return; }
            m_enemy.Health = 0;
            m_enemy.OnDeath();
        }
    }
}