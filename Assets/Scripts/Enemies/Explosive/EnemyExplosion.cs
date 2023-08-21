using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyExplosionStats : EnemyStats
    {
        float m_explosionDamage;
        float m_explosionRadius;
        public EnemyExplosionStats(EnemyExplosionData p_data) : base(p_data)
        {
            SetUpStats(p_data);
        }

        public override void SetUpStats(EnemyData p_data)
        {
            EnemyExplosionData data = p_data as EnemyExplosionData;

            base.SetUpStats(p_data);

            m_explosionDamage = data.ExplosionDamage;
            m_explosionRadius = data.ExplosionRadius;
        }
    }

    public class EnemyExplosion : Enemy
    {
        int m_deathTriggerHash;
        Collider2D m_collider;

        [SerializeField] LayerMask m_explosionLayerMask;

        [SerializeField] LayerMask m_layerToAvoidWhileExploding;
        [SerializeField] LayerMask m_defaultLayerCollision;

        CircleCollider2D m_playerDetectionCollider;
        protected override void Awake()
        {
            base.Awake();
            m_deathTriggerHash = Animator.StringToHash("deathTrigger");
            m_collider = GetComponent<Collider2D>();
            m_playerDetectionCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        }

        public override void OnDeath()
        {
            m_animator.SetTrigger(m_deathTriggerHash);
            m_rb.velocity = Vector3.zero;
            m_rb.simulated = false;
            m_AIController.IsEnabled = false;
            m_collider.excludeLayers = m_layerToAvoidWhileExploding;
        }

        private void OnDisable()
        {
            m_collider.excludeLayers = m_defaultLayerCollision;
            m_rb.simulated = true;
            transform.localScale = Vector3.one;
        }

        public override void Initialize(EnemyData p_data)
        {
            base.Initialize(p_data);
            m_playerDetectionCollider.radius = Data.ExplosionRadius;
        }

        protected override void InitializeStats(EnemyData p_data)
        {
            if (m_stats == null) { m_stats = new EnemyExplosionStats(Data); }
            else { m_stats.SetUpStats(p_data); }
        }

        public void HandleExplosion()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Data.ExplosionRadius, Vector3.one, 0.0f, m_explosionLayerMask);

            foreach(RaycastHit2D hit in hits)
            {
                IDamagable damagable = hit.transform.GetComponent<IDamagable>();
                if(damagable == null || damagable == this as IDamagable) { continue; }
                damagable.TakeDamage(Data.ExplosionDamage);
            }
            base.OnDeath();
        }

        public new EnemyExplosionData Data { get { return m_data as EnemyExplosionData; } }
        public new EnemyExplosionStats Stats { get { return m_stats as EnemyExplosionStats; } }
    }
}