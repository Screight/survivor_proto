using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnBulletHitDamagable(IDamagable p_enemy, Bullet p_bullet);

    public class Bullet : MonoBehaviour
    {
        Rigidbody2D m_rb;
        Weapon m_weaponController;

        Timer m_maxLifeTimeTimer;

        static event OnBulletHitDamagable m_onBulletHitDamagableEvent;
        public static OnBulletHitDamagable OnBulletHitDamagable { get { return m_onBulletHitDamagableEvent; } set { m_onBulletHitDamagableEvent = value; } }

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

        public void Initialize(Vector2 p_direction, float p_speed, float p_damage)
        {
            m_speed = p_speed;
            m_damage = p_damage;
            m_objectsPierced = 0;
            transform.localScale = Vector3.one * m_weaponController.BulletSize;

            m_rb.velocity = m_speed * p_direction;
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            IDamagable damagable = p_collision.gameObject.GetComponent<IDamagable>();
            if (damagable == null) { return; }
            m_objectsPierced++;
            damagable.TakeDamage(m_damage);
            m_onBulletHitDamagableEvent?.Invoke(damagable, this);

            // ENEMY KNOCKBACK
            Enemy enemy = damagable as Enemy;
            if(enemy != null) {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemy.SetVelocity(direction, m_weaponController.Recoil);
            }

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

        public int ObjectsPierced { get { return m_objectsPierced; } set { m_objectsPierced = value; } }

    }
}
