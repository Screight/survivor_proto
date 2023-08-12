using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Weapon
    {
        WeaponData m_data;

        float m_baseDamage;
        float m_recoil;
        float m_fireRate;
        int m_projectiles;
        int m_maxAmmo;
        float m_reloadTime;
        float m_bulletSpeed;

        int m_currentAmmo;

        ObjectPool m_bulletPool;
        Timer m_shootingTimer;

        GUIManager m_GUIManager;

        public Weapon(WeaponData p_data)
        {
            m_data = p_data;
            
            m_baseDamage = m_data.BaseDamage;
            m_recoil = m_data.Recoil;
            m_fireRate = m_data.FireRate;
            m_projectiles = m_data.Projectiles;
            m_maxAmmo = m_data.Ammo;
            m_currentAmmo = m_maxAmmo;
            m_reloadTime = m_data.ReloadTime;
            m_bulletSpeed = m_data.BulletSpeed;

            m_bulletPool = new ObjectPool(100, 500, 50, m_data.BulletPrefab);

            float shootingPeriod = 1 / m_fireRate;
            m_shootingTimer = new Timer(shootingPeriod, false, true, null, new OnFinishedDelegate(TryShooting), true);

            m_GUIManager = GUIManager.Instance;
        }

        public ObjectPool BulletPool { get { return m_bulletPool; } }

        public void TryShooting() {
            if(m_currentAmmo > 0) { Shoot(); }
            else { Reload(); }
        }

        void Reload()
        {
            m_shootingTimer.Period = m_reloadTime;
            m_shootingTimer.ClearEvents();
            m_shootingTimer.AddOnTickEvent(UpdateReloadBar);
            m_shootingTimer.AddOnFinishedEvent(OnFinishReload);

            m_GUIManager.SetReloadBarTo(true);
            m_GUIManager.SetReloadBarFillTo(0);
        }

        void UpdateReloadBar(float p_deltaTime)
        {
            GUIManager.Instance.SetReloadBarFillTo(m_shootingTimer.CurrentTime / m_shootingTimer.Period);
        }

        public void Shoot()
        {
            m_currentAmmo--;
            Bullet bullet = m_bulletPool.GetObject().GetComponent<Bullet>();

            Vector2 targetPos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

            bullet.Initialize(targetPos, m_bulletSpeed, m_baseDamage);
            bullet.transform.position = PlayerController.Instance.transform.position;
        }

        void OnFinishReload() {
            m_currentAmmo = m_maxAmmo;

            float shootingPeriod = 1 / m_fireRate;
            m_shootingTimer.Period = shootingPeriod;
            m_shootingTimer.ClearEvents();
            m_shootingTimer.AddOnFinishedEvent(TryShooting);

            m_GUIManager.SetReloadBarTo(false);
        }

        #region Accessors
        public float BaseDamage { get { return m_baseDamage; } set { m_baseDamage = value; } }
        public float Recoil { get { return m_recoil; } set { m_recoil = value; } }
        public float FireRate { get { return m_fireRate; } set { m_fireRate = value; } }
        public int Projectiles { get { return m_projectiles; } set { m_projectiles = value; } }
        public float ReloadTime { get { return m_reloadTime; } set { m_reloadTime = value; } }
        public int Ammo { get { return m_maxAmmo; } set { m_maxAmmo = value; } }
        public WeaponData Data { get { return m_data; } }
        #endregion
    }

}