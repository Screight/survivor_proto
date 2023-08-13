using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyData m_data;

        EnemyStats m_stats;
        EnemyAI m_AIController;

        Rigidbody2D m_rb;

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
            Debug.Log("Player damaged!");
        }

        public EnemyStats Stats { get { return m_stats; } }
    }
}