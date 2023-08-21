using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_GameData", menuName = "SurvivorProto/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] List<EnemyTypePrefab> m_enemyPrefabList;

        [Tooltip("Given in minutes")]
        [SerializeField] int m_levelDuration;
        [SerializeField] float m_splashTextDMGLifeTime = 1;

        [Header("Experience")]
        [SerializeField] List<ExperienceCollectible> m_experienceCollectibleList;
        [SerializeField] float m_experienceCollectibleSpeed;

        [Header("Upgrades")]
        [SerializeField] List<UpgradeFamilyData> m_upgradeFamilyList;
        [SerializeField] int m_numberOfUpgradesToChooseFrom;

        [Header("Audio")]
        [SerializeField] AudioClip m_onCollectExpAC;
        [SerializeField] AudioClip m_onLevelUpAC;

        public GameObject GetEnemyTypePrefab(ENEMY_TYPE p_type)
        {
            foreach (EnemyTypePrefab element in m_enemyPrefabList)
            {
                if (element.Type == p_type) { return element.Prefab; }
            }
            return null;
        }

        public AudioClip OnCollectExpAC { get { return m_onCollectExpAC; } }
        public AudioClip OnLevelUpAC { get { return m_onLevelUpAC; } }
        public float SplashTextDMGLifeTime { get { return m_splashTextDMGLifeTime; } }
        public int NumberOfUpgradesToChooseFrom { get { return m_numberOfUpgradesToChooseFrom; } }
        public List<ExperienceCollectible> ExperienceCollectibleList { get { return m_experienceCollectibleList; } }
        public float ExperienceCollectibleSpeed { get { return m_experienceCollectibleSpeed; } }
        public List<UpgradeFamilyData> UpgradeFamilyList { get { return m_upgradeFamilyList; } }

        public int LevelDuration { get { return m_levelDuration; } }
    }

    [System.Serializable]
    public struct ExperienceCollectible
    {
        [Tooltip("Max amount of exp it can give")]
        [SerializeField] int m_maxValue;
        [SerializeField] Sprite m_sprite;

        public int MaxValue { get { return m_maxValue; } }
        public Sprite Sprite { get { return m_sprite; } }
    }

    [System.Serializable]
    public struct EnemyTypePrefab
    {
        [SerializeField] ENEMY_TYPE m_type;
        [SerializeField] GameObject m_prefab;

        public ENEMY_TYPE Type { get { return m_type; } }
        public GameObject Prefab { get { return m_prefab; } }
    }
}
