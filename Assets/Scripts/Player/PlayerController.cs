using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class PlayerController : Singleton<PlayerController>
    {
        Vector2 m_movementInput;

        [SerializeField] PlayerData m_playerData;
        [SerializeField] WeaponData m_weaponData;

        Weapon m_weaponController;

        PlayerStats m_playerStats;
        PLAYER_STATES m_state;

        Rigidbody2D m_rb;

        protected override void Awake()
        {
            base.Awake();
            m_rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_playerStats = new PlayerStats(m_playerData);
            m_weaponController = new Weapon(m_weaponData);
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

        public Vector2 Movement { get { return m_movementInput; } }
        public Weapon WeaponController { get { return m_weaponController; } }
    }
}
