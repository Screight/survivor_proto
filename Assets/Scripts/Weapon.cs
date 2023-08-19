using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public delegate void OnFireWeapon(Weapon p_weapon);
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
        float m_bulletLifeTime;
        float m_spread;
        float m_bulletSize;
        int m_bounce;
        int m_piercing;
        int m_currentAmmo;

        ObjectPool m_bulletPool;
        Timer m_shootingTimer;

        GUIManager m_GUIManager;

        event OnFireWeapon m_onFireBulletEvent;
        public OnFireWeapon OnFireBulletEvent
        { get { return m_onFireBulletEvent; } set { m_onFireBulletEvent = value; } }

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
            m_piercing = m_data.Piercing;
            m_bulletLifeTime = m_data.BulletLifeTime;
            m_spread = m_data.Spread;
            m_bulletSize = 1;
            m_bounce = m_data.Bounce;


            float shootingPeriod = 1 / m_fireRate;
            m_shootingTimer = new Timer(shootingPeriod, false, true, null, new OnFinishedDelegate(TryShooting), true);

            m_GUIManager = GUIManager.Instance;
        }

        public void InitializePool() {
            if(m_bulletPool != null) { return; }
            m_bulletPool = new ObjectPool(100, 500, 50, m_data.BulletPrefab);
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
            m_onFireBulletEvent?.Invoke(this);

            Vector2 targetPos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

            Vector2 direction = (targetPos - (Vector2)PlayerController.Instance.transform.position).normalized;
            float m_initialAngle = (m_projectiles - 1) / 2 * m_spread;

            // EVEN AN ODD NUMBER OF BULLETS HAVE A SLIGHTLY OFF ANGLE TO LOOK SIMMETRICAL
            if (m_projectiles % 2 == 0) { m_initialAngle += m_spread / 2; }
            // CALCULATE DIRECTION OF FIRST BULLET BY ROTATING THE MAIN DIRECTION
            direction = Quaternion.AngleAxis(m_initialAngle, Vector3.forward) * direction;

            for(int i = 0; i < m_projectiles; i++)
            {
                Bullet bullet = m_bulletPool.GetObject().GetComponent<Bullet>();
                if(i > 0)
                {// ROTATE THE INITIAL DIRECTION BY THE SPREAD AMOUNT TO GET EACH BULLET DIRECTION
                    direction = Quaternion.AngleAxis(-m_spread, Vector3.forward) * direction;
                }
                
                bullet.Initialize(direction, m_bulletSpeed, m_baseDamage);
                bullet.transform.position = PlayerController.Instance.transform.position;
            }
            
            m_GUIManager.SetCurrentAmmoTo(m_currentAmmo, m_maxAmmo);
        }

        void OnFinishReload() {
            m_currentAmmo = m_maxAmmo;

            float shootingPeriod = 1 / m_fireRate;
            m_shootingTimer.Period = shootingPeriod;
            m_shootingTimer.ClearEvents();
            m_shootingTimer.AddOnFinishedEvent(TryShooting);

            m_GUIManager.SetReloadBarTo(false);
            m_GUIManager.SetCurrentAmmoTo(m_currentAmmo, m_maxAmmo);
            Shoot();
        }

        #region Accessors
        public float BaseDamage { get { return m_baseDamage; } set { m_baseDamage = value; } }
        public float Recoil { get { return m_recoil; } set { m_recoil = value; } }
        public float FireRate { get { return m_fireRate; } set { m_fireRate = value; } }
        public int Projectiles { get { return m_projectiles; } set { m_projectiles = value; } }
        public float ReloadTime { get { return m_reloadTime; } set { m_reloadTime = value; } }
        public int MaxAmmo { get { return m_maxAmmo; } set { m_maxAmmo = value; } }
        public int Ammo { get { return m_currentAmmo; } set { m_currentAmmo = value; } }
        public float BulletSpeed { get { return m_bulletSpeed; } set { m_bulletSpeed = value; } }
        public int Piercing { get { return m_piercing; } set { m_piercing = value; } }
        public float BulletLifeTime { get { return m_bulletLifeTime; } set { m_bulletLifeTime = value; } }
        public float BulletSize { get { return m_bulletSize; } set { m_bulletSize = value; } }
        public float Spread { get { return m_spread; } set { m_spread = value; } }
        public int Bounce { get { return m_bounce; } set { m_bounce = value; } }
        public WeaponData Data { get { return m_data; } }
        #endregion
    }

}