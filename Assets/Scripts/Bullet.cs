using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Bullet : MonoBehaviour
    {
        Rigidbody2D m_rb;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        float m_speed;
        float m_damage;

        public void Initialize(Vector2 p_targetPos, float p_speed, float p_damage)
        {
            m_speed = p_speed;
            m_damage = p_damage;

            Vector2 direction = (p_targetPos - (Vector2)PlayerController.Instance.transform.position).normalized;

            m_rb.velocity = m_speed * direction;
        }

        private void OnCollisionEnter2D(Collision2D p_collision)
        {
            IDamagable damagable = p_collision.gameObject.GetComponent<IDamagable>();
            if(damagable == null) { return; }
            damagable.TakeDamage(m_damage);
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            IDamagable damagable = p_collision.gameObject.GetComponent<IDamagable>();
            if (damagable == null) { return; }
            damagable.TakeDamage(m_damage);
        }

    }
}
