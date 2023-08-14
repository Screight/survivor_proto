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
        Dictionary<UpgradeFamilyData, int> m_upgradeFamilyLevel;

        protected override void Awake()
        {
            base.Awake();
            m_experienceManager = new ExperienceManager(m_experiencePrefab);
            m_spawner = transform.GetChild(0).GetComponent<Spawner>();

            m_upgradeFamilyLevel = new Dictionary<UpgradeFamilyData, int>();
            GameManager.Instance.GameData.UpgradeFamilyList.ForEach((UpgradeFamilyData data) =>
            {
                if (!m_upgradeFamilyLevel.ContainsKey(data)) { m_upgradeFamilyLevel.Add(data, 0); }
            });
        }

        public void HandleLevelUp()
        {
            Time.timeScale = 0;
            List<UpgradeFamilyData> upgradeFamilyDataList = GetAvailableUpgradeFamily();

            List<UpgradeData> upgradeDataList = new List<UpgradeData>();

            for(int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, upgradeFamilyDataList.Count);
                UpgradeFamilyData upgradeFamily = upgradeFamilyDataList[index];

                upgradeDataList.Add(upgradeFamily.UpgradeList[m_upgradeFamilyLevel[upgradeFamily]]);
                if(upgradeFamilyDataList.Count > 1) { upgradeDataList.RemoveAt(index); }
            }

            GUIManager.Instance.UpgradeWindowModel.SetUp(upgradeDataList);
        }

        public List<UpgradeFamilyData> GetAvailableUpgradeFamily()
        {
            List<UpgradeFamilyData> upgradeFamilies = new List<UpgradeFamilyData>();

            foreach(KeyValuePair<UpgradeFamilyData, int> pair in m_upgradeFamilyLevel)
            {
                if(pair.Value < pair.Key.UpgradeList.Count) { upgradeFamilies.Add(pair.Key); }
            }
            return upgradeFamilies;
        }

        public Spawner Spawner { get { return m_spawner; } }
        public ExperienceManager ExperienceManager { get { return m_experienceManager; } }
    }
}