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

        private void OnDestroy()
        { m_onBulletHitDamagableEvent = null; }

        private void OnEnable() {
            m_maxLifeTimeTimer?.Restart();
        }

        private void OnDisable() {
            m_maxLifeTimeTimer?.Stop();
        }

        float m_speed;
        float m_damage;

        int m_objectsPierced;
        int m_bounceCount;

        public void Initialize(Vector2 p_direction, float p_speed, float p_damage)
        {
            m_speed = p_speed;
            m_damage = p_damage;
            m_objectsPierced = 0;
            m_bounceCount = 0;

            transform.localScale = Vector3.one * m_weaponController.BulletSize;

            m_rb.velocity = m_speed * p_direction;
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            IDamagable damagable = p_collision.gameObject.GetComponent<IDamagable>();
            if (damagable == null) { return; }
            m_objectsPierced++;
            damagable.TakeDamage(m_damage, p_collision.transform.position);
            m_onBulletHitDamagableEvent?.Invoke(damagable, this);

            // ENEMY KNOCKBACK
            Enemy enemy = damagable as Enemy;
            if(enemy != null) {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemy.SetVelocity(direction, m_weaponController.Recoil);
            }

            if(m_objectsPierced >= m_weaponController.Piercing)
            {
                if(m_bounceCount < m_weaponController.Bounce) { Bounce(p_collision.transform); }
                else { ReturnBulletToPool(); }
            }
        }

        void Bounce(Transform p_collidedTr)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 20.0f, Vector2.one, 0, LevelManager.Instance.DamagableMask);

            Transform objectiveTr;

            // GET A RANDOM CLOSE ENEMY OR A RANDOM ONE IF NONE IS CLOSE ENOUGH
            if(hits.Length != 0) { objectiveTr = hits[Random.Range(0, hits.Length)].transform; }
            else {
                List<Enemy> enemyList = EnemyManager.Instance.EnemyList;
                if(enemyList.Count != 0)
                {
                    objectiveTr = enemyList[Random.Range(0, enemyList.Count)].transform;
                }
                else { objectiveTr = p_collidedTr; }
            }

            Vector2 direction;

            if (objectiveTr == p_collidedTr)
            {// IF THERE IS NO ENEMY AVAILABLE, SET A RANDOM DIRECTION
                float randomAngle = Random.Range(0, 2 * Mathf.PI);
                direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            }
            else { direction = (objectiveTr.position - transform.position).normalized; }

            m_rb.velocity = m_speed * direction;
        }

        void ReturnBulletToPool() { m_weaponController.BulletPool.AddObject(gameObject); }

        public int ObjectsPierced { get { return m_objectsPierced; } set { m_objectsPierced = value; } }

    }
}
