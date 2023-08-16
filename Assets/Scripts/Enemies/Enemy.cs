using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnEnemyDeathDelegate(Enemy p_enemy);

    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] EnemyData m_data;

        EnemyStats m_stats;
        EnemyAI m_AIController;

        Rigidbody2D m_rb;

        event OnEnemyDeathDelegate m_onEnemyDeathEvent;
        public OnEnemyDeathDelegate OnEnemyDeathEvent { get { return m_onEnemyDeathEvent; } }

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_stats = new EnemyStats(m_data);
            m_AIController = new EnemyAIFollower(this, m_rb);
        }

        private void Update()
        {
            m_AIController.HandleAI();
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            if (p_collision.GetComponent<PlayerController>() == null) { return; }
            PlayerController.Instance.TakeDamage(m_data.Damage);
        }

        public void Initialize() { if (m_stats != null) { Health = m_stats.MaxHealth; } }

        public void TakeDamage(float p_amount)
        {
            Health -= p_amount;
            if (Health <= 0) { OnDeath(); }
        }

        public void OnDeath()
        {
            Experience expController = LevelManager.Instance.ExperienceManager.ExperienceCollectiblePool.GetObject().GetComponent<Experience>();
            m_onEnemyDeathEvent?.Invoke(this);
            EnemyManager.Instance.OnEnemyGeneralDeath(this);
            expController.Initialize(m_data.Experience);
            expController.transform.position = transform.position;
            LevelManager.Instance.Spawner.ReturnEnemy(this);
        }

        public void RestoreHealth(float p_amount)
        {
            Debug.Log("Restore " + p_amount + " health.");
        }
        public void SetVelocity(Vector2 p_direction, float p_speed) {
            m_rb.velocity = p_direction * p_speed;
        }
        public EnemyStats Stats { get { return m_stats; } }
        public EnemyData Data { get { return m_data; } }
        public float Health
        {
            get { return m_stats.Health; }
            set { m_stats.Health = value; }
        }
    }
}