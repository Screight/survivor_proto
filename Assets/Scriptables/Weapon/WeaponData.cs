using System.Collections;

using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WeaponData", menuName = "SurvivorProto/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] int m_baseDamage;
        [SerializeField] float m_recoil;
        [SerializeField] float m_fireRate;
        [SerializeField] int m_projectiles;
        [SerializeField] int m_ammo;
        [SerializeField] float m_reloadTime;
        [SerializeField] float m_bulletSpeed;

        [SerializeField] GameObject m_bulletPrefab;

        public int BaseDamage { get { return m_baseDamage; } }
        public float Recoil { get { return m_recoil; } }
        public float FireRate { get { return m_fireRate; } }
        public int Projectiles { get { return m_projectiles; } }
        public float ReloadTime { get { return m_reloadTime; } }
        public int Ammo { get { return m_ammo; } }
        public float BulletSpeed { get { return m_bulletSpeed; } }
        public GameObject BulletPrefab { get { return m_bulletPrefab; } }

    }
}
