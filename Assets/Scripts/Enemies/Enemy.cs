using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnEnemyDeathDelegate(Enemy p_enemy);
    public delegate void OnEnemyDamagedDelegate(Enemy p_enemy);

    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] EnemyData m_data;

        EnemyStats m_stats;
        EnemyAI m_AIController;

        Rigidbody2D m_rb;
        Animator m_animator;
        SpriteRenderer m_renderer;

        int m_hitTriggerHash;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_animator = GetComponent<Animator>();
            m_renderer = GetComponent<SpriteRenderer>();

            m_hitTriggerHash = Animator.StringToHash("hitTrigger");
        }

        private void Start()
        {
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

        public void Initialize(EnemyData p_data)
        {
            m_data = p_data;
            m_animator.runtimeAnimatorController = p_data.AnimatorController;
            if(m_stats == null) { m_stats = new EnemyStats(m_data); }
            else { m_stats.SetUpStats(m_data); }
            EnemyManager.Instance.AddEnemy(this);
            m_renderer.color = m_data.Color;
        }

        public void TakeDamage(float p_amount)
        {
            GameObject gO = LevelManager.Instance.SplashTextPool.GetObject();
            if (gO != null) {

                GUIData data = GameManager.Instance.GUIData;
                float radius = Random.Range(data.DamageSplashTextInsideRadius, data.DamageSplashTextOutsideRadius);
                float angle = Random.Range(0, 2 * Mathf.PI);
                Vector2 pos = radius * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                gO.GetComponent<SplashText>().SetUp((Vector2)transform.position + pos, (int)p_amount, data.DamageSplashTextDuration, data.DamageSplashTextMaxSize);
            }

            Health -= p_amount;
            if (Health <= 0) { OnDeath(); }
            EnemyManager.Instance.OnEnemyDamagedEvent?.Invoke(this);
            m_renderer.color = Color.white;
            m_animator.SetTrigger(m_hitTriggerHash);
        }

        public void OnDeath()
        {
            Experience expController = LevelManager.Instance.ExperienceManager.ExperienceCollectiblePool.GetObject().GetComponent<Experience>();
            EnemyManager.Instance.OnEnemyGeneralDeathEvent?.Invoke(this);
            expController.Initialize(m_data.Experience);

            expController.transform.position = transform.position;

            LevelManager.Instance.Spawner.ReturnEnemy(this);
            EnemyManager.Instance.RemoveEnemy(this);
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
        public void SetDefaultColor() { m_renderer.color = m_data.Color; }
    }
}