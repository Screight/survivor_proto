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
        [SerializeField] int m_piercing = 1;
        [SerializeField] float m_bulletLifeTime;
        [SerializeField] int m_spread;

        [SerializeField] GameObject m_bulletPrefab;

        public int BaseDamage { get { return m_baseDamage; } }
        public float Recoil { get { return m_recoil; } }
        public float FireRate { get { return m_fireRate; } }
        public int Projectiles { get { return m_projectiles; } }
        public float ReloadTime { get { return m_reloadTime; } }
        public int Ammo { get { return m_ammo; } }
        public float BulletSpeed { get { return m_bulletSpeed; } }
        public int Piercing { get { return m_piercing; } }
        public float BulletLifeTime { get { return m_bulletLifeTime; } }
        public int Spread { get { return m_spread; } }
        public GameObject BulletPrefab { get { return m_bulletPrefab; } }

    }
}
