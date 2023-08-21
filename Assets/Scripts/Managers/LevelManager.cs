using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SurvivorProto
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] LayerMask m_damagableMask;
        public LayerMask DamagableMask { get { return m_damagableMask; } }
        [SerializeField] GameObject m_experiencePrefab;
        ExperienceManager m_experienceManager;
        Spawner m_spawner;
        
        UpgradeController m_upgradeController;

        [Header("Splash text")]
        [SerializeField] Transform m_splashTextParentTr;
        [SerializeField] GameObject m_splashTextPrefab;
        ObjectPool m_splashTextPool;

        Dictionary<Type, ISkill> m_skillDictionary;

        Timer m_levelTimer;

        protected override void Awake()
        {
            base.Awake();
            m_experienceManager = new ExperienceManager(m_experiencePrefab);
            m_spawner = transform.GetChild(0).GetComponent<Spawner>();
            m_splashTextPool = new ObjectPool(50, 300, 50, m_splashTextPrefab, m_splashTextParentTr);

            Bullet.OnBulletHitDamagable = null;
            m_skillDictionary = new Dictionary<Type, ISkill>();
        }

        private void Start()
        {
            m_upgradeController = new UpgradeController();
            m_levelTimer = new Timer(60 * GameManager.Instance.GameData.LevelDuration, true, false, HandleLevelTimer, HandleEndOfLevelTimer, true);
        }

        public void HandleLevelTimer(float p_deltaTime)
        {
            float time = m_levelTimer.Period - m_levelTimer.CurrentTime;
            int minutes = (int)(time / 60);
            int seconds = (int)(time % 60);
            GUIManager.Instance.SetLevelTimerTo(minutes, seconds);
        }

        public void HandleEndOfLevelTimer()
        {
            Time.timeScale = 0;
        }

        public void HandleLevelUp()
        {
            Time.timeScale = 0;
            GUIManager.Instance.CurrentAmmoTarget.SetActive(false);
            m_upgradeController.SetUpUpgradeMenu();
        }

        public Dictionary<Type, ISkill> SkillDictionary { get { return m_skillDictionary; } }
        public Spawner Spawner { get { return m_spawner; } }
        public ExperienceManager ExperienceManager { get { return m_experienceManager; } }
        public UpgradeController UpgradeController { get { return m_upgradeController; } }
        public ObjectPool SplashTextPool { get { return m_splashTextPool; } }
    }
}