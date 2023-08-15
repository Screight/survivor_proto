using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Bullet : MonoBehaviour
    {
        Rigidbody2D m_rb;
        Weapon m_weaponController;

        Timer m_maxLifeTimeTimer;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_weaponController = PlayerController.Instance.WeaponController;
            m_maxLifeTimeTimer = new Timer(m_weaponController.BulletLifeTime, false, false, null, ReturnBulletToPool, false);
        }

        private void OnEnable() {
            m_maxLifeTimeTimer?.Restart();
        }

        private void OnDisable() {
            m_maxLifeTimeTimer?.Stop();
        }

        float m_speed;
        float m_damage;

        int m_objectsPierced;

        public void Initialize(Vector2 p_targetPos, float p_speed, float p_damage)
        {
            m_speed = p_speed;
            m_damage = p_damage;
            m_objectsPierced = 0;

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
            m_objectsPierced++;
            damagable.TakeDamage(m_damage);

            GameObject gO = LevelManager.Instance.SplashTextPool.GetObject();

            if(gO != null)
            {
                gO.GetComponent<SplashText>().SetUp(p_collision.transform.position, (int)m_damage);
            }

            if(m_objectsPierced >= m_weaponController.Piercing)
            {
                ReturnBulletToPool();
            }
        }

        void ReturnBulletToPool() { m_weaponController.BulletPool.AddObject(gameObject); }

    }
}
