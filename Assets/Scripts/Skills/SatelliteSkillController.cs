using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class SatelliteSkillController : MonoBehaviour, ISkill
    {
        [SerializeField] GameObject m_prefab;
        [SerializeField] SatelliteSkillData m_skillData;

        float m_damage;
        float m_rotationalSpeed;
        int m_numberOfSatellites;
        float m_size;
        float m_radius;
        float m_recoil;

        Transform m_playerTr;

        private void Start()
        {
            m_playerTr = PlayerController.Instance.transform;
            Initialize();
        }

        private void Update()
        {
            transform.position = m_playerTr.position;
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * m_rotationalSpeed, Vector3.forward) * transform.rotation;
        }

        public void Initialize()
        {
            m_damage = m_skillData.Damage;
            m_rotationalSpeed = m_skillData.RotationalSpeed;
            m_size = m_skillData.Size;
            m_radius = m_skillData.Radius;
            m_recoil = m_skillData.Recoil;

            AddSatellites(m_skillData.NumberOfSatellites);
        }

        public void AddSatellites(int p_numberOfSatellites)
        {
            m_numberOfSatellites += p_numberOfSatellites;

            float angleSeparation = 2 * Mathf.PI / m_numberOfSatellites;

            for (int i = 0; i < m_numberOfSatellites; i++)
            {
                float angle = angleSeparation * i;
                Transform tr;
                if (i < (m_numberOfSatellites - p_numberOfSatellites)) { tr = transform.GetChild(i); }
                else { tr = Instantiate(m_prefab, transform).transform; }

                tr.localPosition = m_radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            }
        }

        #region Accessors
        public float Damage { get { return m_damage; } set { m_damage = value; } }
        public float RotationalSpeed { get { return m_rotationalSpeed; } set { m_rotationalSpeed = value; } }
        public int NumberOfSatellites { get { return m_numberOfSatellites; } set { m_numberOfSatellites = value; } }
        public float Size { get { return m_size; } set { m_size = value; } }
        #endregion

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            IDamagable damagable = p_collision.gameObject.GetComponent<IDamagable>();
            if (damagable == null) { return; }
            damagable.TakeDamage(m_damage, p_collision.transform.position);

            // ENEMY KNOCKBACK
            Enemy enemy = damagable as Enemy;
            if (enemy != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemy.SetVelocity(direction, m_recoil);
            }
        }
        public SatelliteSkillData Data { get { return m_skillData; } }
    }
}