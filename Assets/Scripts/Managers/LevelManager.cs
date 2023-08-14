using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] GameObject m_experiencePrefab;
        ExperienceManager m_experienceManager;
        Spawner m_spawner;
        
        UpgradeController m_upgradeController;

        [Header("Splash text")]
        [SerializeField] Transform m_splashTextParentTr;
        [SerializeField] GameObject m_splashTextPrefab;
        ObjectPool m_splashTextPool;

        protected override void Awake()
        {
            base.Awake();
            m_experienceManager = new ExperienceManager(m_experiencePrefab);
            m_spawner = transform.GetChild(0).GetComponent<Spawner>();
            m_splashTextPool = new ObjectPool(50, 300, 50, m_splashTextPrefab, m_splashTextParentTr);
        }

        private void Start()
        {
            m_upgradeController = new UpgradeController();
        }

        public void HandleLevelUp()
        {
            Time.timeScale = 0;
            m_upgradeController.SetUpUpgradeMenu();
        }

        public Spawner Spawner { get { return m_spawner; } }
        public ExperienceManager ExperienceManager { get { return m_experienceManager; } }
        public UpgradeController UpgradeController { get { return m_upgradeController; } }
        public ObjectPool SplashTextPool { get { return m_splashTextPool; } }
    }
}