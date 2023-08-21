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

        Weapon m_weaponController;

        PlayerStats m_playerStats;
        PLAYER_STATES m_state;

        Rigidbody2D m_rb;

        protected override void Awake()
        {
            base.Awake();
            m_rb = GetComponent<Rigidbody2D>();
            m_playerStats = new PlayerStats(m_playerData);
            m_weaponController = new Weapon(m_weaponData);
            m_weaponController.InitializePool();
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_state)
            {
                case PLAYER_STATES.DEFAULT:
                    HandleMovement();
                    break;
            }
        }

        void HandleMovement()
        {
            m_movementInput = InputManager.Instance.RawMovementInput;
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
            Health -= (int)p_amount;
            GUIManager.Instance.SetHealthTo((int)Health);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 3, Vector2.one, 0, m_enemyLayerMask);

            foreach(RaycastHit2D hit in hits)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy == null) { return; }
                Vector2 direction = -(transform.position - enemy.transform.position).normalized;
                enemy.SetVelocity(direction, m_playerData.RepulseOnHitForce);
            }
        }

        public void RestoreHealth(float p_amount)
        {
            Health += (int)p_amount;
            GUIManager.Instance.SetHealthTo((int)Health);
        }

        public void OnDeath()
        {
            throw new System.NotImplementedException();
        }

        public Vector2 Movement { get { return m_movementInput; } }
        public Weapon WeaponController { get { return m_weaponController; } }
        public PlayerStats Stats { get { return m_playerStats; } }

        public float Health
        {
            get { return m_playerStats.Health; }
            set { m_playerStats.Health = (int)value; }
        }
    }
}
