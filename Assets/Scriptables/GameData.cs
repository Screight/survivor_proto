using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_GameData", menuName = "SurvivorProto/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] List<ExperienceCollectible> m_experienceCollectibleList;
        [SerializeField] GameObject m_experiencePrefab;
        [SerializeField] float m_experienceCollectibleSpeed;

        [SerializeField] List<UpgradeFamilyData> m_upgradeFamilyList;

        public List<ExperienceCollectible> ExperienceCollectibleList { get { return m_experienceCollectibleList; } }
        public float ExperienceCollectibleSpeed { get { return m_experienceCollectibleSpeed; } }

        public List<UpgradeFamilyData> UpgradeFamilyList { get { return m_upgradeFamilyList; } }

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
