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

        protected override void Awake()
        {
            base.Awake();
            m_experienceManager = new ExperienceManager(m_experiencePrefab);
            m_spawner = transform.GetChild(0).GetComponent<Spawner>();
        }

        public Spawner Spawner { get { return m_spawner; } }
        public ExperienceManager ExperienceManager { get { return m_experienceManager; } }
    }
}