using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnEnemyDeathDelegate(Enemy p_enemy);
    public delegate void OnEnemyDamagedDelegate(Enemy p_enemy);

    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected EnemyData m_data;

        protected EnemyStats m_stats;
        protected EnemyAI m_AIController;

        protected Rigidbody2D m_rb;
        protected Animator m_animator;
        protected SpriteRenderer m_renderer;

        int m_hitTriggerHash;

        protected virtual void Awake()
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
            if (m_AIController != null && m_AIController.IsEnabled) { m_AIController.HandleAI(); }
        }

        private void OnCollisionEnter2D(Collision2D p_collision)
        {
            if (p_collision.gameObject.GetComponent<PlayerController>() == null) { return; }
            PlayerController.Instance.TakeDamage(m_data.Damage);
        }

        public void Initialize(EnemyData p_data)
        {
            m_data = p_data;
            m_animator.runtimeAnimatorController = p_data.AnimatorController;
            InitializeStats(p_data);
            EnemyManager.Instance.AddEnemy(this);
            m_renderer.color = m_data.Color;

            if (m_AIController == null) { m_AIController = new EnemyAIFollower(this, m_rb); }
            m_AIController.IsEnabled = true;
        }

        protected virtual void InitializeStats(EnemyData p_data)
        {
            if(m_stats == null) { m_stats = new EnemyStats(m_data); }
            else { m_stats.SetUpStats(p_data); }
        }

        public virtual void TakeDamage(float p_amount)
        {
            if(Health <= 0) { return; }
            HandleSplashText(p_amount);

            Health -= p_amount;
            if (Health <= 0) { OnDeath(); }
            EnemyManager.Instance.OnEnemyDamagedEvent?.Invoke(this);
            m_renderer.color = Color.white;
            m_animator.SetTrigger(m_hitTriggerHash);
        }

        protected void HandleSplashText(float p_amount)
        {
            GameObject gO = LevelManager.Instance.SplashTextPool.GetObject();
            if (gO != null)
            {

                GUIData data = GameManager.Instance.GUIData;
                float radius = Random.Range(data.DamageSplashTextInsideRadius, data.DamageSplashTextOutsideRadius);
                float angle = Random.Range(0, 2 * Mathf.PI);
                Vector2 pos = radius * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                gO.GetComponent<SplashText>().SetUp((Vector2)transform.position + pos, (int)p_amount, data.DamageSplashTextDuration, data.DamageSplashTextMaxSize);
            }
        }

        public virtual void OnDeath()
        {
            GameObject gO = LevelManager.Instance.ExperienceManager.ExperienceCollectiblePool.GetObject();

            if(gO != null) {
                Experience expController = gO.GetComponent<Experience>();
                expController.Initialize(m_data.Experience);
                expController.transform.position = transform.position;
            }
            else { PlayerController.Instance.Stats.GainExperience(m_data.Experience); }
            
            EnemyManager.Instance.OnEnemyGeneralDeathEvent?.Invoke(this);

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