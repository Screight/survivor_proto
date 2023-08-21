using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_EnemyExplosionData", menuName = "SurvivorProto/Enemy/Explosive", order = 1)]
    public class EnemyExplosionData : EnemyData
    {
        [SerializeField] int m_explosionDamage;
        [SerializeField] float m_explosionRadius;

        [Header("Audio")]
        [SerializeField] AudioClip m_explosionAlertAC;
        [SerializeField] AudioClip m_explosionAC;

        public AudioClip ExplosionAlertAC { get { return m_explosionAlertAC; } }
        public AudioClip ExplosionAC { get { return m_explosionAC; } }

        public float ExplosionDamage { get { return m_explosionDamage; } }
        public float ExplosionRadius { get { return m_explosionRadius; } }
    }
}
