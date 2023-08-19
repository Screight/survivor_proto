using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_GameData", menuName = "SurvivorProto/GameData", order = 1)]
    public class GameData : ScriptableObject
    {


        [Tooltip("Given in minutes")]
        [SerializeField] int m_levelDuration;
        [SerializeField] float m_splashTextDMGLifeTime = 1;

        [Header("Experience")]
        [SerializeField] List<ExperienceCollectible> m_experienceCollectibleList;
        [SerializeField] float m_experienceCollectibleSpeed;

        [Header("Upgrades")]
        [SerializeField] List<UpgradeFamilyData> m_upgradeFamilyList;
        [SerializeField] int m_numberOfUpgradesToChooseFrom;

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
}
