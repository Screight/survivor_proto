using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class PlayerController : Singleton<PlayerController>, IDamagable
    {
        Vector2 m_movementInput;

        [SerializeField] PlayerData m_playerData;
        [SerializeField] WeaponData m_weaponData;

        [SerializeField] LayerMask m_enemyLayerMask;

        [SerializeField] ParticleSystem m_walkParticles;
        [SerializeField] ParticleSystem m_hitParticles;

        Weapon m_weaponController;

        PlayerStats m_playerStats;
        PLAYER_STATES m_state;

        Rigidbody2D m_rb;
        AudioSource m_audioSource;

        Timer m_healthRegenTimer;


        protected override void Awake()
        {
            base.Awake();
            m_audioSource = GetComponent<AudioSource>();
            m_rb = GetComponent<Rigidbody2D>();
            m_playerStats = new PlayerStats(m_playerData);
            m_weaponController = new Weapon(m_weaponData);
            m_weaponController.InitializePool();

            m_walkParticles.Stop();
        }

        private void Start()
        {
            GUIManager.Instance.SetHealthTo(Health, m_playerStats.MaxHealth);
            m_healthRegenTimer = new Timer(1, false, true, null, OnHealthRegen, true);
        }

        void OnHealthRegen()
        {
            RestoreHealth(m_playerStats.HealthRegen);
        }

        void Update()
        {
            switch (m_state)
            {
                case PLAYER_STATES.DEFAULT:
                    HandleMovement();
                    break;
            }

            if (m_movementInput.x != 0 || m_movementInput.y != 0) {
                if (!m_walkParticles.isEmitting) { m_walkParticles.Play(); }
            } else { m_walkParticles.Stop(); }

            if (m_walkParticles.isEmitting) {
                var vel = m_walkParticles.velocityOverLifetime;
                vel.x = -m_movementInput.x;
                vel.y = -m_movementInput.y;
            }
        }

        void HandleMovement()
        {
            m_movementInput = InputManager.Instance.RawMovementInput;
            //m_movementInput = m_movementInput.normalized;
        }

        private void FixedUpdate()
        {
            m_rb.velocity = m_movementInput * m_playerStats.MovementSpeed;
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            ICollectible collectible = p_collision.GetComponent<ICollectible>();
            if (collectible == null) { return; }
            collectible.OnCollect();
        }

        public void TakeDamage(float p_amount)
        {
            Health -= Damage.GetPhysicalDamage(p_amount, m_playerStats.PhysicalResistance, m_playerStats.Level);
            GUIManager.Instance.SetHealthTo(Health, m_playerStats.MaxHealth);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 3, Vector2.one, 0, m_enemyLayerMask);

            foreach(RaycastHit2D hit in hits)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy == null) { return; }
                Vector2 direction = -(transform.position - enemy.transform.position).normalized;
                enemy.SetVelocity(direction, m_playerData.RepulseOnHitForce);
            }
            m_hitParticles.Play();
            m_audioSource.PlayOneShot(m_playerData.OnHitAC);

            if(Health <= 0) { OnDeath(); }
        }

        public void RestoreHealth(float p_amount)
        {
            if(Health > m_playerStats.MaxHealth){ return; }

            if(m_playerStats.MaxHealth - Health > p_amount) { Health += p_amount; }
            else { Health = m_playerStats.MaxHealth; }
            
            GUIManager.Instance.SetHealthTo(Health, m_playerStats.MaxHealth);
        }

        public void OnDeath()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        public Vector2 Movement { get { return m_movementInput; } }
        public Weapon WeaponController { get { return m_weaponController; } }
        public PlayerStats Stats { get { return m_playerStats; } }

        public float Health
        {
            get { return m_playerStats.Health; }
            set { m_playerStats.Health = value; }
        }
    }
}
