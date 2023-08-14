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

        protected override void Awake()
        {
            base.Awake();
            m_experienceManager = new ExperienceManager(m_experiencePrefab);
            m_spawner = transform.GetChild(0).GetComponent<Spawner>();
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
    }
}