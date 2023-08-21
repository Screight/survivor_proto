using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnLightningBarrierTick(uint p_ticks);

    public class LightningBarrierSkillController : MonoBehaviour, ISkill
    {
        [SerializeField] LightningBarrierSkillData m_data;

        float m_damage;
        float m_interval;

        List<Enemy> m_enemyInsideBarrierList;
        Timer m_damageTimer;

        uint m_numberOfTicks;

        event OnLightningBarrierTick m_onLightningBarrierTickEvent;
        public OnLightningBarrierTick OnLightningBarrierTickEvent { get { return m_onLightningBarrierTickEvent; } set { m_onLightningBarrierTickEvent = value; } }

        Transform m_playerTr;

        private void Awake()
        {
            m_enemyInsideBarrierList = new List<Enemy>();
        }

        private void Start()
        {
            m_damage = m_data.Damage;
            m_interval = m_data.DamageInterval;

            m_damageTimer = new Timer(m_interval, false, true, null, DamageEnemiesInside, true);
            m_playerTr = PlayerController.Instance.transform;
        }

        private void Update()
        {
            transform.position = m_playerTr.position;
        }

        public void DamageEnemiesInside()
        {
            List<Enemy> enemyList = new List<Enemy>(m_enemyInsideBarrierList);
            for(int i = 0; i < enemyList.Count; i++)
            {
                Enemy enemy = enemyList[i];
                enemy.TakeDamage(m_damage);
            }

            m_numberOfTicks++;
            m_onLightningBarrierTickEvent?.Invoke(m_numberOfTicks);
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            Enemy enemy = p_collision.gameObject.GetComponent<Enemy>();
            if (enemy == null) { return; }
            m_enemyInsideBarrierList.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D p_collision)
        {
            Enemy enemy = p_collision.gameObject.GetComponent<Enemy>();
            if (enemy == null) { return; }
            m_enemyInsideBarrierList.Remove(enemy);
        }

        public float Damage { get { return m_damage; } set { m_damage = value; } }
        public float Interval {
            get { return m_interval; }
            set {
                m_interval = value;
                m_damageTimer.Period = m_interval;
            }
        }
        public float Size { get { return transform.localScale.x; } set { transform.localScale = value * Vector3.one; } }
        public uint NumberOfTicks { get { return m_numberOfTicks; } set { m_numberOfTicks = value; } }
    }
}